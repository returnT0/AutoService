using System;
using System.Linq;
using System.Threading;
using System.Windows;
using Autoservice.Model;
using Autoservice.ViewModel;

namespace Autoservice.Views;

/// <summary>
///     Interaction logic for NewService.xaml
/// </summary>
public partial class NewService : Window
{
    public NewService()
    {
        InitializeComponent();
        if (!ServiceView.EditService) return;
        zavada.Text = ServiceView.Service.Zavada;
        datumServiu.Text = ServiceView.Service.DatumServisu;
        tachometr.Text = ServiceView.Service.Tachometr + "";
        plnostNadrze.Text = ServiceView.Service.PlnostNadrze;
        add.Content = "✔";
    }

    private void AddClick(object sender, RoutedEventArgs e)
    {
        try
        {
            var threadAdd = new Thread(() =>
            {
                if (ServiceView.EditService)
                {
                    Dispatcher.Invoke(() => ServiceView.Service.Zavada = zavada.Text);
                    Dispatcher.Invoke(() => ServiceView.Service.DatumServisu = datumServiu.Text);
                    Dispatcher.Invoke(() => ServiceView.Service.Tachometr = int.Parse(tachometr.Text));
                    Dispatcher.Invoke(() => ServiceView.Service.PlnostNadrze = plnostNadrze.Text);
                }
                else
                {
                    Dispatcher.Invoke(() => ServisViewModel.SeznamServisu.Add(new Service
                    {
                        IdServis = Dispatcher.Invoke(() => ServisViewModel.SeznamServisu.Count() + 1),
                        IdAuto = Dispatcher.Invoke(() => AutoView.auto.IdVozu),
                        Zavada = Dispatcher.Invoke(() => zavada.Text),
                        DatumServisu = Dispatcher.Invoke(() => datumServiu.Text),

                        Tachometr = Dispatcher.Invoke(() => int.Parse(tachometr.Text)),
                        PlnostNadrze = Dispatcher.Invoke(() => plnostNadrze.Text),
                        PricePolozky = null
                    }));
                }

                Dispatcher.Invoke(() => GetWindow(this).Close());
            });
            threadAdd.Start();
        }
        catch (Exception)
        {
        }
    }

    private void CloseClick(object sender, RoutedEventArgs e)
    {
        Dispatcher.Invoke(() => GetWindow(this)?.Close());
    }
}