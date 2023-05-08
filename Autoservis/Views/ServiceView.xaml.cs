using Autoservice.Model;
using Autoservice.ViewModel;
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
    /// Interaction logic for ServiceView.xaml
    /// </summary>
    public partial class ServiceView : UserControl
    {
        public static Service Service;
        public static bool EditService;
        public static bool EditItem;
        public static Price Price;
        
        public ServiceView()
        {
            InitializeComponent();
            lvAuta.ItemsSource = Dispatcher.Invoke(() => AutoView.seznamVybraneAuto);
            lvServis.ItemsSource =
                Dispatcher.Invoke(() => ServisViewModel.SeznamServisu.Where(x => x.IdAuto == AutoView.auto.IdVozu));

            lvServis.Items.Refresh();
            lvCena.Items.Refresh();
        }

        private void AddServiceClick(object sender, RoutedEventArgs e)
        {
            var threadAdd = new Thread(() =>
            {
                var newServiceWindow = Dispatcher.Invoke(() => new NewService());
                Dispatcher.Invoke(() => newServiceWindow.ShowDialog());
                Dispatcher.Invoke(() => lvServis.ItemsSource =  ServisViewModel.SeznamServisu.Where(x => x.IdAuto == AutoView.auto.IdVozu));
            });
            threadAdd.Start();

        }

        // public bool FiltrServisu(object obj)
        // {    
        //     if ((obj as Service).IdAuto == AutoView.auto.IdVozu)
        //     {
        //         return true;
        //     }
        //     return false;
        // }
        //
        // public bool FiltrCena(object obj)
        // {
        //     Service service = (Service)lvServis.SelectedItem;
        //     if ((obj as Price).IdServisu == service.IdServis)
        //     {
        //         return true;
        //     }
        //     return false;
        // }



        private void LV_Service_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvServis.SelectedItem != null)
            {
                Service = (Service)lvServis.SelectedItem;
                lvCena.ItemsSource = CenaViewModel.SeznamCenaServisu.Where(x => x.IdServisu == Service.IdServis);
                lvCena.Items.Refresh();
            }
            else
            {
                lvCena.ItemsSource = null;
                lvCena.Items.Refresh();
            }

        }

        private void AddItemClick(object sender, RoutedEventArgs e)
        {
            var thread = new Thread(() =>
            {
                if (Dispatcher.Invoke(() => lvServis.SelectedItem != null))
                {
                    Service = Dispatcher.Invoke(() => (Service)lvServis.SelectedItem);
                    var newItemWindow = Dispatcher.Invoke(() => new NovaCena());
                    Dispatcher.Invoke(() => newItemWindow.ShowDialog());
                    Dispatcher.Invoke(() => lvCena.Items.Refresh());
                    Service = Dispatcher.Invoke(() => (Service)lvServis.SelectedItem);
                    Dispatcher.Invoke(() => lvCena.ItemsSource = CenaViewModel.SeznamCenaServisu.Where(x => x.IdServisu == Service.IdServis));
                }
                else
                {
                    MessageBox.Show("Není vybrán Service", "Chyba");
                }
            });
            thread.Start();

        }

        private void RemoveServiceClick(object sender, RoutedEventArgs e)
        {
            var thread = new Thread(() =>
            {
                Service = Dispatcher.Invoke(() => (Service)lvServis.SelectedItem);
                ObservableCollection<Price> a = new ObservableCollection<Price>();
                foreach (var item in CenaViewModel.SeznamCenaServisu)
                {

                    if (Dispatcher.Invoke(() => item.IdServisu == Service.IdServis))
                    {
                        Dispatcher.Invoke(() => a.Add(item));
                        Dispatcher.Invoke(() => MessageBox.Show(item.Polozka));
                    }
                }
                foreach (var item2 in a)
                {
                    Dispatcher.Invoke(() => CenaViewModel.SeznamCenaServisu.Remove(item2));
                }
                Dispatcher.Invoke(() => ServisViewModel.SeznamServisu.Remove((Service)lvServis.SelectedItem));
                Dispatcher.Invoke(() => lvServis.ItemsSource = ServisViewModel.SeznamServisu.Where(x => x.IdAuto == AutoView.auto.IdVozu));

            });
            thread.Start();
        }

        private void RemoveItemClick(object sender, RoutedEventArgs e)
        {
            var thread = new Thread(() =>
            {
                Dispatcher.Invoke(() => CenaViewModel.SeznamCenaServisu.Remove((Price)lvCena.SelectedItem));
                Service = Dispatcher.Invoke(() => (Service)lvServis.SelectedItem);
                Dispatcher.Invoke(() => lvCena.ItemsSource = CenaViewModel.SeznamCenaServisu.Where(x => x.IdServisu == Service.IdServis));
            });
            thread.Start();
        }

        private void EditItemClick(object sender, RoutedEventArgs e)
        {
            var thread = new Thread(() =>
            {
                EditItem = true;
                Price = Dispatcher.Invoke(() => (Price)lvCena.SelectedItem);
                if (Dispatcher.Invoke(() => lvServis.SelectedItem != null))
                {
                    Service = Dispatcher.Invoke(() => (Service)lvServis.SelectedItem);
                    var editItemWindow = Dispatcher.Invoke(() => new NovaCena());
                    Dispatcher.Invoke(() => editItemWindow.ShowDialog());
                    Dispatcher.Invoke(() => lvCena.Items.Refresh());
                    Service = Dispatcher.Invoke(() => (Service)lvServis.SelectedItem);
                    Dispatcher.Invoke(() => lvCena.ItemsSource = CenaViewModel.SeznamCenaServisu.Where(x => x.IdServisu == Service.IdServis));
                    EditItem = false;
                }
                else
                {
                    MessageBox.Show("Není vybránä položka", "Chyba");
                }
            });
            thread.Start();
        }

        private void EditServiceClick(object sender, RoutedEventArgs e)
        {
            var thread = new Thread(() =>
            {
                EditService = true;
                Service = Dispatcher.Invoke(() => (Service)lvServis.SelectedItem);
                if (Dispatcher.Invoke(() => (Service)lvServis.SelectedItem != null))
                {
                    var editServiceWindow = Dispatcher.Invoke(() => new NewService());
                    Dispatcher.Invoke(() => editServiceWindow.ShowDialog());
                    Dispatcher.Invoke(() => lvServis.ItemsSource = ServisViewModel.SeznamServisu.Where(x => x.IdAuto == AutoView.auto.IdVozu));
                    EditService = false;
                }
                else
                {
                    MessageBox.Show("Není vybrán Service", "Chyba");
                }
            });
            thread.Start();
        }
        
        private void CloseWindow()
        {
            var parentWindow = Window.GetWindow(this);
            parentWindow?.Close();
        }

        private void BackToAutoViewClick(object sender, RoutedEventArgs e)
        {
            CloseWindow();
        }
        
        private void CloseAppClick(object sender, RoutedEventArgs e)
        {
            ExitApplication();
        }
    
        private static void Reminder()
        {
            MessageBox.Show("Don't forget to Save data!!", "Reminder", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    
        private static void ExitApplication()
        {
            Reminder();
            var result = MessageBox.Show("Are you sure you want to exit the application?", "Confirmation",
                MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes) Application.Current.Shutdown();
        }
        
    }
}
