using System;
using System.Threading;
using System.Windows;
using Autoservice.Model;
using Autoservice.ViewModel;

namespace Autoservice.Views;

/// <summary>
///     Interaction logic for NewPrice.xaml
/// </summary>
public partial class NovaCena : Window
{
    public NovaCena()
    {
        InitializeComponent();
        if (ServiceView.EditItem)
        {
            item.Text = ServiceView.Price.Polozka;
            price.Text = ServiceView.Price.CenaPolozky + "";
        }
    }

    private void AddClick(object sender, RoutedEventArgs e)
    {
        var threadAdd = new Thread(() =>
        {
            if (ServiceView.EditItem)
            {
                try
                {
                    Dispatcher.Invoke(() => ServiceView.Price.Polozka = item.Text);
                    Dispatcher.Invoke(() => ServiceView.Price.CenaPolozky = int.Parse(price.Text));
                    Dispatcher.Invoke(() => GetWindow(this).Close());
                }
                catch (Exception e)
                {
                    MessageBox.Show("Chybne vyplněno" + e, "Chyba");
                }
            }
            else
            {
                var prevodCena = 0;

                if (Dispatcher.Invoke(() => int.TryParse(price.Text, out prevodCena)))
                {
                    Dispatcher.Invoke(() => CenaViewModel.SeznamCenaServisu.Add(new Price
                    {
                        Polozka = Dispatcher.Invoke(() => item.Text),
                        CenaPolozky = Dispatcher.Invoke(() => int.Parse(price.Text)),
                        IdServisu = Dispatcher.Invoke(() => ServiceView.Service.IdServis)
                    }));
                    Dispatcher.Invoke(() => GetWindow(this).Close());
                }
                else
                {
                    MessageBox.Show("Chybně vyplněno" + e, "Chyba");
                }
            }
        });
        threadAdd.Start();
    }


    private void CloseClick(object sender, RoutedEventArgs e)
    {
        Dispatcher.Invoke(() => GetWindow(this).Close());
    }
}