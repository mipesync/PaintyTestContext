using lightning_shop.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using PaintyTestContext.Application.Common.Exceptions;

namespace lightning_shop.Persistence.Services
{
    /// <inheritdoc/>
    public class FileUploader : IFileUploader
    {
        public IFormFile? File { get; set; } = null;
        public string AbsolutePath { get; set; } = string.Empty;
        public string WebRootPath { get; set; } = string.Empty;

        [Obsolete("Obsolete")]
        public async Task<string> UploadFileAsync()
        {
            string fileExtension;
            string fileNameHash;
            var fullName = string.Empty;
            string path;

            Directory.CreateDirectory(Path.Combine(WebRootPath, AbsolutePath));
            
            if (File is not null)
            {
                fileExtension = Path.GetExtension(File.FileName);
                fileNameHash = Guid.NewGuid().ToString();
                fullName = fileNameHash + fileExtension;
                path = Path.Combine(AbsolutePath, fullName);
          
                await using var fileStream = new FileStream(Path.Combine(WebRootPath, path), FileMode.Create);
                await File.CopyToAsync(fileStream);

                return fullName;
            }

            if (File is null)
                throw new BadRequestException("Пустой контент для загрузки");
            
            return fullName;
        }
    }
}
