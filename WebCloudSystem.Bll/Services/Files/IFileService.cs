using System.Threading.Tasks;
using WebCloudSystem.Bll.Services.Base;
using WebCloudSystem.Bll.Dto.Files;

namespace WebCloudSystem.Bll.Services.Files {
    public interface IFileService : IBaseService
    {
        Task<FileDtoPaged> GetFilesByUser(string userId,FileDtoPagedQuery fileQuery);
    }
}