using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace WebCloudSystem.Bll.Services.Files
{
    public interface IFileWriter
    {
        Task<string> SaveFileOnServer(IFormFile file,int userId);
        string GetFileExtension(IFormFile file);
        string GetFileNameWithoutExtension(IFormFile file);
        bool DeleteFileFromServer(string fileName,int userId);

    }
}