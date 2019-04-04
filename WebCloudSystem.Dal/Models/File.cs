using WebCloudSystem.Dal.Models.Base;

namespace WebCloudSystem.Dal.Models {
    public class File : BaseEntity {
        public string FileName {get; set;}
        public string Extension {get; set;}
        public long FileSize {get; set;}
        public User user {get; set;}
    }
}