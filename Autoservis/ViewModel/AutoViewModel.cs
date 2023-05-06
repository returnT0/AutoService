
using Autoservis.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoservis.ViewModel
{
    internal class AutoViewModel
    {
        public static ObservableCollection<Auto> Auta
        {
            get;
            set;
        }

        public void LoadAuta()
        {
            ObservableCollection<Auto> seznamAut = new ObservableCollection<Auto>();
        
            seznamAut.Add(new Auto
            {
                IdVozu = 1,
                ZnackaVozu = "Ferrari",
                ModelVozu = "418",
                Spz = "000FF",
                Vin = "1DFC503DC0BBD7BE5",
                Barva = "Red",
                IdKlienta = 1
            });
            seznamAut.Add(new Auto
            {
                IdVozu = 2,
                ZnackaVozu = "Porche",
                ModelVozu = "GT3",
                Spz = "HE32DF",
                Vin = "402F137E3C35FBB0F",
                Barva = "Black",
                IdKlienta = 2
                
            });
            Auta = seznamAut;
        }
    }
}
