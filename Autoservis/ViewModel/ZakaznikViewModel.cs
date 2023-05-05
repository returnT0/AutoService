using Autoservis.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoservis.ViewModel
{
    public class ZakaznikViewModel
    {
        public static ObservableCollection<Zakaznik> Zakaznici
        {
            get;
            set;
        }

        public void LoadStudents()
        {
            ObservableCollection<Zakaznik> zakaznici = new ObservableCollection<Zakaznik>();

            zakaznici.Add(new Zakaznik
            {
                Jmeno = "Mark",
                Prijmeni = "Allain",
                Telefon = "789456123",
                Email = "xxx@yyy.cz",
                Adresa = "bbdbd 12, dbdd, 12345",
                Poznamky = "xxxxxxxxxx",
                Id = 1
            }); 
            zakaznici.Add(new Zakaznik
            {
                Jmeno = "Allen",
                Prijmeni = "Brown",
                Telefon = "555666999",
                Email = "ddd@yyy.cz",
                Adresa = "bbdbd 12, dbdd, 12345",
                Poznamky = "xxxxxxxxxx",
                Id = 2
            }) ;

            Zakaznici = zakaznici;
        }
    }
}

