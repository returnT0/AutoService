using Autoservis.Model;
using Autoservis.ViewModel;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for NovyKlient.xaml
    /// </summary>
    public partial class NovyKlient : Window
    {
        public NovyKlient()
        {
            InitializeComponent();
            Thread threadAdd = new Thread(() =>
            {
                if (ZakaznikView.edit && ZakaznikView.zakaznik != null )
                {
                    Dispatcher.Invoke(() => pridat.Content =  "Editovat");
                    Dispatcher.Invoke(() => jmeno.Text =  ZakaznikView.zakaznik.Jmeno);
                    Dispatcher.Invoke(() => prijmeni.Text = ZakaznikView.zakaznik.Prijmeni);
                    Dispatcher.Invoke(() => telefon.Text = ZakaznikView.zakaznik.Telefon);
                    Dispatcher.Invoke(() => email.Text = ZakaznikView.zakaznik.Email);
                    Dispatcher.Invoke(() => adresa.Text = ZakaznikView.zakaznik.Adresa);
                    Dispatcher.Invoke(() => poznamka.Text = ZakaznikView.zakaznik.Poznamky);
                }
            });
            threadAdd.Start();

        }

        private void pridat_Click(object sender, RoutedEventArgs e)
        {
            Thread threadAdd = new Thread(() =>
            {
                if (ZakaznikView.edit)
            {
                    Dispatcher.Invoke(() => ZakaznikView.zakaznik.Jmeno = jmeno.Text);
                    Dispatcher.Invoke(() => ZakaznikView.zakaznik.Prijmeni = prijmeni.Text);
                    Dispatcher.Invoke(() => ZakaznikView.zakaznik.Telefon = telefon.Text);
                    Dispatcher.Invoke(() => ZakaznikView.zakaznik.Email = email.Text);
                    Dispatcher.Invoke(() => ZakaznikView.zakaznik.Adresa = adresa.Text);
                    Dispatcher.Invoke(() => ZakaznikView.zakaznik.Poznamky = poznamka.Text);
            }
            else
            {
                    Dispatcher.Invoke(() => ZakaznikViewModel.Zakaznici.Add(new Zakaznik
                    {
                        Id = Dispatcher.Invoke(() => ZakaznikViewModel.Zakaznici.Count() + 1),
                        Jmeno = Dispatcher.Invoke(() => jmeno.Text),
                        Prijmeni =  Dispatcher.Invoke(() => prijmeni.Text),
                        Telefon = Dispatcher.Invoke(() => telefon.Text),
                        Email = Dispatcher.Invoke(() => email.Text),
                        Adresa = Dispatcher.Invoke(() => adresa.Text),
                        Poznamky = Dispatcher.Invoke(() => poznamka.Text)
                    }));
            }
                ZakaznikView.edit = Dispatcher.Invoke(() => false);
                ZakaznikView.zakaznik = null;

                Dispatcher.Invoke(() => NovyKlient.GetWindow(this).Close());
            });
            threadAdd.Start();
        }

        private void konec_Click(object sender, RoutedEventArgs e)
        {
            Dispatcher.Invoke(() => NovyKlient.GetWindow(this).Close());
        }
    }
}
