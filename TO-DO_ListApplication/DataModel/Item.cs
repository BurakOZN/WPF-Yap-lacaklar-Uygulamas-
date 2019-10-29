using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class Item : BaseEntity
    {
        [Display]
        public string Name { get; set; }
        [Display]
        public Status Status { get; set; }
        [Display]
        public DateTime EndDate { get; set; }

        #region FK
        public virtual string ToDoListId { get; set; }
        public virtual ToDoList ToDoList { get; set; }
        #endregion
    }
    public enum Status
    {
        Incomplete,
        Completed
    }
    public class Display : Attribute
    {

    }
}
