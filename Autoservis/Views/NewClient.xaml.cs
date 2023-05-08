using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using Autoservis.Model;
using Autoservis.ViewModel;

namespace Autoservice.Views;

/// <summary>
///     Interaction logic for NewClient.xaml
/// </summary>
public partial class NewClient : Window
{
    public NewClient()
    {
        InitializeComponent();

        if (ClientsView.edit)
        {
            pridat.Content = "✔";
            jmeno.Text = ClientsView.zakaznik.Jmeno;
            prijmeni.Text = ClientsView.zakaznik.Prijmeni;
            telefon.Text = ClientsView.zakaznik.Telefon;
            email.Text = ClientsView.zakaznik.Email;
            adresa.Text = ClientsView.zakaznik.Adresa;
            poznamka.Text = ClientsView.zakaznik.Poznamky;
        }
    }

    private void AddClick(object sender, RoutedEventArgs e)
    {
        // Check if all required fields are entered
        if (string.IsNullOrEmpty(jmeno.Text) ||
            string.IsNullOrEmpty(prijmeni.Text) ||
            string.IsNullOrEmpty(email.Text) ||
            string.IsNullOrEmpty(telefon.Text) ||
            string.IsNullOrEmpty(adresa.Text))
        {
            Dispatcher.Invoke(() => MessageBox.Show("Please enter all required information."));
            return;
        }

        // Validate email format
        const string emailPattern = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
        const string phonePattern = @"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$";

        if (!Regex.IsMatch(email.Text, emailPattern))
        {
            Dispatcher.Invoke(() => MessageBox.Show("Please enter a valid email address."));
            return;
        }

        if (!Regex.IsMatch(telefon.Text, phonePattern))
        {
            Dispatcher.Invoke(() => MessageBox.Show("Please enter a valid phone number."));
            return;
        }

        if (ClientsView.edit)
        {
            Dispatcher.Invoke(() => ClientsView.zakaznik.Jmeno = jmeno.Text);
            Dispatcher.Invoke(() => ClientsView.zakaznik.Prijmeni = prijmeni.Text);
            Dispatcher.Invoke(() => ClientsView.zakaznik.Telefon = telefon.Text);
            Dispatcher.Invoke(() => ClientsView.zakaznik.Email = email.Text);
            Dispatcher.Invoke(() => ClientsView.zakaznik.Adresa = adresa.Text);
            Dispatcher.Invoke(() => ClientsView.zakaznik.Poznamky = poznamka.Text);
        }
        else
        {
            Dispatcher.Invoke(() => ZakaznikViewModel.Zakaznici.Add(new Zakaznik
            {
                Id = Dispatcher.Invoke(() => ZakaznikViewModel.Zakaznici.Count() + 1),
                Jmeno = Dispatcher.Invoke(() => jmeno.Text),
                Prijmeni = Dispatcher.Invoke(() => prijmeni.Text),
                Telefon = Dispatcher.Invoke(() => telefon.Text),
                Email = Dispatcher.Invoke(() => email.Text),
                Adresa = Dispatcher.Invoke(() => adresa.Text),
                Poznamky = Dispatcher.Invoke(() => poznamka.Text)
            }));
        }

        ClientsView.edit = Dispatcher.Invoke(() => false);
        ClientsView.zakaznik = null!;

        Dispatcher.Invoke(() => GetWindow(this)!.Close());
    }


    private void ClientEndClick(object sender, RoutedEventArgs e)
    {
        Dispatcher.Invoke(() => GetWindow(this)!.Close());
    }
}