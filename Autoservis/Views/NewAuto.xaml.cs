using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using Autoservice.Model;
using Autoservice.ViewModel;

namespace Autoservice.Views;

/// <summary>
///     Interaction logic for NewAuto.xaml
/// </summary>
public partial class NoveAuto : Window
{
    public NoveAuto()
    {
        InitializeComponent();
        if (AutoView.edit)
        {
            znackaVozu.Text = AutoView.auto.ZnackaVozu;
            modelVozu.Text = AutoView.auto.ModelVozu;
            spz.Text = AutoView.auto.Spz;
            vin.Text = AutoView.auto.Vin;
            barva.Text = AutoView.auto.Barva;
            pridat.Content = "✔";
        }
    }

    private void pridat_Click(object sender, RoutedEventArgs e)
    {
        // Check if all required fields are entered
        if (string.IsNullOrEmpty(znackaVozu.Text) ||
            string.IsNullOrEmpty(modelVozu.Text) ||
            string.IsNullOrEmpty(spz.Text) ||
            string.IsNullOrEmpty(vin.Text) ||
            string.IsNullOrEmpty(barva.Text))
        {
            Dispatcher.Invoke(() => MessageBox.Show("Please enter all required information."));
            return;
        }

        var spzPattern = @"^[A-Z0-9]{8}$";
        var vinPattern = @"^\d{11,17}$";

        if (!Regex.IsMatch(spz.Text, spzPattern))
        {
            Dispatcher.Invoke(() =>
                MessageBox.Show("Please enter a valid SPZ number. (8 characters, capital letters and numbers only)"));
            return;
        }

        if (!Regex.IsMatch(vin.Text, vinPattern))
        {
            Dispatcher.Invoke(() => MessageBox.Show("Please enter a valid VIN number. (11-17 numbers only)"));
            return;
        }

        if (AutoView.edit)
        {
            Dispatcher.Invoke(() => AutoView.auto.ZnackaVozu = znackaVozu.Text);
            Dispatcher.Invoke(() => AutoView.auto.ModelVozu = modelVozu.Text);
            Dispatcher.Invoke(() => AutoView.auto.Spz = spz.Text);
            Dispatcher.Invoke(() => AutoView.auto.Vin = vin.Text);
            Dispatcher.Invoke(() => AutoView.auto.Barva = barva.Text);
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
                IdKlienta = Dispatcher.Invoke(() => ClientsView.ClientsList.First().Id)
            }));
        }

        AutoView.edit = Dispatcher.Invoke(() => false);
        AutoView.auto = null!;

        Dispatcher.Invoke(() => GetWindow(this)!.Close());
    }

    private void konec_Click(object sender, RoutedEventArgs e)
    {
        Dispatcher.Invoke(() => GetWindow(this)!.Close());
    }
}