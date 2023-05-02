using System;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace Tema_2_MVP.Models
{
    [Serializable]
    public class TodoList
    {
        [XmlAttribute]
        public int Id { get; set; }
        [XmlAttribute]
        public string Title { get; set; }
        [XmlAttribute]
        public string Image { get; set; }
        [XmlArray]
        public ObservableCollection<TodoList> SubLists { get; set; }
        [XmlArray]
        public ObservableCollection<Task> Tasks { get; set; }

    }
}
