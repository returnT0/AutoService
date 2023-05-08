using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autoservice.Model;

namespace Autoservice.ViewModel
{
    public class CenaViewModel
    {
        public static ObservableCollection<Price> SeznamCenaServisu
        {
            get;
            set;
        }

        public void LoadCena()
        {
            ObservableCollection<Price> seznamCenaServisu = new ObservableCollection<Price>();
        
            seznamCenaServisu.Add(new Price
            {
                  Polozka = "Kola",
                  CenaPolozky = 5352,
                  IdServisu = 1
              });
            seznamCenaServisu.Add(new Price
            {
                Polozka = "Kola2",
                CenaPolozky = 5352,
                IdServisu = 1
            });
            seznamCenaServisu.Add(new Price
            {
                Polozka = "Brzdy",
                CenaPolozky = 6000,
                IdServisu = 2
            });
            seznamCenaServisu.Add(new Price
            {
                Polozka = "Brzdy2",
                CenaPolozky = 6000,
                IdServisu = 2
            });
        
        
            SeznamCenaServisu = seznamCenaServisu;
        }
    }
}

