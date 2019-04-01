using System;
using System.ComponentModel.DataAnnotations;

namespace WebCloudSystem.Dal.Models.Base {
    
    public abstract class BaseEntity
    {
        [Key]
        public int Id {get; set;}
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
    }
}
