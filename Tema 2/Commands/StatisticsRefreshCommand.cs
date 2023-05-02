using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Tema_2_MVP.Models;
using Tema_2_MVP.ViewModels;
using Tema_2_MVP.Views;
using Task = Tema_2_MVP.Models.Task;

namespace Tema_2_MVP.Commands
{
    public class StatisticsRefreshCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private MainViewModel MainView;

        public StatisticsRefreshCommand(MainViewModel mainView)
        {
            MainView = mainView;
        }

        public bool CanExecute(object parameter)
        {
            return MainView != null;
        }

        public void Execute(object parameter)
        {
            ObservableCollection<TodoList> todoLists = MainView.TodoLists;
            List<Task> toateSarcinile = new List<Task>();
            Stack<TodoList> stack = new Stack<TodoList>(todoLists);
            while (stack.Count > 0)
            {
                TodoList ListaSarcini = stack.Pop();
                toateSarcinile.AddRange(ListaSarcini.Tasks);
                if (ListaSarcini.SubLists != null)
                {
                    foreach (TodoList listaSubSarcini in ListaSarcini.SubLists)
                    {
                        stack.Push(listaSubSarcini);
                    }
                }
            }

            MainView.Done = MainView.Tomorrow = MainView.Overdue = MainView.ToBeDone = MainView.Done = 0;

            foreach(Task sarcina in toateSarcinile)
            {
                if(sarcina.Status == Models.TaskStatus.Done)
                {
                    MainView.Done++;
                }
                else
                {
                    MainView.ToBeDone++;
                }

                if(sarcina.Dealine.Date == DateTime.Today.Date)
                {
                    MainView.Today++;
                }

                if(sarcina.Dealine.Date == DateTime.Today.AddDays(1).Date)
                {
                    MainView.Tomorrow++;
                }

                if(sarcina.Dealine.Date < DateTime.Today.Date)
                {
                    MainView.Overdue++;
                }
            }
        }
    }
}
