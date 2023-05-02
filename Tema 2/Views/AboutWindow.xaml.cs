using System.Windows;
using Tema_2_MVP.Commands;

namespace Tema_2_MVP.Views
{
    public partial class AboutWindow : Window
    {
        public AboutWindow(AboutCommandParamters Parameter)
        {
            InitializeComponent();
            Nume.Text = Parameter.Nume;
            Grupa.Text = Parameter.Grup;
        }
    }
}
