using System;
using System.Collections.ObjectModel;
using Tema_2_MVP.DataStorage;
using Tema_2_MVP.Models;
using Task = Tema_2_MVP.Models.Task;
using TaskStatus = Tema_2_MVP.Models.TaskStatus;

namespace Tema_2_MVP.ViewModels
{
    public class AddingTaskViewModel
    {
        public string TitleText { get; set; }

        public string DescriptionText { get; set; }

        public ObservableCollection<string> Categories { get; set; }
        public int CategoryIndex { get; set; }

        public ObservableCollection<Priority> Priorities { get; set; }
        public int PriorityIndex { get; set; }

        public ObservableCollection<Models.TaskStatus> Status { get; set; }
        public int StatusIndex { get; set; }

        public DateTime Deadline { get; set; }

        public TodoList CurrentTodoList { get; set; }

        public AddingTaskViewModel(TodoList listaCurenta)
        {
            CurrentTodoList = listaCurenta;
            Deadline = DateTime.Now.AddDays(1);
            Priorities = new ObservableCollection<Priority>()
            {
                Priority.Low,
                Priority.Medium,
                Priority.High
            };
            Status = new ObservableCollection<Models.TaskStatus>()
            {
                TaskStatus.Created,
                TaskStatus.Ongoing,
                TaskStatus.Done
            };
            Categories = new ObservableCollection<string>(XMLHelpers.Categorii);

        }

        public AddingTaskViewModel(Task sarcina)
        {
            Priorities = new ObservableCollection<Priority>()
            {
                Priority.Low,
                Priority.Medium,
                Priority.High
            };
            Status = new ObservableCollection<Models.TaskStatus>()
            {
                TaskStatus.Created,
                TaskStatus.Ongoing,
                TaskStatus.Done
            };
            Categories = new ObservableCollection<string>(XMLHelpers.Categorii);

            TitleText = sarcina.Title;
            DescriptionText = sarcina.Description;
            PriorityIndex = Priorities.IndexOf(sarcina.Priority);
            StatusIndex = Status.IndexOf(sarcina.Status);
            CategoryIndex = Categories.IndexOf(sarcina.Category);
            Deadline = sarcina.Dealine;
        }
    }
}
