using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autoservice.Model;

namespace Autoservice.ViewModel
{
    public class ServisViewModel
    {
        public static ObservableCollection<Service> SeznamServisu
        {
            get;
            set;
        }

        public void LoadSeznamServisu()
        {
            ObservableCollection<Service> seznamServisu = new ObservableCollection<Service>();

            seznamServisu.Add(new Service
            {
                IdServis = 1,
                IdAuto = 1,
                Zavada = "xxxxx",
                DatumServisu = "05/29/2022",
                Tachometr = 235412,
                PlnostNadrze = "3/4",
                PricePolozky = CenaViewModel.SeznamCenaServisu[0]
            }) ;

            seznamServisu.Add(new Service
            {
                IdServis = 2,
                IdAuto = 2,
                Zavada = "yyyyy",
                DatumServisu = "05/29/2020",
                Tachometr = 222144,
                PlnostNadrze = "1/2",
                PricePolozky = CenaViewModel.SeznamCenaServisu[1]
            });
            

            SeznamServisu = seznamServisu;
        }
    }
}

