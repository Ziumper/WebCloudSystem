using WebCloudSystem.Bll.Dto.Files;
namespace WebCloudSystem.Bll.Services.Files {
    public interface IFileReader
    {
        FileDtoDownload ReadFromServer(int userId, string fileNameOnServer,string fileName, string fileExtension);
    }
}