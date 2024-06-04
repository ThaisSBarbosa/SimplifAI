using SimplifAI.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace SimplifAI.ViewModels
{
    public class StartViewModel : BaseViewModel
    {
        public ObservableCollection<Carousel> Items { get; set; }

        public StartViewModel()
        {
            Items = new ObservableCollection<Carousel>
        {
            new Carousel { Title = "Página 1", Description = "Descrição da página 1", ImageUrl = "image1.png" },
            new Carousel { Title = "Página 2", Description = "Descrição da página 2", ImageUrl = "image2.png" },
            new Carousel { Title = "Página 3", Description = "Descrição da página 3", ImageUrl = "image3.png" }
        };
        }

    }
}