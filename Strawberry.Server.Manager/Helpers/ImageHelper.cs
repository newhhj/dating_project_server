using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Strawberry.Server.Manager.Helpers
{
    public class ImageHelper
    {
        public IWebHostEnvironment Env { get; }

        public ImageHelper(IWebHostEnvironment env)
        {
            this.Env = env;
        }

        public async Task<(string local, string url)> SaveImageFromStreamAsync(Stream stream, string path, string filename)
        {
            while (path.StartsWith("/"))
                path = path[1..];

            var local = Path.Combine(this.Env.WebRootPath, path, Guid.NewGuid().ToString().ToLower().Replace("-", "") + Path.GetExtension(filename));
            var fileInfo = new FileInfo(local);
            if (!fileInfo.Directory.Exists)
                fileInfo.Directory.Create();

            using var fileStream = fileInfo.Create();
            await stream.CopyToAsync(fileStream);

            var url = local.Replace(this.Env.WebRootPath, "").Replace("\\", "/");
            url = url.StartsWith("/") ? url : $"/{url}";

            return (local, url);
        }
    }
}
