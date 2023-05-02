using System.Windows;
using Tema_2_MVP.ViewModels;

namespace Tema_2_MVP.Views
{
    public partial class PickCategory : Window
    {
        public ManagingCategoriesViewModel ViewModel { get; set; }

        public MainViewModel MainView { get; set; }

        public PickCategory(MainViewModel mainView)
        {
            InitializeComponent();
            ViewModel = new ManagingCategoriesViewModel();
            DataContext = ViewModel;
            MainView = mainView;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainView.Category = ViewModel.CategorieSelectata;
            Window window = this;
            window.Close();
        }
    }
}
