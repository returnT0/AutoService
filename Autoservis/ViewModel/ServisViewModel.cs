using Autoservis.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoservis.ViewModel
{
    public class ServisViewModel
    {
        public static ObservableCollection<Servis> SeznamServisu
        {
            get;
            set;
        }

        public void LoadSeznamServisu()
        {
            ObservableCollection<Servis> seznamServisu = new ObservableCollection<Servis>();

            seznamServisu.Add(new Servis
            {
                IdServis = 1,
                IdAuto = 1,
                Zavada = "xxxxx",
                DatumServisu = "05/29/2022",
                Tachometr = 235412,
                PlnostNadrze = "3/4",
                CenaPolozky = CenaViewModel.SeznamCenaServisu[0]
            }) ;

            seznamServisu.Add(new Servis
            {
                IdServis = 2,
                IdAuto = 2,
                Zavada = "yyyyy",
                DatumServisu = "05/29/2020",
                Tachometr = 222144,
                PlnostNadrze = "1/2",
                CenaPolozky = CenaViewModel.SeznamCenaServisu[1]
            });
            

            SeznamServisu = seznamServisu;
        }
    }
}

