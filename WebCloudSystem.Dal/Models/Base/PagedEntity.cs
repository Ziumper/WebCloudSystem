using System.Collections.Generic;

namespace WebCloudSystem.Dal.Models.Base
{
    public class PagedEntity<T> where T:BaseEntity
    {
        public int Count {get; set;}
        public IEnumerable<T> Entities {get; set;}
    }
}