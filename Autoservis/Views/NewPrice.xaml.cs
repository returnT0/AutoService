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
        // Check if all required fields are entered
        if (string.IsNullOrEmpty(item.Text) || string.IsNullOrEmpty(price.Text))
        {
            Dispatcher.Invoke(() => MessageBox.Show("Please enter all required information. ", "Missing information", 
                MessageBoxButton.OK, MessageBoxImage.Error));
            return;
        }

        if (ServiceView.EditItem)
        {
            try
            {
                // Validate input
                var itemPrice = int.Parse(price.Text);
                if (itemPrice <= 0)
                {
                    Dispatcher.Invoke(() => MessageBox.Show("Please enter a valid price. ", "Error", 
                        MessageBoxButton.OK, MessageBoxImage.Error));
                    return;
                }

                // Update existing item
                Dispatcher.Invoke(() => ServiceView.Price.Polozka = item.Text);
                Dispatcher.Invoke(() => ServiceView.Price.CenaPolozky = itemPrice);
                Dispatcher.Invoke(() => GetWindow(this)!.Close());
            }
            catch (Exception ex)
            {
                Dispatcher.Invoke(() => MessageBox.Show("Please enter a valid price. " + ex.Message, "Error", 
                    MessageBoxButton.OK, MessageBoxImage.Error));
            }
        }
        else
        {
            try
            {
                // Validate input
                var itemPrice = int.Parse(price.Text);
                if (itemPrice <= 0)
                {
                    Dispatcher.Invoke(() => MessageBox.Show("Please enter a valid price.", "Error", 
                        MessageBoxButton.OK, MessageBoxImage.Error));
                    return;
                }

                // Add new item
                var priceNew = new Price
                {
                    Polozka = Dispatcher.Invoke(() => item.Text),
                    CenaPolozky = itemPrice,
                    IdServisu = Dispatcher.Invoke(() => ServiceView.Service.IdServis)
                };
                Dispatcher.Invoke(() => CenaViewModel.SeznamCenaServisu.Add(priceNew));
                Dispatcher.Invoke(() => GetWindow(this)!.Close());
            }
            catch (Exception ex)
            {
                Dispatcher.Invoke(() => MessageBox.Show("Please enter a valid price. " + ex.Message, "Error", 
                    MessageBoxButton.OK, MessageBoxImage.Error));
            }
        }
    }


    private void CloseClick(object sender, RoutedEventArgs e)
    {
        Dispatcher.Invoke(() => GetWindow(this)!.Close());
    }
}