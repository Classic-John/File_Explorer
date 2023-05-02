using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Tema_2_MVP.DataStorage;
using Tema_2_MVP.Models;
using Tema_2_MVP.ViewModels;
using Task = Tema_2_MVP.Models.Task;
using TaskStatus = Tema_2_MVP.Models.TaskStatus;

namespace Tema_2_MVP.Views
{
    public partial class MainView : UserControl
    {
        MainViewModel mainView;

        public MainView()
        {
            InitializeComponent();
            mainView = new MainViewModel();
            DataContext = mainView;
            mainView.TodoLists=new ObservableCollection<TodoList>();
        }

        private void TreeTask_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            TodoList listaSarcini = e.NewValue as TodoList;
            if (listaSarcini != null)
            {
                mainView.SelectedTodoList = listaSarcini;
            }
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                Task sarcina = e.AddedItems[0] as Task;
                if (sarcina != null)
                {
                    mainView.SelectedTask = sarcina;
                }
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            Task sarcina = checkBox.DataContext as Task;
            sarcina.Status = Models.TaskStatus.Done;

            var view = CollectionViewSource.GetDefaultView(mainView.SelectedTodoList.Tasks);
            view.Refresh();
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            mainView.TodoLists = XMLHelpers.Deserialize();
            ICommand comanda = mainView.StatisticsCommand;

            if (comanda.CanExecute(0))
            {
                comanda.Execute(0);
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Salvezi?", "Se salveaza...", MessageBoxButton.OKCancel);

            if (result == MessageBoxResult.OK)
            {
                XMLHelpers.Serialize(mainView.TodoLists);
            }

        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            if (mainView.TodoLists != null)
            {
                MessageBoxResult deznodamant = MessageBox.Show("Vrei sa stergi tot? E pe barba ta", "Stergere...", MessageBoxButton.OKCancel);

                if (deznodamant == MessageBoxResult.OK)
                {
                    mainView.TodoLists.Clear();
                    mainView.TodoLists = new ObservableCollection<TodoList>();
                    MessageBox.Show("Trebuie sa ai macar o salvare pentru a putea sterge actiunile curente", "...");
                }

            }
            else
            {
                MessageBox.Show("Nu exista liste selectate", "...");
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            if (mainView.TodoLists != null)
            {
                Save_Click(sender, e);

            }
            Environment.Exit(0);
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            if (mainView.SelectedTodoList != null)
            {

                AddTaskWindow addTaskWindow = new AddTaskWindow(mainView.SelectedTodoList);
                while ((bool)addTaskWindow.ShowDialog())
                {
                }

            }
            else
            {
                MessageBox.Show("Nicio lista de sarcini selectata");
            }
        }

        private void ManageCategories_Click(object sender, RoutedEventArgs e)
        {
            var schimbaSarcini = new ManagingCategoriesWindow();
            schimbaSarcini.Show();
        }

        private void EditTask_Click(object sender, RoutedEventArgs e)
        {
            if (mainView.SelectedTask != null)
            {

                AddTaskWindow addTaskWindow = new AddTaskWindow(mainView.SelectedTask);
                while ((bool)addTaskWindow.ShowDialog())
                {
                }
                var view = CollectionViewSource.GetDefaultView(mainView.SelectedTodoList.Tasks);
                view.Refresh();
                mainView.SelectedTask = mainView.SelectedTask;
            }
            else
            {
                MessageBox.Show("Nicio lista de sarcini selectata");
            }
        }

        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            if (mainView.SelectedTask != null)
            {
                MessageBoxResult rezultat = MessageBox.Show("Sigur vrei sa stergi sarcina?", "Stergere...", MessageBoxButton.OKCancel);

                if (rezultat == MessageBoxResult.OK)
                {
                    mainView.SelectedTodoList.Tasks.Remove(mainView.SelectedTask);
                }
            }
            else
            {
                MessageBox.Show("Nu se poate amice.", "...");
            }
        }

        private void SetDoneTask_Click(object sender, RoutedEventArgs e)
        {
            if (mainView.SelectedTask != null)
            {
                mainView.SelectedTask.Status = TaskStatus.Done;

                var view = CollectionViewSource.GetDefaultView(mainView.SelectedTodoList.Tasks);
                view.Refresh();
            }
            else
            {
                MessageBox.Show("Ai terminat ceva ce nu exista? Impresionant", "Notita");
            }
        }

        private void MoveTaskUp_Click(object sender, RoutedEventArgs e)
        {
            if (mainView.SelectedTask != null)
            {
                if (mainView.SelectedTodoList.Tasks.Count >= 2 && mainView.SelectedTodoList.Tasks.IndexOf(mainView.SelectedTask) >= 1)
                {
                    int indiceCurent = mainView.SelectedTodoList.Tasks.IndexOf(mainView.SelectedTask);
                    var aux = mainView.SelectedTodoList.Tasks[indiceCurent - 1];
                    mainView.SelectedTodoList.Tasks[indiceCurent - 1] = mainView.SelectedTask;
                    mainView.SelectedTodoList.Tasks[indiceCurent] = aux;
                }
            }
        }

        private void MoveTaskDown_Click(object sender, RoutedEventArgs e)
        {
            if (mainView.SelectedTask != null)
            {
                if (mainView.SelectedTodoList.Tasks.Count >= 2 && mainView.SelectedTodoList.Tasks.IndexOf(mainView.SelectedTask) < mainView.SelectedTodoList.Tasks.Count - 1)
                {
                    int indiceCurent = mainView.SelectedTodoList.Tasks.IndexOf(mainView.SelectedTask);
                    var aux = mainView.SelectedTodoList.Tasks[indiceCurent + 1];
                    mainView.SelectedTodoList.Tasks[indiceCurent + 1] = mainView.SelectedTask;
                    mainView.SelectedTodoList.Tasks[indiceCurent] = aux;
                }
            }
        }

        private void FindTask_Click(object sender, RoutedEventArgs e)
        {
            FindTaskWindow findTaskWindow = new FindTaskWindow(mainView.TodoLists);
            findTaskWindow.ShowDialog();
        }

        private void SortPriority_Click(object sender, RoutedEventArgs e)
        {
            if (mainView.SelectedTodoList != null)
            {
                var sarciniSortate = mainView.SelectedTodoList.Tasks.OrderBy(x =>
                {
                    if (x.Priority == Priority.Low)
                    {
                        return -1;
                    }
                    else if (x.Priority == Priority.High)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                });

                var sortedObservableTasks = new ObservableCollection<Task>(sarciniSortate);
                mainView.SelectedTodoList.Tasks.Clear();
                foreach (var sarcini in sortedObservableTasks)
                {
                    mainView.SelectedTodoList.Tasks.Add(sarcini);
                }
                var view = CollectionViewSource.GetDefaultView(mainView.SelectedTodoList.Tasks);
                view.Refresh();
            }
        }
        private void SortDealine_Click(object sender, RoutedEventArgs e)
        {
            if (mainView.SelectedTodoList != null)
            {
                var sarciniSortate = mainView.SelectedTodoList.Tasks.OrderBy(x =>
                {
                    return x.Dealine;
                });

                var sortedObservableTasks = new ObservableCollection<Task>(sarciniSortate);
                mainView.SelectedTodoList.Tasks.Clear();
                foreach (var sarcini in sortedObservableTasks)
                {
                    mainView.SelectedTodoList.Tasks.Add(sarcini);
                }
                var view = CollectionViewSource.GetDefaultView(mainView.SelectedTodoList.Tasks);
                view.Refresh();
            }
        }
        private void FilterDone_Click(object sender, RoutedEventArgs e)
        {
            if (mainView.SelectedTodoList != null)
            {
                var original = new ObservableCollection<Task>();

                foreach (var sarcina in mainView.SelectedTodoList.Tasks)
                {
                    original.Add(sarcina);
                }
                var filtrate = mainView.SelectedTodoList.Tasks.Where(x => x.Status == TaskStatus.Done);

                var filteredObservableTasks = new ObservableCollection<Task>(filtrate);
                mainView.SelectedTodoList.Tasks.Clear();
                foreach (var sarcina in filteredObservableTasks)
                {
                    mainView.SelectedTodoList.Tasks.Add(sarcina);
                }
                var view = CollectionViewSource.GetDefaultView(mainView.SelectedTodoList.Tasks);
                view.Refresh();
                mainView.SelectedTodoList.Tasks = original;
            }
        }

        private void FilterOverdue_Click(object sender, RoutedEventArgs e)
        {
            if (mainView.SelectedTodoList != null)
            {
                var original = new ObservableCollection<Task>();

                foreach (var sarcina in mainView.SelectedTodoList.Tasks)
                {
                    original.Add(sarcina);
                }
                var filteredTasks = mainView.SelectedTodoList.Tasks.Where(x => x.Dealine.Date < DateTime.Today.Date);

                var filteredObservableTasks = new ObservableCollection<Task>(filteredTasks);
                mainView.SelectedTodoList.Tasks.Clear();
                foreach (var sarcina in filteredObservableTasks)
                {
                    mainView.SelectedTodoList.Tasks.Add(sarcina);
                }
                var view = CollectionViewSource.GetDefaultView(mainView.SelectedTodoList.Tasks);
                view.Refresh();
                mainView.SelectedTodoList.Tasks = original;
            }
        }

        private void FilterUndone_Click(object sender, RoutedEventArgs e)
        {
            if (mainView.SelectedTodoList != null)
            {
                var original = new ObservableCollection<Task>();

                foreach (var sarcina in mainView.SelectedTodoList.Tasks)
                {
                    original.Add(sarcina);
                }
                var filtrate = mainView.SelectedTodoList.Tasks.Where(
                    x => x.Dealine.Date < DateTime.Today.Date && x.Status != TaskStatus.Done);

                var filteredObservableTasks = new ObservableCollection<Task>(filtrate);
                mainView.SelectedTodoList.Tasks.Clear();
                foreach (var task in filteredObservableTasks)
                {
                    mainView.SelectedTodoList.Tasks.Add(task);
                }
                var view = CollectionViewSource.GetDefaultView(mainView.SelectedTodoList.Tasks);
                view.Refresh();
                mainView.SelectedTodoList.Tasks = original;
            }
        }

        private void FilterFuture_Click(object sender, RoutedEventArgs e)
        {
            if (mainView.SelectedTodoList != null)
            {
                var original = new ObservableCollection<Task>();

                foreach (var sarcini in mainView.SelectedTodoList.Tasks)
                {
                    original.Add(sarcini);
                }
                var filtrate = mainView.SelectedTodoList.Tasks.Where(
                    x => x.Dealine.Date > DateTime.Today.Date && x.Status != TaskStatus.Done);

                var filteredObservableTasks = new ObservableCollection<Task>(filtrate);
                mainView.SelectedTodoList.Tasks.Clear();
                foreach (var sarcini in filteredObservableTasks)
                {
                    mainView.SelectedTodoList.Tasks.Add(sarcini);
                }
                var view = CollectionViewSource.GetDefaultView(mainView.SelectedTodoList.Tasks);
                view.Refresh();
                mainView.SelectedTodoList.Tasks = original;
            }
        }
        private void TDLDelete_Click(object sender, RoutedEventArgs e)
        {
            if (mainView.SelectedTodoList != null)
            {
                MessageBoxResult deznodamant = MessageBox.Show("Stergi lista?", "Stergere...", MessageBoxButton.OKCancel);

                if (deznodamant == MessageBoxResult.OK)
                {
                    mainView.TodoLists.Remove(mainView.SelectedTodoList);
                }
            }
            else
            {
                MessageBox.Show("Nu merge amice", "Note");
            }
        }

        private void TDLMoveUp_Click(object sender, RoutedEventArgs e)
        {
            if (mainView.SelectedTodoList != null)
            {
                if (mainView.TodoLists.Count >= 2 && mainView.TodoLists.IndexOf(mainView.SelectedTodoList) >= 1)
                {
                    int indiceCurent = mainView.TodoLists.IndexOf(mainView.SelectedTodoList);
                    var aux = mainView.TodoLists[indiceCurent - 1];
                    mainView.TodoLists[indiceCurent - 1] = mainView.SelectedTodoList;
                    mainView.TodoLists[indiceCurent] = aux;
                }
            }
        }

        private void TDLMoveDown_Click(object sender, RoutedEventArgs e)
        {
            if (mainView.SelectedTodoList != null)
            {
                if (mainView.TodoLists.Count >= 2 && mainView.TodoLists.IndexOf(mainView.SelectedTodoList) <= mainView.TodoLists.Count - 1)
                {
                    int indiceCurent = mainView.TodoLists.IndexOf(mainView.SelectedTodoList);
                    var aux = mainView.TodoLists[indiceCurent + 1];
                    mainView.TodoLists[indiceCurent + 1] = mainView.SelectedTodoList;
                    mainView.TodoLists[indiceCurent] = aux;
                }
            }
        }

        private void TDLAddRoot_Click(object sender, RoutedEventArgs e)
        {
            if (mainView.SelectedTodoList != null)
            {
                AddTodoListWindow adaugaListaSarcini = new AddTodoListWindow(mainView.SelectedTodoList, true);
                while ((bool)adaugaListaSarcini.ShowDialog())
                {
                }
                if (mainView.TodoLists != null)
                {
                    var view = CollectionViewSource.GetDefaultView(mainView.TodoLists);
                    view.Refresh();
                }

            }
            else
            {
                AddTodoListWindow adaugaListaSarcini = new AddTodoListWindow(mainView.TodoLists);
                while ((bool)adaugaListaSarcini.ShowDialog())
                {
                }
                if (mainView.TodoLists != null)
                {
                    var view = CollectionViewSource.GetDefaultView(mainView.TodoLists);
                    view.Refresh();
                }

            }
        }

        private void TDLAddSubList_Click(object sender, RoutedEventArgs e)
        {
            if (mainView.SelectedTodoList != null)
            {
                AddTodoListWindow adaugaListaSarcini = new AddTodoListWindow(mainView.SelectedTodoList, false);
                while ((bool)adaugaListaSarcini.ShowDialog())
                {
                }
                var view = CollectionViewSource.GetDefaultView(mainView.TodoLists);
                view.Refresh();
            }
            else
            {
                MessageBox.Show("Nu merge amice", "Note");
            }
        }

        private void TDLEdit_Click(object sender, RoutedEventArgs e)
        {
            if (mainView.SelectedTodoList != null)
            {

                AddTodoListWindow adaugaListaSarcini = new AddTodoListWindow(mainView.SelectedTodoList);
                while ((bool)adaugaListaSarcini.ShowDialog())
                {
                }
                var view = CollectionViewSource.GetDefaultView(mainView.TodoLists);
                view.Refresh();
            }
            else
            {
                MessageBox.Show("Nu se poate amice", "Notita");
            }
        }

        private void FilterGroup_Click(object sender, RoutedEventArgs e)
        {
            if (mainView.SelectedTodoList != null)
            {
                if (mainView.SelectedTodoList != null)
                {
                    PickCategory alegeCategoria = new PickCategory(mainView);
                    while ((bool)alegeCategoria.ShowDialog())
                    {
                    }
                    var original = new ObservableCollection<Task>();
                    foreach (var sarcina in mainView.SelectedTodoList.Tasks)
                    {
                        original.Add(sarcina);
                    }
                    var filtrate = mainView.SelectedTodoList.Tasks.Where(x => x.Category == mainView.Category);
                    var filteredObservableTasks = new ObservableCollection<Task>(filtrate);
                    mainView.SelectedTodoList.Tasks.Clear();
                    foreach (var sarcini in filteredObservableTasks)
                    {
                        mainView.SelectedTodoList.Tasks.Add(sarcini);
                    }
                    var view = CollectionViewSource.GetDefaultView(mainView.SelectedTodoList.Tasks);
                    view.Refresh();
                    mainView.SelectedTodoList.Tasks = original;

                }
                else
                {
                    MessageBox.Show("Ai nevoie de o lista selectata.");
                }

            }
        }
    }
}
