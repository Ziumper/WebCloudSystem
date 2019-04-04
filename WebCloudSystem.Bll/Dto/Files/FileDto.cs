using WebCloudSystem.Bll.Dto.Base;

namespace WebCloudSystem.Bll.Dto.Files{

    public class FileDto : BaseDto {
        public string FileName {get; set;}
        public long FileSize {get; set;}
        public string Extension {get; set;}
    }
}