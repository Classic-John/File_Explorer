using System.Windows;
using Tema_2_MVP.Models;
using Tema_2_MVP.ViewModels;
using Task = Tema_2_MVP.Models.Task;

namespace Tema_2_MVP.Views
{
    public partial class AddTaskWindow : Window
    {
        public Task Task { get; set; }

        public Task CurrentTask { get; set; }

        public TodoList CurrentTodoList { get; set; }

        public AddingTaskViewModel CurrentTaskViewModel { get; set; }

        public AddTaskWindow(TodoList ListaActuala)
        {
            InitializeComponent();
            CurrentTaskViewModel = new AddingTaskViewModel(ListaActuala);
            DataContext = CurrentTaskViewModel;
            CurrentTodoList = ListaActuala;
        }

        public AddTaskWindow(Task sarcinaActuala)
        {
            InitializeComponent();
            CurrentTask = sarcinaActuala; 
            CurrentTaskViewModel = new AddingTaskViewModel(sarcinaActuala);
            DataContext = CurrentTaskViewModel;
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentTaskViewModel.TitleText != string.Empty ||
                CurrentTaskViewModel.DescriptionText != string.Empty)
            {
                Task SarcinaNoua = new Task()
                {
                    Title = CurrentTaskViewModel.TitleText,
                    Description = CurrentTaskViewModel.DescriptionText,
                    Category = CurrentTaskViewModel.Categories[CurrentTaskViewModel.CategoryIndex],
                    Priority = CurrentTaskViewModel.Priorities[CurrentTaskViewModel.PriorityIndex],
                    Status = CurrentTaskViewModel.Status[CurrentTaskViewModel.StatusIndex],
                    Dealine = CurrentTaskViewModel.Deadline
                };
                Task = SarcinaNoua;

                if(CurrentTodoList != null)
                {
                    CurrentTodoList.Tasks.Add(SarcinaNoua);
                }
                else
                {
                    CurrentTask.Title = Task.Title;
                    CurrentTask.Description = Task.Description;
                    CurrentTask.Status = Task.Status;
                    CurrentTask.Priority =Task.Priority;
                    CurrentTask.Category =Task.Category;
                    CurrentTask.Dealine = Task.Dealine;
                }
                Window window = this;
                window.Close();
            }
            Task = null;
        }
    }
}
