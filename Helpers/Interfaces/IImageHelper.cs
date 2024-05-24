namespace xilopro2.Helpers.Interfaces
{
    public interface IImageHelper
    {
        public string UploadImage(IFormFile imageFile, string folder);

        public bool DeleteImage(string idFile, string folder);
    }

    public class ImageHelper : IImageHelper
    {
        IWebHostEnvironment _env;
        public ImageHelper(IWebHostEnvironment env)
        {

            _env = env;

        }

        public string UploadImage(IFormFile imageFile, string folder)
        {
            string uniqueFileName = string.Empty;
            if (imageFile != null)
            {
                string path = $"Content/{folder}";
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                string uploadFolder = Path.Combine(_env.WebRootPath, path);
                uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                string filePath = Path.Combine(uploadFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    imageFile.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }

        public bool DeleteImage(string idFile, string folder)
        {
            if (idFile != null)
            {
                string path = $"Content/{folder}";
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                string deleteFromFolder = Path.Combine(_env.WebRootPath, path);
                string currentImage = Path.Combine(Directory.GetCurrentDirectory(), deleteFromFolder, idFile);
                if (currentImage != null)
                {
                    if (System.IO.File.Exists(currentImage)) System.IO.File.Delete(currentImage);
                }
                return true;

            }
            return false;
        }


    }


}
