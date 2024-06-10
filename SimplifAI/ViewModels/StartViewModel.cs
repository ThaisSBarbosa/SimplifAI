using SimplifAI.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace SimplifAI.ViewModels
{
    public class StartViewModel : BaseViewModel
    {
        public StartViewModel()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;

        private bool _aceitou = false;

        public bool Aceitou
        {
            get => _aceitou;
            set
            {
                if (_aceitou == value) return;
                _aceitou = value;
                OnPropertyChanged();
               // PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Aceitou)));
            }
        }




    }
}