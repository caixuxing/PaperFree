using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Trasen.PaperFree.Domain.SeedWork
{
    public interface IMinioFileService
    {
        Task<string> Upload(IFormFile file,string storagePath);
        Task<string> UploadStream(Stream file, string storagePath,string type,string fileName);

        Task<byte[]> DownLoad(string fileName, string storagePath);


        Task<bool> Remove(string fileName, string storagePath);
    }
}
