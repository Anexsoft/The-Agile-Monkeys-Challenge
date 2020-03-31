using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Common.File
{
    public interface IImageUploadService
    {
        /// <summary>
        /// Attach file
        /// </summary>
        /// <param name="file"></param>
        public void Attach(IFormFile file);

        /// <summary>
        /// Save and return file path
        /// </summary>
        /// <returns></returns>
        public Task<string> SaveAsync();
    }

    public class ImageUploadPhysicalService : IImageUploadService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ImageUploadPhysicalService(IHttpContextAccessor httpContextAccessor) 
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private IFormFile _file;

        public void Attach(IFormFile file)
        {
            _file = file;
        }

        public async Task<string> SaveAsync()
        {
            var filePath = Directory.GetCurrentDirectory() + "\\wwwroot\\images\\";
            var fileName = GetFileName;

            using (var stream = System.IO.File.Create(filePath + fileName))
            {
                await _file.CopyToAsync(stream);
            }

            // Empty _file
            _file = null;

            return GetPath(fileName);
        }

        private string GetFileName 
        {
            get 
            {
                var extension = this._file.FileName.Split('.').Last();
                return Guid.NewGuid().ToString() + "." + extension.ToLower();
            }
        }

        private string GetPath(string fileName) 
        {
            var current = _httpContextAccessor.HttpContext;
            var basePath = $"{current.Request.Scheme}://{current.Request.Host}{current.Request.PathBase}/images/";

            return basePath + fileName;
        }
    }
}
