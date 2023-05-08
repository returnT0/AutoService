using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoservice.Model
{
    public class CenaModel
    {
    }

    public class Price : INotifyPropertyChanged
    {
        private string polozka;
        private int cena;
        private int idServis;
        public event PropertyChangedEventHandler PropertyChanged;


        public int IdServisu
        {
            get
            {
                return idServis;
            }

            set
            {
                if (idServis != value)
                {
                    idServis = value;
                    RaisePropertyChanged("IdServisu");
                }
            }
        }
        public string Polozka
        {
            get
            {
                return polozka;
            }

            set
            {
                if (polozka != value)
                {
                    polozka = value;
                    RaisePropertyChanged("Polozka");
                }
            }
        }

        public int CenaPolozky
        {
            get
            {
                return cena;
            }

            set
            {
                if (cena != value)
                {
                    cena = value;
                    RaisePropertyChanged("PricePolozky");
                }
            }
        }


        
        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }


    }


}