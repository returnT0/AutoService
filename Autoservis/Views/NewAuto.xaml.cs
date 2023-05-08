using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using Autoservice.Model;
using Autoservice.ViewModel;

namespace Autoservice.Views;

/// <summary>
///     Interaction logic for NewAuto.xaml
/// </summary>
public partial class NewAuto : Window
{
    public NewAuto()
    {
        InitializeComponent();
        if (AutoView.Edit)
        {
            znackaVozu.Text = AutoView.Auto!.ZnackaVozu;
            modelVozu.Text = AutoView.Auto.ModelVozu;
            spz.Text = AutoView.Auto.Spz;
            vin.Text = AutoView.Auto.Vin;
            barva.Text = AutoView.Auto.Barva;
            pridat.Content = "✔";
        }
    }

    private void AddClick(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrEmpty(znackaVozu.Text) ||
            string.IsNullOrEmpty(modelVozu.Text) ||
            string.IsNullOrEmpty(spz.Text) ||
            string.IsNullOrEmpty(vin.Text) ||
            string.IsNullOrEmpty(barva.Text))
        {
            Dispatcher.Invoke(() => MessageBox.Show("Please enter all required information.", "Error",
                MessageBoxButton.OK, MessageBoxImage.Error));
            return;
        }

        var spzPattern = @"^[A-Z0-9]{8}$";
        var vinPattern = @"^\d{11,17}$";

        if (!Regex.IsMatch(spz.Text, spzPattern))
        {
            Dispatcher.Invoke(() =>
                MessageBox.Show("Please enter a valid SPZ number. (8 characters, capital letters and numbers only)",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error));
            return;
        }

        if (!Regex.IsMatch(vin.Text, vinPattern))
        {
            Dispatcher.Invoke(() => MessageBox.Show("Please enter a valid VIN number. (11-17 numbers only)", "Error",
                MessageBoxButton.OK, MessageBoxImage.Error));
            return;
        }

        if (AutoView.Edit)
        {
            Dispatcher.Invoke(() => AutoView.Auto!.ZnackaVozu = znackaVozu.Text);
            Dispatcher.Invoke(() => AutoView.Auto!.ModelVozu = modelVozu.Text);
            Dispatcher.Invoke(() => AutoView.Auto!.Spz = spz.Text);
            Dispatcher.Invoke(() => AutoView.Auto!.Vin = vin.Text);
            Dispatcher.Invoke(() => AutoView.Auto!.Barva = barva.Text);
        }
        else
        {
            Dispatcher.Invoke(() => AutoViewModel.Auta.Add(new Auto
            {
                IdVozu = Dispatcher.Invoke(() => AutoViewModel.Auta.Count() + 1),
                ZnackaVozu = Dispatcher.Invoke(() => znackaVozu.Text),
                ModelVozu = Dispatcher.Invoke(() => modelVozu.Text),
                Spz = Dispatcher.Invoke(() => spz.Text),
                Vin = Dispatcher.Invoke(() => vin.Text),
                Barva = Dispatcher.Invoke(() => barva.Text),
                IdKlienta = Dispatcher.Invoke(() => ClientsView.ClientsList.First()!.Id)
            }));
        }

        AutoView.Edit = Dispatcher.Invoke(() => false);
        AutoView.Auto = null!;

        Dispatcher.Invoke(() => GetWindow(this)!.Close());
    }

    private void CloseClick(object sender, RoutedEventArgs e)
    {
        Dispatcher.Invoke(() => GetWindow(this)!.Close());
    }
}