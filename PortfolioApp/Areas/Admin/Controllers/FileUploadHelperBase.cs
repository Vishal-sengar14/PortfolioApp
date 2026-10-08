namespace PortfolioApp.Areas.Admin.Controllers
{
    /// <summary>Shared helper for saving uploaded images/files under wwwroot and returning a web-relative path.</summary>
    public static class FileUploadHelper
    {
        public static async Task<string?> SaveFileAsync(IFormFile? file, IWebHostEnvironment env, string subFolder)
        {
            if (file == null || file.Length == 0) return null;

            var folder = Path.Combine(env.WebRootPath, subFolder);
            Directory.CreateDirectory(folder);

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var fullPath = Path.Combine(folder, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return $"/{subFolder.Replace('\\', '/')}/{fileName}";
        }
    }
}
