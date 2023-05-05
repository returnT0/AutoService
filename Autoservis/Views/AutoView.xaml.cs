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
    /// Interaction logic for AutoView.xaml
    /// </summary>
    public partial class AutoView : UserControl

    {

        public static ObservableCollection<Auto> seznamVybraneAuto = new ObservableCollection<Auto>();
        public static Auto auto;
        public static bool edit;
        public AutoView()
        {
            InitializeComponent();
            lvZakaznik.ItemsSource = Dispatcher.Invoke(() => ZakaznikView.seznamVybranyZakaznik);
            lvAuta.ItemsSource = Dispatcher.Invoke(() => AutoViewModel.Auta.Where(x => x.IdKlienta == ZakaznikView.zakaznik.Id));
            Dispatcher.Invoke(() => lvAuta.Items.Refresh());
        }

        private void lvAuta_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Thread threadDoubleClick = new Thread(() =>
            {
                if (Dispatcher.Invoke(() => lvAuta.SelectedItems.Count > 0))
                {
                    auto = Dispatcher.Invoke(() => (Auto)lvAuta.SelectedItem);
                    ServisWindow oknoSeznamServis = Dispatcher.Invoke(() => new ServisWindow());
                    Dispatcher.Invoke(() => seznamVybraneAuto.Clear());
                    Dispatcher.Invoke(() => seznamVybraneAuto.Add(auto));
                    Dispatcher.Invoke(() => oknoSeznamServis.Show());
                }
                else
                {
                    MessageBox.Show("Není vybráno auto", "Chyba");
                }

            });
            threadDoubleClick.Start();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Thread threadAdd = new Thread(() =>
            {
                NoveAuto oknoNoveAuto = Dispatcher.Invoke(() => new NoveAuto());
                Dispatcher.Invoke(() => oknoNoveAuto.ShowDialog());
                Dispatcher.Invoke(() => lvAuta.ItemsSource = AutoViewModel.Auta.Where(x => x.IdKlienta == ZakaznikView.zakaznik.Id));
            });
            threadAdd.Start();
           
        }

        private void Button_Click_Odebrat(object sender, RoutedEventArgs e)
        {
            Thread threadOdeber = new Thread(() =>
            {
                if (Dispatcher.Invoke(() => lvAuta.SelectedItems.Count > 0))
                {
                    Dispatcher.Invoke(() => AutoViewModel.Auta.Remove((Auto)lvAuta.SelectedItem));
                    Dispatcher.Invoke(() => lvAuta.ItemsSource = AutoViewModel.Auta.Where(x => x.IdKlienta == ZakaznikView.zakaznik.Id));
                }
                else
                {
                    Dispatcher.Invoke(() => MessageBox.Show("Auto není vybráno", "Chyba"));
                }
            });
            threadOdeber.Start();

        }

        private void Button_Click_Edit(object sender, RoutedEventArgs e)
        {
            Thread threadEdit = new Thread(() =>
            {
                if (Dispatcher.Invoke(() => lvAuta.SelectedItems.Count > 0))
                {
                    edit = true;
                    auto = Dispatcher.Invoke(() => (Auto)lvAuta.SelectedItem);
                    NoveAuto oknoNoveAuto = Dispatcher.Invoke(() => new NoveAuto());
                    Dispatcher.Invoke(() => oknoNoveAuto.ShowDialog());
                    edit = false;
                }
                else
                {
                    Dispatcher.Invoke(() => MessageBox.Show("Auto není vybráno", "Chyba"));
                }
            });
            threadEdit.Start();
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
