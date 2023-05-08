using Autoservis.Model;
using Autoservis.ViewModel;
using System;
using System.Collections;
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

namespace Autoservice.Views
{
    /// <summary>
    /// Interaction logic for ServisView.xaml
    /// </summary>
    public partial class ServisView : UserControl
    {
        public static Servis servis;
        public static bool editS;
        public static bool editP;
        public static Cena cena;
        public ServisView()
        {
            InitializeComponent();
            lvServis.ItemsSource = ServisViewModel.SeznamServisu.Where(x => x.IdAuto == AutoView.auto.IdVozu);

            lvServis.Items.Refresh();
            lvCena.Items.Refresh();
        }

        private void PridatServis_Click(object sender, RoutedEventArgs e)
        {
            Thread threadAdd = new Thread(() =>
            {
                NovyServis oknoNovyServis = Dispatcher.Invoke(() => new NovyServis());
                Dispatcher.Invoke(() => oknoNovyServis.ShowDialog());
                Dispatcher.Invoke(() => lvServis.ItemsSource =  ServisViewModel.SeznamServisu.Where(x => x.IdAuto == AutoView.auto.IdVozu));
            });
            threadAdd.Start();

        }

        public bool FiltrServisu(object obj)
        {    
            if ((obj as Servis).IdAuto == AutoView.auto.IdVozu)
            {
                return true;
            }
            return false;
        }

        public bool FiltrCena(object obj)
        {
            Servis servis = (Servis)lvServis.SelectedItem;
            if ((obj as Cena).IdServisu == servis.IdServis)
            {
                return true;
            }
            return false;
        }



        private void lvServis_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvServis.SelectedItem != null)
            {
                servis = (Servis)lvServis.SelectedItem;
                lvCena.ItemsSource = CenaViewModel.SeznamCenaServisu.Where(x => x.IdServisu == servis.IdServis);
                lvCena.Items.Refresh();
            }
            else
            {
                lvCena.ItemsSource = null;
                lvCena.Items.Refresh();
            }

        }

        private void PridatPolozku_Click(object sender, RoutedEventArgs e)
        {
            Thread threadAddPolozka = new Thread(() =>
            {
                if (Dispatcher.Invoke(() => lvServis.SelectedItem != null))
                {
                    servis = Dispatcher.Invoke(() => (Servis)lvServis.SelectedItem);
                    NovaCena oknoNovyCena = Dispatcher.Invoke(() => new NovaCena());
                    Dispatcher.Invoke(() => oknoNovyCena.ShowDialog());
                    Dispatcher.Invoke(() => lvCena.Items.Refresh());
                    servis = Dispatcher.Invoke(() => (Servis)lvServis.SelectedItem);
                    Dispatcher.Invoke(() => lvCena.ItemsSource = CenaViewModel.SeznamCenaServisu.Where(x => x.IdServisu == servis.IdServis));
                 }
                else
                {
                    MessageBox.Show("Není vybrán servis", "Chyba");
                }
            });
            threadAddPolozka.Start();

        }

        private void OdebratServis_Click(object sender, RoutedEventArgs e)
        {
            Thread threadOdeberServis = new Thread(() =>
            {
                servis = Dispatcher.Invoke(() => (Servis)lvServis.SelectedItem);
                ObservableCollection<Cena> a = new ObservableCollection<Cena>();
                foreach (var item in CenaViewModel.SeznamCenaServisu)
                {

                    if (Dispatcher.Invoke(() => item.IdServisu == servis.IdServis))
                    {
                        Dispatcher.Invoke(() => a.Add(item));
                        Dispatcher.Invoke(() => MessageBox.Show(item.Polozka));
                    }
                }
                foreach (var item2 in a)
                {
                    Dispatcher.Invoke(() => CenaViewModel.SeznamCenaServisu.Remove(item2));
                }
                Dispatcher.Invoke(() => ServisViewModel.SeznamServisu.Remove((Servis)lvServis.SelectedItem));
                Dispatcher.Invoke(() => lvServis.ItemsSource = ServisViewModel.SeznamServisu.Where(x => x.IdAuto == AutoView.auto.IdVozu));

            });
            threadOdeberServis.Start();
        }

        private void OdebratPolozku_Click(object sender, RoutedEventArgs e)
        {
            Thread threadOdeberPolozku = new Thread(() =>
            {
                Dispatcher.Invoke(() => CenaViewModel.SeznamCenaServisu.Remove((Cena)lvCena.SelectedItem));
                servis = Dispatcher.Invoke(() => (Servis)lvServis.SelectedItem);
                Dispatcher.Invoke(() => lvCena.ItemsSource = CenaViewModel.SeznamCenaServisu.Where(x => x.IdServisu == servis.IdServis));
            });
            threadOdeberPolozku.Start();
        }

        private void Edit_Polozka(object sender, RoutedEventArgs e)
        {
            Thread threadEditPolozka = new Thread(() =>
            {
                editP = true;
                cena = Dispatcher.Invoke(() => (Cena)lvCena.SelectedItem);
                if (Dispatcher.Invoke(() => lvServis.SelectedItem != null))
                {
                    servis = Dispatcher.Invoke(() => (Servis)lvServis.SelectedItem);
                    NovaCena oknoNovyCena = Dispatcher.Invoke(() => new NovaCena());
                    Dispatcher.Invoke(() => oknoNovyCena.ShowDialog());
                    Dispatcher.Invoke(() => lvCena.Items.Refresh());
                    servis = Dispatcher.Invoke(() => (Servis)lvServis.SelectedItem);
                    Dispatcher.Invoke(() => lvCena.ItemsSource = CenaViewModel.SeznamCenaServisu.Where(x => x.IdServisu == servis.IdServis));
                    editP = false;
                }
                else
                {
                    MessageBox.Show("Není vybránä položka", "Chyba");
                }
            });
            threadEditPolozka.Start();
        }

        private void Edit_servis(object sender, RoutedEventArgs e)
        {
            Thread threadEditServis = new Thread(() =>
            {
                editS = true;
                servis = Dispatcher.Invoke(() => (Servis)lvServis.SelectedItem);
                if (Dispatcher.Invoke(() => (Servis)lvServis.SelectedItem != null))
                {
                    NovyServis oknoNovyServis = Dispatcher.Invoke(() => new NovyServis());
                    Dispatcher.Invoke(() => oknoNovyServis.ShowDialog());
                    Dispatcher.Invoke(() => lvServis.ItemsSource = ServisViewModel.SeznamServisu.Where(x => x.IdAuto == AutoView.auto.IdVozu));
                    editS = false;
                }
                else
                {
                    MessageBox.Show("Není vybrán servis", "Chyba");
                }
            });
            threadEditServis.Start();
        }
        
        private void CloseWindow()
        {
            Window parentWindow = Window.GetWindow(this);
            if (parentWindow != null)
            {
                parentWindow.Close();
            }
        }

        private void konec_Click(object sender, RoutedEventArgs e)
        {
            CloseWindow();
        }
        
    }
}
