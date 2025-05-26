using Microsoft.IdentityModel.Tokens;

namespace CarRentalSale.helper
{
    public class UploadCarPhoto
    {
        public async Task<string?> UploadCarImage(IFormFile? file)
        {
            if (file == null) { return null; }
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "cars");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);
            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            string imagePath = $"uploads/cars/{uniqueFileName}";
            return imagePath;

        }
        public bool DeleteImagesFromServer(List<string> imagePaths)
        {
            if (imagePaths.Count==0)
                return false;

            imagePaths.ForEach(imagePath =>
                  {
                      string fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", imagePath.Replace("/", Path.DirectorySeparatorChar.ToString()));
                      if (File.Exists(fullPath))
                      {
                          File.Delete(fullPath);
                         
                      }

                  });



            return true;
        }
    }
}