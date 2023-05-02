using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;
using Tema_2_MVP.Models;

namespace Tema_2_MVP.DataStorage
{
    static class XMLHelpers
    {
        static public List<string> Categorii { get; set; }

        static public List<string> CaiLaImagini { get; set; }

        static XMLHelpers()
        {
            Categorii = DeserializeCategories();
            CaiLaImagini = new List<string>
            {
                "../Images/1.png",
                "../Images/2.png",
                "../Images/3.png",
                "../Images/4.png",
                "../Images/5.png",
                "../Images/6.png",
                "../Images/7.png",
                "../Images/8.png",
                "../Images/9.png",
                "../Images/10.png",

            };

        }

        static public List<string> PopulateCategories()
        {
            return new List<string>()
            {
                "Teme",
                "Facultate",
                "Munca",
                "Acasa",
                "Evenimente",
                "Intalniri",
                "Speciale",
                "Ocazionale",
                "Altele"
            };
        }

        static public void SerializeCategories(List<string> categorii)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<string>));
            using (var writer = new StreamWriter("Categorii.xml"))
            {
                xmlSerializer.Serialize(writer, categorii);
            }
        }

        static public List<string> DeserializeCategories()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<string>));
            List<string> categories = new List<string>();
            using (var reader = new StreamReader("Categorii.xml"))
            {
                categories = (List<string>)xmlSerializer.Deserialize(reader);
            }
            return categories;
        }

        static public void Serialize(ObservableCollection<TodoList> todoLists)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ObservableCollection<TodoList>));
            using (var writer = new StreamWriter("Grupuri.xml"))
            {
                xmlSerializer.Serialize(writer, todoLists);
            }
        }

        static public ObservableCollection<TodoList> Deserialize()
        {
            XmlSerializer xmlSerializer = new XmlSerializer (typeof(ObservableCollection<TodoList>));
            ObservableCollection<TodoList> allData = new ObservableCollection<TodoList>();
            using (var reader = new StreamReader("Grupuri.xml"))
            {
                allData = (ObservableCollection<TodoList>) xmlSerializer.Deserialize(reader);
            }

            return allData;
        }
    }
}
