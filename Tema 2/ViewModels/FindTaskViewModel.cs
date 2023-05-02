using System.Collections.Generic;
using System.Collections.ObjectModel;
using Tema_2_MVP.Models;

namespace Tema_2_MVP.ViewModels
{
    public class FindTaskViewModel : ViewModelBase
    {
        private string taskTitle;
        public string TaskTitle
        {
            get
            {
                return taskTitle;
            }
            set
            {
                taskTitle = value;
                OnPropertyChanged(nameof(TaskTitle));
            }
        }

        private ObservableCollection<TodoList> data;
        public ObservableCollection<TodoList> Data
        {
            get 
            {
                return data;
            }
            set 
            {
                data = value;
                OnPropertyChanged(nameof(Data));
            }
        }

        private List<Finding> found;
        public List<Finding> Found
        {
            get 
            {
                return found;
            }
            set
            {
                found = value;
                OnPropertyChanged(nameof(Found));
            }
        }

        private string foundText;
        public string FoundText
        {
            get
            {
                return foundText;
            }
            set
            {
                foundText = value;
                OnPropertyChanged(nameof(FoundText));
            }
        }

        public FindTaskViewModel(ObservableCollection<TodoList> data)
        {
            Data = data;
            Found = new List<Finding>();
        }
    }
}
