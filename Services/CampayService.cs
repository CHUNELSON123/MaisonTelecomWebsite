using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Net;

namespace MaisonTelecom.Services
{
    public class CampayService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public CampayService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        private async Task<string> GetAccessTokenAsync()
        {
            var baseUrl = _configuration["Campay:BaseUrl"];
            var username = _configuration["Campay:AppUsername"];
            var password = _configuration["Campay:AppPassword"];

            var requestData = new { username = username, password = password };
            var content = new StringContent(JsonSerializer.Serialize(requestData), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{baseUrl}token/", content);

            if (!response.IsSuccessStatusCode)
                throw new Exception("Campay Login Failed. Check Username/Password.");

            var responseString = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(responseString);
            return doc.RootElement.GetProperty("token").GetString();
        }

        public async Task<string> InitializePayment(decimal amount, string email, string phoneNumber, string name, string orderId)
        {
            var token = await GetAccessTokenAsync();
            var baseUrl = _configuration["Campay:BaseUrl"];

            string formattedPhone = FormatPhoneNumber(phoneNumber);
            // Format amount as integer string (e.g. "10")
            string formattedAmount = Convert.ToInt32(amount).ToString(CultureInfo.InvariantCulture);

            var requestData = new
            {
                amount = formattedAmount,
                currency = "XAF",
                from = formattedPhone,
                description = $"Order #{orderId} - Maison Telecom",
                external_reference = orderId
                // redirect_url is optional for Direct Push, but we can keep it
            };

            var content = new StringContent(JsonSerializer.Serialize(requestData), Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", token);

            var response = await _httpClient.PostAsync($"{baseUrl}collect/", content);
            var responseString = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Campay Failed ({response.StatusCode}): {responseString}");
            }

            using var doc = JsonDocument.Parse(responseString);

            // 1. Try to find a Web Redirect Link (Standard Flow)
            if (doc.RootElement.TryGetProperty("link", out var linkProp)) return linkProp.GetString();
            if (doc.RootElement.TryGetProperty("url", out var urlProp)) return urlProp.GetString();

            // 2. Handle "Direct Push" (USSD) Response (The error you got)
            // JSON: {"reference": "...", "ussd_code": "*126#", ...}
            if (doc.RootElement.TryGetProperty("reference", out var refProp))
            {
                string refCode = refProp.GetString();
                string ussdCode = "";
                if (doc.RootElement.TryGetProperty("ussd_code", out var ussdProp))
                {
                    ussdCode = ussdProp.GetString();
                }

                // Redirect to OUR OWN confirmation page with the reference and code
                string appUrl = _configuration["AppUrl"] ?? "";
                if (appUrl.EndsWith("/")) appUrl = appUrl.TrimEnd('/');

                return $"{appUrl}/payment-confirm?reference={refCode}&ussd={WebUtility.UrlEncode(ussdCode)}";
            }

            throw new Exception($"Campay response missing 'link' and 'reference'. Response: {responseString}");
        }

        public async Task<PaymentVerifyResponse> VerifyPayment(string transactionReference)
        {
            var token = await GetAccessTokenAsync();
            var baseUrl = _configuration["Campay:BaseUrl"];

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", token);

            var response = await _httpClient.GetAsync($"{baseUrl}transaction/{transactionReference}/");
            var responseString = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(responseString);

            // Safely get status
            string status = "UNKNOWN";
            if (doc.RootElement.TryGetProperty("status", out var statusProp))
            {
                status = statusProp.GetString();
            }

            string myOrderRef = "";
            if (doc.RootElement.TryGetProperty("external_reference", out var refProp))
            {
                myOrderRef = refProp.GetString();
            }

            // Map SUCCESSFUL to "complete" so our page understands it
            string finalStatus = (status == "SUCCESSFUL") ? "complete" : status;

            return new PaymentVerifyResponse
            {
                Status = finalStatus,
                MyOrderReference = myOrderRef
            };
        }

        private string FormatPhoneNumber(string phone)
        {
            if (string.IsNullOrEmpty(phone)) return "237000000000";
            string digits = Regex.Replace(phone, @"[^\d]", "");
            if (digits.Length == 9) return "237" + digits;
            return digits;
        }
    }

    public class PaymentVerifyResponse
    {
        public string Status { get; set; }
        public string MyOrderReference { get; set; }
    }
}