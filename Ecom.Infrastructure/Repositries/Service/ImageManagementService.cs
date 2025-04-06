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
            var ImageDirctoty = Path.Combine("wwwroot", "Images", src);
            if(Directory.Exists(ImageDirctoty) is not true)
            {
                Directory.CreateDirectory(ImageDirctoty);
            }

            foreach (var item in files)
            {
                if(item.Length > 0)
                {
                    var ImageName = item.FileName;
                    var ImageSrc = $"/Images/{src}/{ImageName}";
                    var root = Path.Combine(ImageDirctoty, ImageName);
                    using (FileStream stream = new FileStream(root, FileMode.Create)) { 
                     
                      await item.CopyToAsync(stream);
                    }
                    SaveImageSrc.Add(ImageName);
                }
            }
            return SaveImageSrc;
        }

        public void DeleteImageAsync(string src)
        {
            var info = _fileProvider.GetFileInfo(src);
            var root = info.PhysicalPath;
            File.Delete(root);
        }
    }
}
