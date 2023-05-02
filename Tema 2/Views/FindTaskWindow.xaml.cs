using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Data;
using Tema_2_MVP.Models;
using Tema_2_MVP.ViewModels;
using Task = Tema_2_MVP.Models.Task;

namespace Tema_2_MVP.Views
{
    /// <summary>
    /// Interaction logic for FindTaskWindow.xaml
    /// </summary>
    public partial class FindTaskWindow : Window
    {
        public ObservableCollection<TodoList> Data { get; set; }

        public FindTaskViewModel ViewModel { get; set; }

        public FindTaskWindow(ObservableCollection<TodoList> data)
        {
            InitializeComponent();
            Data = data;
            ViewModel = new FindTaskViewModel(data);
            DataContext = ViewModel;
        }

        private void Find_Click(object sender, RoutedEventArgs e)
        {
            if(ViewModel.TaskTitle != string.Empty)
            {
                if(ViewModel.Found.Count > 0)
                {
                    ViewModel.Found.Clear();
                }

                ObservableCollection<TodoList> liste = ViewModel.Data;
                List<Task> toateSarcinile = new List<Task>();
                List<TodoList> toateListele = new List<TodoList>();
                Stack<TodoList> stack = new Stack<TodoList>(liste);
                while (stack.Count > 0)
                {
                    TodoList todoList = stack.Pop();
                    toateListele.Add(todoList);
                    toateSarcinile.AddRange(todoList.Tasks);

                    if (todoList.SubLists != null)
                    {
                        foreach (TodoList subList in todoList.SubLists)
                        {
                            stack.Push(subList);
                        }
                    }
                }

                List<Task> auxList = toateSarcinile.FindAll(x => x.Title.ToLower().Contains(ViewModel.TaskTitle.ToLower()));
                foreach(TodoList ListaSarcini in toateListele)
                {
                    foreach(Task sarcina in auxList)
                    {
                        if(ListaSarcini.Tasks.Contains(sarcina))
                        {
                            ViewModel.Found.Add(new Finding()
                            {
                                Task = sarcina,
                                TaskParentImage = ListaSarcini.Image,
                                TaskParentTitle = ListaSarcini.Title
                            }
                            );
                        }
                    }
                }
                var view = CollectionViewSource.GetDefaultView(ViewModel.Found);
                view.Refresh();
                ViewModel.FoundText = ViewModel.Found.Count.ToString() + " lucruri gasite.";
            }
            else
            {
                ViewModel.FoundText = "Nimic gasit";
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Window window = this;
            Close();
        }
    }
}
