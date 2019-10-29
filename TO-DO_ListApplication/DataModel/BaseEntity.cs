using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public abstract class BaseEntity
    {
        public string Id { get; set; }
        public DateTime CreateAt { get; set; }
        public string CreateBy { get; set; }
        public DateTime UpdateAt { get; set; }
        public string UpdateBy { get; set; }
        public bool IsDelete { get; set; }
    }
}
