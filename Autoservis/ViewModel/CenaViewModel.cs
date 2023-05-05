using Autoservis.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoservis.ViewModel
{
    public class CenaViewModel
    {
        public static ObservableCollection<Cena> SeznamCenaServisu
        {
            get;
            set;
        }

        public void LoadCena()
        {
            ObservableCollection<Cena> seznamCenaServisu = new ObservableCollection<Cena>();

            seznamCenaServisu.Add(new Cena
            {
                  Polozka = "Kola",
                  CenaPolozky = 5352,
                  IdServisu = 1
              });
            seznamCenaServisu.Add(new Cena
            {
                Polozka = "Kola2",
                CenaPolozky = 5352,
                IdServisu = 1
            });
            seznamCenaServisu.Add(new Cena
            {
                Polozka = "Brzdy",
                CenaPolozky = 6000,
                IdServisu = 2
            });
            seznamCenaServisu.Add(new Cena
            {
                Polozka = "Brzdy2",
                CenaPolozky = 6000,
                IdServisu = 2
            });


            SeznamCenaServisu = seznamCenaServisu;
        }
    }
}

