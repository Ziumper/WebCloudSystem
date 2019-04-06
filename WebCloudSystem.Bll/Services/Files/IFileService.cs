using System.Threading.Tasks;
using WebCloudSystem.Bll.Services.Base;
using WebCloudSystem.Bll.Dto.Files;
using Microsoft.AspNetCore.Http;

namespace WebCloudSystem.Bll.Services.Files {
    public interface IFileService : IBaseService
    {
        Task<FileDtoPaged> GetFilesByUser(int userId,FileDtoPagedQuery fileQuery);
        Task<FileDto> Upload(IFormFile file,int userId);
        Task<FileDto> GetFileByUser(int userId, int fileId);
        Task<FileDto> UpdateFile(int userId,FileDtoUpdate file);
        Task<FileDto> DeleteAsync(int userId, int id);
    }
}