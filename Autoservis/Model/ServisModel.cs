using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Autoservis.Model
{
    public class ServisModel 
    {
    }

    public class Servis : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private int idServis;
        private int idAuto;
        private string zavada;
        private string datumServiu;
        private int tachometr;
        private string plnostNadrze;
        private Cena? cena;

        public int IdServis
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
                    RaisePropertyChanged("IdServis");
                }
            }
        }

        public int IdAuto
        {
            get
            {
                return idAuto;
            }

            set
            {
                if (idAuto != value)
                {
                    idAuto = value;
                    RaisePropertyChanged("IdAuto");
                }
            }
        }
        public string Zavada
        {
            get
            {
                return zavada;
            }

            set
            {
                if (zavada != value)
                {
                    zavada = value;
                    RaisePropertyChanged("Zavada");
                }
            }
        }

        public string DatumServisu
        {
            get
            {
                return datumServiu;
            }

            set
            {
                if (datumServiu != value)
                {
                    datumServiu = value;
                    RaisePropertyChanged("DatumServisu");
                }
            }
        }

        public int Tachometr
        {
            get
            {
                return tachometr;
            }

            set
            {
                if (tachometr != value)
                {
                    tachometr = value;
                    RaisePropertyChanged("Tachometr");
                }
            }
        }

        public string PlnostNadrze
        {
            get
            {
                return plnostNadrze;
            }

            set
            {
                if (plnostNadrze != value)
                {
                    plnostNadrze = value;
                    RaisePropertyChanged("PlnostNadrze");
                }
            }
        }

        public Cena CenaPolozky
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
                    RaisePropertyChanged("CenaPolozky");
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

        protected bool SetProperty<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (!Equals(field, newValue))
            {
                field = newValue;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                return true;
            }

            return false;
        }

        private System.Collections.IEnumerable cena1;

        public System.Collections.IEnumerable Cena { get => cena1; set => SetProperty(ref cena1, value); }
    }
}
