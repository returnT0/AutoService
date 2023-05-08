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
using System.Windows.Shapes;
using Autoservice.ViewModel;

namespace Autoservice
{
    /// <summary>
    /// Interaction logic for ServiceWindow.xaml
    /// </summary>
    public partial class ServiceWindow : Window
    {
        public ServiceWindow()
        {
            InitializeComponent();

        }

        private void ServisViewControl_Loaded(object sender, RoutedEventArgs e)
        {
            ServisViewModel servisViewModelObject =
            new ServisViewModel();
            servisViewModelObject.LoadSeznamServisu();

            ServisViewControl.DataContext = servisViewModelObject;
        }

    }
}
