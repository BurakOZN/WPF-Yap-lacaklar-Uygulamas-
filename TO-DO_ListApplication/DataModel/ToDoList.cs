using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class ToDoList : BaseEntity
    {
        public ToDoList()
        {
            Items = new List<Item>();
        }
        public string Name { get; set; }

        #region
        public string UserId { get; set; }
        public virtual User User { get; set; }

        public virtual List<Item> Items { get; set; }
        #endregion
    }
}
