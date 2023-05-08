using Autoservis.Manager;
using Autoservis.Model;
using Autoservis.ViewModel;
using Autoservice.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace Autoservis
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void ZakaznikViewControl_Loaded(object sender, RoutedEventArgs e)
        {
            ServisViewModel.SeznamServisu = ClientsView.servisMng.GetAllServis();
            ZakaznikViewModel.Zakaznici = ClientsView.klientMng.GetAllKlient();
            AutoViewModel.Auta = ClientsView.autoMng.GetAllAuto();
            CenaViewModel.SeznamCenaServisu = ClientsView.cenaMng.GetAllCena();

        }

     

        private void PridejViewControl_Loaded(object sender, RoutedEventArgs e)
        {
            Autoservis.ViewModel.ZakaznikViewModel autoViewModelObject =
               new Autoservis.ViewModel.ZakaznikViewModel();

            ZakaznikViewControl.DataContext = autoViewModelObject;
        }

        private void PridejKlienta_Loaded(object sender, RoutedEventArgs e)
        {
            Autoservis.ViewModel.ZakaznikViewModel autoViewModelObject =
               new Autoservis.ViewModel.ZakaznikViewModel();

            ZakaznikViewControl.DataContext = autoViewModelObject;
        }

    }
}
    
