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
        public Arquivo()
        {
            this.seleciona = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public string Caminho { get; set; }
        public string TextoOriginal { get; set; }

        public string TextoSimplificado { get; set; }

        
        private bool seleciona;  
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

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
