using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zust.Business.Abstract;

namespace Zust.Business.Concrete
{
    public class ImageService : IImageService
    {

        public async Task<string> SaveFile(IFormFile file)
        {
            if (file != null && file.Length > 0)
            { 
                var fileName = Path.GetFileNameWithoutExtension(file.FileName) + "_" + Guid.NewGuid() + Path.GetExtension(file.FileName);
                 
                var fileDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", "images", "news-feed-post");
                 
                if (!Directory.Exists(fileDirectory))
                {
                    Directory.CreateDirectory(fileDirectory);
                }
                 
                var filePath = Path.Combine(fileDirectory, fileName);
                 
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                 
                return "~/assets/images/news-feed-post/" + fileName;
            }

            return null;
        }


    }

}

