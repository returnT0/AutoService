using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoservis.Model
{
    public class ZakaznikModel { }

    public class Zakaznik : INotifyPropertyChanged
    {
        private int id;
        private string jmeno;
        private string prijmeni;
        private string telefon;
        private string email;
        private string adresa;
        private string? poznamky;
        private ObservableCollection<Auto>? auta;

        public Zakaznik()
        {
        }

        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                if (id != value)
                {
                    id = value;
                    RaisePropertyChanged("Id");
                }
            }
        }


        public string Jmeno
        {
            get
            {
                return jmeno;
            }

            set
            {
                if (jmeno != value)
                {
                    jmeno = value;
                    RaisePropertyChanged("Jmeno");
                }
            }
        }

        public string Prijmeni
        {
            get { return prijmeni; }

            set
            {
                if (prijmeni != value)
                {
                    prijmeni = value;
                    RaisePropertyChanged("LastName");
                }
            }
        }

        public string Telefon
        {
            get { return telefon; }

            set
            {
                if (telefon != value)
                {
                    telefon = value;
                    RaisePropertyChanged("Telefon");
                }
            }
        }

        public string Email
        {
            get { return email; }

            set
            {
                if (email != value)
                {
                    email = value;
                    RaisePropertyChanged("Email");
                }
            }
        }

        public string Adresa
        {
            get { return adresa; }

            set
            {
                if (adresa != value)
                {
                    adresa = value;
                    RaisePropertyChanged("Adresa");
                }
            }
        }

        public string Poznamky
        {
            get { return poznamky; }

            set
            {
                if (poznamky != value)
                {
                    poznamky = value;
                    RaisePropertyChanged("Poznamky");
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
