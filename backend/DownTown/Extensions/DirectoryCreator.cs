namespace DownTown.Extensions;

public static class DirectoryCreator
{
    public static void EnsureProductImagesFolderExists(IWebHostEnvironment env)
    {
        if (string.IsNullOrWhiteSpace(env.WebRootPath))
        {
            string defaultWebRootPath = Path.Combine(env.ContentRootPath, "wwwroot");
            env.WebRootPath = defaultWebRootPath;
        }

        if (!Directory.Exists(env.WebRootPath))
        {
            Directory.CreateDirectory(env.WebRootPath);
        }

        string imagesPath = Path.Combine(env.WebRootPath, "Images");
        if (!Directory.Exists(imagesPath))
        {
            Directory.CreateDirectory(imagesPath);
        }
    }
}
