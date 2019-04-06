using WebCloudSystem.Bll.Dto.Files;
using System.IO;
using System;
using WebCloudSystem.Bll.Exceptions;

namespace WebCloudSystem.Bll.Services.Files
{
    public class FileReader : IFileReader
    {
        public FileDtoDownload ReadFromServer(int userId, string fileNameOnServer, string fileName, string fileExtension)
        {
            var pathToFileOnServer = Path.Combine(Directory.GetCurrentDirectory(),"uploads\\",userId.ToString(),fileNameOnServer);
            if(!File.Exists(pathToFileOnServer)){
                throw new ResourceNotFoundException("File not found on server");
            }

            //here it goes 
            var fileBytes = System.IO.File.ReadAllBytes(pathToFileOnServer);

            var result = new FileDtoDownload();
            result.FileName = fileName + fileExtension;
            result.FileBytes = fileBytes;
            result.ContentType = "application/octet-stream";
            return result;
        }
    }
}