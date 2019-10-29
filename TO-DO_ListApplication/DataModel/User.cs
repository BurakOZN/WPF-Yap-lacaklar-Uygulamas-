using System;
using System.Collections.Generic;

namespace Entity
{
    public class User : BaseEntity
    {
        public User()
        {
            ToDoLists = new List<ToDoList>();
        }
        public string UserName { get; set; }
        public string Password { get; set; }

        #region
        public virtual List<ToDoList> ToDoLists { get; set; }
        #endregion
    }
}
