using Ecom.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Infrastructure.Repositries.Service
{
    public class ImageManagementService : IImageManagementService
    {
        private readonly IFileProvider _fileProvider;
        public ImageManagementService(IFileProvider fileProvider)
        {
            _fileProvider = fileProvider;
        }
        public async Task<List<string>> AddImageAsync(IFormFileCollection files, string src)
        {
            List<string> SaveImageSrc = new List<string>();
            var sanitizedSrc = src.Trim();
            var ImageDirectory = Path.Combine("wwwroot", "Images", sanitizedSrc);

            if (!Directory.Exists(ImageDirectory))
            {
                Directory.CreateDirectory(ImageDirectory);
            }

            foreach (var item in files)
            {
                if (item.Length > 0)
                {
                    try
                    {
                        var sanitizedFileName = SanitizeFileName(item.FileName);
                        var uniqueFileName = $"{Guid.NewGuid()}_{sanitizedFileName}";

                        var root = Path.Combine(ImageDirectory, uniqueFileName);
                        var fullImagePath = $"/Images/{sanitizedSrc}/{uniqueFileName}";

                        using (FileStream stream = new FileStream(root, FileMode.Create))
                        {
                            await item.CopyToAsync(stream);
                        }

                        SaveImageSrc.Add(fullImagePath);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error saving file: {ex.Message}");
                    }
                }
            }

            return SaveImageSrc;
        }
        private string SanitizeFileName(string fileName)
        {
            var invalidChars = Path.GetInvalidFileNameChars();
            return string.Concat(fileName.Split(invalidChars)).Trim();
        }

        public void DeleteImageAsync(string src)
        {
            var info = _fileProvider.GetFileInfo(src);
            var root = info.PhysicalPath;
            File.Delete(root);
        }
    }
}
