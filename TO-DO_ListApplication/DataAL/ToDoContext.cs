using Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAL
{
    public class ToDoContext : DbContext
    {
        public ToDoContext():base("MyContext")
        {

        }
        protected override void OnModelCreating(DbModelBuilder mb)
        {
            mb.Entity<User>().HasIndex(x => x.Id);
            mb.Entity<ToDoList>().HasIndex(x => x.Id);
            mb.Entity<Item>().HasIndex(x => x.Id);

            mb.Entity<Item>()
                .HasRequired(x => x.ToDoList)
                .WithMany(x => x.Items)
                .HasForeignKey(x => x.ToDoListId)
                .WillCascadeOnDelete(true);
            mb.Entity<ToDoList>()
                .HasRequired(x => x.User)
                .WithMany(x => x.ToDoLists)
                .HasForeignKey(x => x.UserId)
                .WillCascadeOnDelete(true);

            base.OnModelCreating(mb);
        }
    }
}
