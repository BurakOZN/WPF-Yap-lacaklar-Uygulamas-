using BLL;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ToDoListUI
{
    /// <summary>
    /// Interaction logic for List.xaml
    /// </summary>
    public partial class List : Window
    {
        User user;
        MainWindow mw;
        public List(User usr, MainWindow mw)
        {
            user = usr;
            this.mw = mw;
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var item in user.ToDoLists)
            {
                lstParents.Items.Add(item);
            }
            lstParents.DisplayMemberPath = "Name";
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            mw.Show();
        }

        private void btnAddList_Click(object sender, RoutedEventArgs e)
        {
            var xxx = new ToDoList() { Name = txtListName.Text, UserId = user.Id };
            var result = UOW.Instance.RepToDoList.Add(xxx);
            if (result == CrudState.Success)
            {
                lstParents.Items.Clear();
                foreach (var item in user.ToDoLists)
                {
                    lstParents.Items.Add(item);
                }
                lstParents.DisplayMemberPath = "Name";
                MessageBox.Show("Success");
            }
            else
                MessageBox.Show("Error");
        }

        private void lstParents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshList();
        }

        private void btnAddItem_Click(object sender, RoutedEventArgs e)
        {
            var obj = (ToDoList)lstParents.SelectedItem;
            if (obj == null)
            {
                MessageBox.Show("Lütfen List Seçiniz");
                return;
            }

            DateTime date = DateTime.Now;
            if (!string.IsNullOrEmpty(dtpItemEndDate.Text))
                date = DateTime.Parse(dtpItemEndDate.Text);
            var item1 = new Item() { Name = txtItemName.Text, EndDate = date, Status = Status.Incomplete, ToDoListId = obj.Id };
            var result = UOW.Instance.RepItem.Add(item1);
            if (result == CrudState.Success)
            {
                dgItems.ItemsSource = obj.Items;
                MessageBox.Show("Başarılı");
                RefreshList();
            }
            else
                MessageBox.Show("Hatalı");
        }

        private void BtnDeleteList_Click(object sender, RoutedEventArgs e)
        {
            var obj = (ToDoList)lstParents.SelectedItem;
            if (obj == null)
            {
                MessageBox.Show("Lütfen List Seçiniz");
                return;
            }
            var result = UOW.Instance.RepToDoList.Delete(obj.Id);
            if (result == CrudState.Success)
                MessageBox.Show("Başarılı");

        }

        private void BtnDeleteItem_Click(object sender, RoutedEventArgs e)
        {
            var item = (Item)dgItems.SelectedItem;
            if (item == null)
            {
                MessageBox.Show("Lütfen Item Seçiniz");
                return;
            }
            var result = UOW.Instance.RepItem.Delete(item.Id);
            if (result == CrudState.Success)
            {
                MessageBox.Show("Başarılı");
                RefreshList();
            }

        }
        private void RefreshList()
        {
            var obj = (ToDoList)lstParents.SelectedItem;
            if (obj != null)
            {
                dgItems.ItemsSource = null;
                dgItems.ItemsSource = obj.Items;
            }
        }

        private void BtnComplete_Click(object sender, RoutedEventArgs e)
        {
            var item = (Item)dgItems.SelectedItem;
            if (item == null)
            {
                MessageBox.Show("Lütfen Item Seçiniz");
                return;
            }
            item.Status = Status.Completed;
            var result = UOW.Instance.RepItem.Update(item);
            if (result == CrudState.Success)
            {
                MessageBox.Show("Başarılı");
                RefreshList();
            }
        }

        private void BtnFilter_Click(object sender, RoutedEventArgs e)
        {
            List<Item> source = new List<Item>();
            foreach (var item in dgItems.ItemsSource)
            {
                var it = (Item)item;
                var properties = typeof(Item).GetProperties();
                foreach (var property in properties)
                {
                    var val = Convert.ToString(property.GetValue(it));
                    if (val.Contains(txtFilter.Text))
                    {
                        source.Add(it);
                        break;
                    }

                }
            }
            dgItems_Filtered.ItemsSource = source;
        }
    }
}
