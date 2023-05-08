using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoservice.Model
{
    public class AutoModel
    {
    }

    public class Auto : INotifyPropertyChanged
    {
        private int idVozu;
        private string znackaVozu;
        private string modelVozu;
        private string spz;
        private string vin;
        private string barva;
        private int idKlienta;

        public int IdVozu
        {
            get
            {
                return idVozu;
            }

            set
            {
                if (idVozu != value)
                {
                    idVozu = value;
                    RaisePropertyChanged("IdVozu");
                }
            }
        }
        public string ZnackaVozu
        {
            get
            {
                return znackaVozu;
            }

            set
            {
                if (znackaVozu != value)
                {
                    znackaVozu = value;
                    RaisePropertyChanged("Brand");
                }
            }
        }

        public int IdKlienta
        {
            get
            {
                return idKlienta;
            }

            set
            {
                if (idKlienta != value)
                {
                    idKlienta = value;
                    RaisePropertyChanged("IdKlienta");
                }
            }
        }

        public string ModelVozu
        {
            get { return modelVozu; }

            set
            {
                if (modelVozu != value)
                {
                    modelVozu = value;
                    RaisePropertyChanged("Model");
                }
            }
        }

        public string Spz
        {
            get { return spz; }

            set
            {
                if (spz != value)
                {
                    spz = value;
                    RaisePropertyChanged("Spz");
                }
            }
        }

        public string Vin
        {
            get { return vin; }

            set
            {
                if (vin != value)
                {
                    vin = value;
                    RaisePropertyChanged("Vin");
                }
            }
        }

        public string Barva
        {
            get { return barva; }

            set
            {
                if (barva != value)
                {
                    barva = value;
                    RaisePropertyChanged("Color");
                }
            }
        }

      
        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}
