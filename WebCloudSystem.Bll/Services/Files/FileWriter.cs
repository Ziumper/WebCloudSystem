using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using System;


namespace WebCloudSystem.Bll.Services.Files
{
    public class FileWriter : IFileWriter
    {
        public bool DeleteFileFromServer(string fileName,int userId)
        {
            var pathToFile = Path.Combine(Directory.GetCurrentDirectory(),"uploads\\"+userId,fileName);
            if(!File.Exists(pathToFile)) {
                return true;
            }

            File.Delete(pathToFile);
            return true;
        }

        public string GetFileExtension(IFormFile file)
        {
            var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
            return extension;
        }

        public async Task<string> SaveFileOnServer(IFormFile file,int userId)
        {
            CheckIsFolderExistIfNotCreateIt("uploads");
            string fileName;
           
            var extension = GetFileExtension(file);
            fileName = Guid.NewGuid().ToString() + extension; //Create a new Name for the file due to security reasons.
            
            var uploadFolderPath = "uploads\\"+userId;
            CheckIsFolderExistIfNotCreateIt(uploadFolderPath);
            
            var path = Path.Combine(Directory.GetCurrentDirectory(),uploadFolderPath, fileName);

            using (var bits = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(bits);
            }

            return fileName;
        }

        private void CheckIsFolderExistIfNotCreateIt(string folderPath) {
            var path = Path.Combine(Directory.GetCurrentDirectory(),folderPath);
            if(!Directory.Exists(path)){
                Directory.CreateDirectory(path);
            }
        }
    }
}