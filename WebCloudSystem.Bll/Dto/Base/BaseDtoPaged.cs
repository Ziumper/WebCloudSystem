using System.Collections.Generic;

namespace WebCloudSystem.Bll.Dto.Base {
      public abstract class BaseDtoPaged<T> {
        public int Page {get; set;}
        public int Size {get; set;}
        public int Count {get; set;}
        public IEnumerable<T> Entities {get; set;}

    }
}