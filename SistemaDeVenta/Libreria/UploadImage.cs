using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaVentas.Libreria
{
    public class UploadImage
    {
        public async Task<byte[]> ByteAvatarImageAsync(IWebHostEnvironment environment, string image)
        {


            var archivoOrigen = $"{environment.ContentRootPath}/wwwroot/{image}";
            return File.ReadAllBytes(archivoOrigen);

        }

    }
}
