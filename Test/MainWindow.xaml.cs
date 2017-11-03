using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Test
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        dataService dataservice = new dataService();
        private int tempSelectedIndex;
        ToDo todoselect = new ToDo();
        ObservableCollection<ToDo> todo = new ObservableCollection<ToDo>();
        public MainWindow()
        {
            InitializeComponent();
            DisplayToDoAsync();
        }

        private void ToDoListItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ToDolist.SelectedItem != null)
            {
                AddButton.Visibility = Visibility.Hidden;
                UpdateButton.Visibility = Visibility.Visible;
                DeleteButton.Visibility = Visibility.Visible;
                IsComplatedLabel.Visibility = Visibility.Visible;
                IsCompleted.Visibility = Visibility.Visible;

                todoselect = ToDolist.SelectedItem as ToDo;
                tempSelectedIndex = ToDolist.SelectedIndex;
                Name.Text = todoselect.Name;
            }
            else
            {
                AddButton.Visibility = Visibility.Visible;
                UpdateButton.Visibility = Visibility.Hidden;
                DeleteButton.Visibility = Visibility.Hidden;
                IsComplatedLabel.Visibility = Visibility.Hidden;
                IsCompleted.Visibility = Visibility.Hidden;
                ClearForm();
            }
        }

        public int GetNumber(string value)
        {
            int num = 0;

            if (value == "Dokončen")
            {
                num = 1;
            }

            return num;
        }

        public async Task DisplayToDoAsync()
        {
            todo = await dataservice.GetToDoListAsync();

            /*for (int i = 0; i < todo.Count; i++)
            {
                 ToDo item = new ToDo();
                 item.ID = todo[i].ID;
                 item.Name = todo[i].Name;
                 item.DateOfCreated = todo[i].DateOfCreated;
                 item.IsCompleted = todo[i].IsCompleted;

                 Database.SaveItemAsync(item);
             }
             var itemsFromDb = Database.GetItemsNotDoneAsync().Result;*/
            ObservableCollection<ToDo> todonotcompleted = new ObservableCollection<ToDo>();
            for (int i = 0; i < todo.Count; i++)
            {
                if (todo[i].IsCompleted == 0)
                {
                    todonotcompleted.Add(todo[i]);
                }
            }
            ToDolist.ItemsSource = todonotcompleted;
        }

        private async void AddButton_ClickAsync(object sender, RoutedEventArgs e)
        {
            ToDo todo = new ToDo();
            todo.Name = Name.Text;
            
            await dataservice.PostToDo(todo);          
            ClearForm();
            await DisplayToDoAsync();
        }

        private async void DeleteButton_ClickAsync(object sender, RoutedEventArgs e)
        {
            await dataservice.DeleteToDoAsync(todoselect);
            ClearForm();
            await DisplayToDoAsync();
        }

        private async void UpdateButton_ClickAsync(object sender, RoutedEventArgs e)
        {
            ToDo todo = new ToDo();
            todo = await dataservice.GetOneToDoAsync(todoselect.ID);
            todo.Name = Name.Text;
            todo.IsCompleted = GetNumber(IsCompleted.Text);
            await dataservice.UpdateToDoasync(todo);
            ClearForm();
            await DisplayToDoAsync();
        }

        private void ClearForm()
        {
            Name.Text = " ";
        }

        private static ToDoDatabase _database;
        public static ToDoDatabase Database
        {
            get
            {
                if (_database == null)
                {
                    var fileHelper = new FileHelper();
                    _database = new ToDoDatabase(fileHelper.GetLocalFilePath("TodoSQLite.db3"));
                }
                return _database;
            }
        }

        private void IsCompleted_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}