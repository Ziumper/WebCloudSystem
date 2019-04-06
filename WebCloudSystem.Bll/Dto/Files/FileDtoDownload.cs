using WebCloudSystem.Bll.Dto.Base;

namespace WebCloudSystem.Bll.Dto.Files{

    public class FileDtoDownload  {
        public string FileName {get; set;}
        public byte[] FileBytes {get; set;}
        public string ContentType {get; set;}
        
    }
}