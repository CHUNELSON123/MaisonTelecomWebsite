using MaisonTelecom.Data;
using MaisonTelecom.Models;
using Microsoft.EntityFrameworkCore;

namespace MaisonTelecom.Services
{
    public class SiteSettingsService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _factory;

        public SiteSettingsService(IDbContextFactory<ApplicationDbContext> factory)
        {
            _factory = factory;
        }

        public async Task<string> GetSettingAsync(string key, string defaultValue = "")
        {
            using var context = await _factory.CreateDbContextAsync();
            var setting = await context.SiteSettings.FindAsync(key);
            return setting?.Value ?? defaultValue;
        }

        public async Task SetSettingAsync(string key, string value)
        {
            using var context = await _factory.CreateDbContextAsync();
            var setting = await context.SiteSettings.FindAsync(key);
            if (setting == null)
            {
                context.SiteSettings.Add(new SiteSetting { Key = key, Value = value });
            }
            else
            {
                setting.Value = value;
            }
            await context.SaveChangesAsync();
        }

        public async Task<Dictionary<string, string>> GetAllSettingsAsync()
        {
            using var context = await _factory.CreateDbContextAsync();
            return await context.SiteSettings.ToDictionaryAsync(s => s.Key, s => s.Value);
        }
    }
}