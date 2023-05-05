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
    /// Interaction logic for NovyServis.xaml
    /// </summary>
    public partial class NovyServis : Window
    {
        public NovyServis()
        {
            InitializeComponent();
            if (ServisView.editS)
            {
                zavada.Text = ServisView.servis.Zavada;
                datumServiu.Text = ServisView.servis.DatumServisu;
                tachometr.Text = ServisView.servis.Tachometr+"";
                plnostNadrze.Text = ServisView.servis.PlnostNadrze;
                pridat.Content = "Editovat";

            }
        }

        private void pridat_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                Thread threadAdd = new Thread(() =>
                {
                    if (ServisView.editS)
                    {
                        Dispatcher.Invoke(() => ServisView.servis.Zavada = zavada.Text);
                        Dispatcher.Invoke(() => ServisView.servis.DatumServisu = datumServiu.Text);
                        Dispatcher.Invoke(() => ServisView.servis.Tachometr = int.Parse(tachometr.Text));
                        Dispatcher.Invoke(() => ServisView.servis.PlnostNadrze = plnostNadrze.Text);
                    }
                    else
                    {

                        Dispatcher.Invoke(() => ServisViewModel.SeznamServisu.Add(new Servis
                        {

                            IdServis = Dispatcher.Invoke(() => ServisViewModel.SeznamServisu.Count()+1),
                            IdAuto = Dispatcher.Invoke(() => AutoView.auto.IdVozu),
                            Zavada = Dispatcher.Invoke(() => zavada.Text),
                            DatumServisu = Dispatcher.Invoke(() => datumServiu.Text),
                          
                            Tachometr = Dispatcher.Invoke(() => int.Parse(tachometr.Text)),
                            PlnostNadrze = Dispatcher.Invoke(() => plnostNadrze.Text),
                            CenaPolozky =  null
                        }));
                    }
                    Dispatcher.Invoke(() => NovyServis.GetWindow(this).Close());
                });
                threadAdd.Start();
            }
            catch (Exception)
            {
            }
           
        }

        private void konec_Click(object sender, RoutedEventArgs e)
        {
            Dispatcher.Invoke(() => NovyServis.GetWindow(this).Close());
        }
    }
}
