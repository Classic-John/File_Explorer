using System.Collections.Generic;
using Tema_2_MVP.DataStorage;

namespace Tema_2_MVP.ViewModels
{
    public class ManagingCategoriesViewModel : ViewModelBase
    {
        private List<string> categorii;
        public List<string> Categorii 
        { 
            get
            {
                return categorii;
            }
            set
            {
                categorii = value;
                OnPropertyChanged(nameof(Categorii));
            }
        }

        private string categorieSelectata;
        public string CategorieSelectata 
        {
            get 
            { 
                return categorieSelectata;
            }
            set 
            {
                categorieSelectata = value;
                OnPropertyChanged(nameof(CategorieSelectata));
            }
        }

        public ManagingCategoriesViewModel ()
        {
            Categorii = XMLHelpers.Categorii;
        }
    }
}
