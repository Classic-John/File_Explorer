using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.ObjectModel;
using Tema_2_MVP.Models;
using Tema_2_MVP.ViewModels;
using Task = Tema_2_MVP.Models.Task;

namespace Tema_2_MVP.Views
{
    public partial class AddTodoListWindow : Window
    {
        AddingTodoListViewModel ViewModel { get;set; }

        public TodoList SarciniActuale {  get; set; }

        public bool? Root { get; set; }

        public ObservableCollection<TodoList> Sarcini { get; set; }

        public AddTodoListWindow(TodoList Sarcina, bool root)
        {
            InitializeComponent();
            SarciniActuale = Sarcina;
            Root = root;
            ViewModel = new AddingTodoListViewModel();
            DataContext = ViewModel;
        }

        public AddTodoListWindow(TodoList listaSarcini)
        {
            InitializeComponent();
            SarciniActuale = listaSarcini;
            ViewModel = new AddingTodoListViewModel();
            ViewModel.Title = SarciniActuale.Title;
            ViewModel.SelectedImage = SarciniActuale.Image;
            DataContext = ViewModel;
        }

        public AddTodoListWindow(ObservableCollection<TodoList> Liste)
        {
            InitializeComponent();
            Sarcini = Liste;
            ViewModel = new AddingTodoListViewModel();
            DataContext = ViewModel;
        }

        private void AddTDL_Click(object sender, RoutedEventArgs e)
        {
            if(!Root.HasValue)
            {
                if(Sarcini ==  null)
                {
                    SarciniActuale=new TodoList();
                    SarciniActuale.Title = ViewModel.Title;
                    SarciniActuale.Image = ViewModel.SelectedImage;
                }
                else
                {
                   Sarcini.Add(new TodoList()
                    {
                        Id = ViewModel.Title.GetHashCode(),
                        Title = ViewModel.Title,
                        Image = ViewModel.SelectedImage,
                        Tasks = new ObservableCollection<Task>(),
                        SubLists = new ObservableCollection<TodoList>(),
                    });
                }
            }
            else
            {
                if (Root.Value)
                {
                    string oldTitle = SarciniActuale.Title;
                    string oldImage = SarciniActuale.Image;
                    int oldId = SarciniActuale.Id;
                    ObservableCollection<Task> tasks = new ObservableCollection<Task>(SarciniActuale.Tasks);
                    ObservableCollection<TodoList> todoLists = new ObservableCollection<TodoList>(SarciniActuale.SubLists);

                    SarciniActuale.Tasks.Clear();
                    SarciniActuale.Image = ViewModel.SelectedImage;
                    SarciniActuale.Title = ViewModel.Title;
                    SarciniActuale.Id = ViewModel.Title.GetHashCode();
                    SarciniActuale.SubLists.Clear();
                    SarciniActuale.SubLists = new ObservableCollection<TodoList>()
                {
                    new TodoList()
                    {
                        Id = oldId,
                        Title = oldTitle,
                        Image = oldImage,
                        Tasks = tasks,
                        SubLists = todoLists
                    }
                };
                }
                else
                {
                    SarciniActuale.SubLists.Add(new TodoList()
                    {
                        Id = ViewModel.Title.GetHashCode(),
                        Title = ViewModel.Title,
                        Image = ViewModel.SelectedImage,
                        Tasks = new ObservableCollection<Task>(),
                        SubLists = new ObservableCollection<TodoList>(),
                    });

                }
            }
            Window window = this;
            window.Close();
        }
    }
}
