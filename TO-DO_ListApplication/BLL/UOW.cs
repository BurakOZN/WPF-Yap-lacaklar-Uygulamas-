using DataAL;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class UOW
    {
        private static UOW _Instance;
        public static UOW Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new UOW();
                return _Instance;
            }
        }

        private UOW()
        {
            ToDoContext context = new ToDoContext();
            RepUser = new Repository<User>(context);
            RepToDoList = new Repository<ToDoList>(context);
            RepItem = new Repository<Item>(context);
        }
        public IRepository<User> RepUser;
        public IRepository<ToDoList> RepToDoList;
        public IRepository<Item> RepItem;
    }
}
