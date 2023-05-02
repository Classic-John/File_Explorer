using System.Windows;
using System.Windows.Data;
using Tema_2_MVP.DataStorage;
using Tema_2_MVP.ViewModels;

namespace Tema_2_MVP.Views
{
    public partial class ManagingCategoriesWindow : Window
    {
        public ManagingCategoriesViewModel ViewModel { get; set; }

        public ManagingCategoriesWindow()
        {
            InitializeComponent();
            ViewModel = new ManagingCategoriesViewModel();
            DataContext = ViewModel;
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (newCategory.Text != string.Empty)
            {
                ViewModel.Categorii.Add(newCategory.Text);
                var view = CollectionViewSource.GetDefaultView(ViewModel.Categorii);
                view.Refresh();
            }
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.CategorieSelectata != null)
            {
                ViewModel.Categorii.Remove(ViewModel.CategorieSelectata);
                var view = CollectionViewSource.GetDefaultView(ViewModel.Categorii);
                view.Refresh();
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Window Window = this;
            XMLHelpers.SerializeCategories(ViewModel.Categorii);
            Close();
        }
    }
}
