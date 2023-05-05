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

namespace Autoservis
{
    /// <summary>
    /// Interaction logic for ServisWindow.xaml
    /// </summary>
    public partial class ServisWindow : Window
    {
        public ServisWindow()
        {
            InitializeComponent();

        }

        private void ServisViewControl_Loaded(object sender, RoutedEventArgs e)
        {
            Autoservis.ViewModel.ServisViewModel servisViewModelObject =
            new Autoservis.ViewModel.ServisViewModel();
            servisViewModelObject.LoadSeznamServisu();

            ServisViewControl.DataContext = servisViewModelObject;
        }

    }
}
