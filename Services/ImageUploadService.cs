using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Components.Forms; // If using IBrowserFile

public class ImageUploadService
{
    private readonly Cloudinary _cloudinary;

    public ImageUploadService(IConfiguration config)
    {
        // Initialize Cloudinary with keys from appsettings
        var account = new Account(
            config["Cloudinary:CloudName"],
            config["Cloudinary:ApiKey"],
            config["Cloudinary:ApiSecret"]
        );
        _cloudinary = new Cloudinary(account);
    }

    public async Task<string> UploadImageAsync(IBrowserFile file)
    {
        if (file == null) return null;

        // Create a stream from the uploaded file
        // Note: Set maxFileSize carefully (e.g., 5MB)
        using var stream = file.OpenReadStream(maxAllowedSize: 5 * 1024 * 1024);

        var uploadParams = new ImageUploadParams()
        {
            File = new FileDescription(file.Name, stream),
            // Optional: Auto-transform image to be lighter for web
            Transformation = new Transformation().Quality("auto").FetchFormat("auto")
        };

        var uploadResult = await _cloudinary.UploadAsync(uploadParams);

        // Return the secure URL to save in your database
        return uploadResult.SecureUrl.ToString();
    }
}