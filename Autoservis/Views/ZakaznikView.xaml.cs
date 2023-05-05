using Autoservis.Manager;
using Autoservis.Model;
using Autoservis.Repository;
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
using System.Windows.Threading;

namespace Autoservis.Views
{
    /// <summary>
    /// Interaction logic for ZakaznikView.xaml
    /// </summary>
    public partial class ZakaznikView : UserControl

    {
        public static ObservableCollection<Zakaznik> seznamVybranyZakaznik = new ObservableCollection<Zakaznik>();

        public static Zakaznik zakaznik;
        public static bool edit;
        public static KlientMng klientMng { get; set; }
        public static AutoMng autoMng { get; set; }
        public static  ServisMng servisMng { get; set; }
        public static CenaMng cenaMng { get; set; }

        public ZakaznikView()
        {
             Repo repo = new();
             InitializeComponent();
             klientMng = new(repo);
             autoMng = new(repo);
             servisMng = new(repo);
             cenaMng = new(repo);

            ServisViewModel.SeznamServisu = ZakaznikView.servisMng.GetAllServis();
            ZakaznikViewModel.Zakaznici = ZakaznikView.klientMng.GetAllKlient();
            AutoViewModel.Auta = ZakaznikView.autoMng.GetAllAuto();
            CenaViewModel.SeznamCenaServisu = ZakaznikView.cenaMng.GetAllCena();

            lvZakaznici.ItemsSource = Dispatcher.Invoke(() => ZakaznikViewModel.Zakaznici);

        }

        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Thread threadOpen = new Thread(() =>
            {
                NovyKlient oknoNovyKlient = Dispatcher.Invoke(() => new NovyKlient());
                Dispatcher.Invoke(() => oknoNovyKlient.Show());
                Dispatcher.Invoke(() => lvZakaznici.Items.Refresh());
            });
            threadOpen.Start();
        }

        private void lvZakaznici_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Thread threadOpen = new Thread(() =>
            {
                zakaznik = Dispatcher.Invoke(() => (Zakaznik)lvZakaznici.SelectedItem);
                AutoWindow oknoSeznamAut = Dispatcher.Invoke(() => new AutoWindow());
                Dispatcher.Invoke(() => seznamVybranyZakaznik.Clear());
                Dispatcher.Invoke(() => seznamVybranyZakaznik.Add(zakaznik));
                Dispatcher.Invoke(() => oknoSeznamAut.Show());
            });
            threadOpen.Start();
        }

        private void lvZakaznici_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        //Delete
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Thread threadDelete = new Thread(() =>
            {
                Dispatcher.Invoke(() => ZakaznikViewModel.Zakaznici.Remove((Zakaznik)lvZakaznici.SelectedItem));
            });
                threadDelete.Start();
        }

        //Edit
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Thread threadEdit = new Thread(() => 
            {
                if (Dispatcher.Invoke(() => lvZakaznici.SelectedItems.Count >0))
                {
                    edit = true;
                    zakaznik = Dispatcher.Invoke(() => (Zakaznik)lvZakaznici.SelectedItem);
                    NovyKlient oknoNovyKlient = Dispatcher.Invoke(() => new NovyKlient());
                    Dispatcher.Invoke(() => oknoNovyKlient.Show());
                    Dispatcher.Invoke(() => lvZakaznici.Items.Refresh());
                }else
                {
                    MessageBox.Show("Není vybráno");
                }
            
            });
            threadEdit.Start();
        }

        //Uloz do db
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Thread threadUloz = new Thread(() =>
            {
            Dispatcher.Invoke(() => klientMng.RemoveAllKlient());
            Dispatcher.Invoke(() => autoMng.RemoveAllAuto());
            Dispatcher.Invoke(() => servisMng.RemoveAllServis());
            Dispatcher.Invoke(() => cenaMng.RemoveAllCena());

            Dispatcher.Invoke(() => klientMng.AddAllKlient(ZakaznikViewModel.Zakaznici));
            Dispatcher.Invoke(() => autoMng.AddAllAuto(AutoViewModel.Auta));
            Dispatcher.Invoke(() => servisMng.AddAllServis(ServisViewModel.SeznamServisu));
            Dispatcher.Invoke(() => cenaMng.AddAllCena(CenaViewModel.SeznamCenaServisu));
            });
            MessageBox.Show("Uloženo", "Uložení");
            threadUloz.Start();
        }
        
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Thread threadOpen = new Thread(() =>
            {
                FindView oknoNovyKlient = Dispatcher.Invoke(() => new FindView());
                Dispatcher.Invoke(() => oknoNovyKlient.Show());
                Dispatcher.Invoke(() => lvZakaznici.Items.Refresh());
            });
            threadOpen.Start();
        }
    }
}
