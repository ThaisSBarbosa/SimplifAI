using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplifAI.Models
{
    public class Arquivo : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string caminho { get; set; }
        private bool apaga;
        private bool seleciona;

        public bool Apaga
        {
            get { return apaga; }
            set
            {
                if (apaga != value)
                {
                    apaga = value;
                    OnPropertyChanged(nameof(Apaga));
                }
            }
        }
        public bool Seleciona
        {
            get { return seleciona; }
            set
            {
                if (seleciona != value)
                {
                    seleciona = value;
                    OnPropertyChanged(nameof(Seleciona));
                }
            }
        }
        public Arquivo()
        {
            this.apaga = false;
            this.seleciona = false;
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
