using System.Collections.Generic;
using Tema_2_MVP.DataStorage;

namespace Tema_2_MVP.ViewModels
{
    public class AddingTodoListViewModel : ViewModelBase
    {
        private string title;
        public string Title 
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        private string selectedImage;
        public string SelectedImage
        {
            get
            {
                return selectedImage;
            }
            set
            {
                selectedImage = value;
                OnPropertyChanged(nameof(SelectedImage));
            }
        }
        private List<string> imagePaths;
        public List<string> ImagePaths
        {
            get 
            {
                return imagePaths;
            }
            set
            {
                imagePaths = value;
                OnPropertyChanged(nameof(ImagePaths));
            }
        }

        public AddingTodoListViewModel()
        {
            imagePaths = XMLHelpers.CaiLaImagini;
        }
    }
}
