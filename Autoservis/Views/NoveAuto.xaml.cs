using Autoservis.Model;
using Autoservis.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Autoservis.Views
{
    /// <summary>
    /// Interaction logic for NoveAuto.xaml
    /// </summary>
    public partial class NoveAuto : Window
    {
        public NoveAuto()
        {
            InitializeComponent();
            if (AutoView.edit)
            {
                znackaVozu.Text = AutoView.auto.ZnackaVozu;
                modelVozu.Text=AutoView.auto.ModelVozu;
                spz.Text = AutoView.auto.Spz;
                vin.Text = AutoView.auto.Vin;
                barva.Text= AutoView.auto.Barva;
                pridat.Content = "Editovat";
            }
             }

        private void pridat_Click(object sender, RoutedEventArgs e)
        {
            Thread threadAdd = new Thread(() =>
            {
                if (AutoView.edit)
            {
                    Dispatcher.Invoke(() => AutoView.auto.ZnackaVozu = znackaVozu.Text);
                    Dispatcher.Invoke(() => AutoView.auto.ModelVozu= modelVozu.Text);
                    Dispatcher.Invoke(() => AutoView.auto.Spz= spz.Text);
                    Dispatcher.Invoke(() => AutoView.auto.Vin= vin.Text);
                    Dispatcher.Invoke(() => AutoView.auto.Barva= barva.Text);
            }
            else
            {
                    Dispatcher.Invoke(() => AutoViewModel.Auta.Add(new Auto
                {
                    IdVozu = Dispatcher.Invoke(() => AutoViewModel.Auta.Count() + 1),
                    ZnackaVozu = Dispatcher.Invoke(() => znackaVozu.Text),
                    ModelVozu = Dispatcher.Invoke(() => modelVozu.Text),
                    Spz = Dispatcher.Invoke(() => spz.Text),
                    Vin = Dispatcher.Invoke(() => vin.Text),
                    Barva = Dispatcher.Invoke(() => barva.Text),
                    IdKlienta = Dispatcher.Invoke(() => (int)ZakaznikView.seznamVybranyZakaznik.First().Id)
                }));
                
            }
                Dispatcher.Invoke(() => NoveAuto.GetWindow(this).Close());
            });
            threadAdd.Start();
        }
    }
}
