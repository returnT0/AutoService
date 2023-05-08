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

        if (ClientsView.Edit)
        {
            pridat.Content = "✔";
            jmeno.Text = ClientsView.Zakaznik.Jmeno;
            prijmeni.Text = ClientsView.Zakaznik.Prijmeni;
            telefon.Text = ClientsView.Zakaznik.Telefon;
            email.Text = ClientsView.Zakaznik.Email;
            adresa.Text = ClientsView.Zakaznik.Adresa;
            poznamka.Text = ClientsView.Zakaznik.Poznamky;
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

        if (ClientsView.Edit)
        {
            Dispatcher.Invoke(() => ClientsView.Zakaznik.Jmeno = jmeno.Text);
            Dispatcher.Invoke(() => ClientsView.Zakaznik.Prijmeni = prijmeni.Text);
            Dispatcher.Invoke(() => ClientsView.Zakaznik.Telefon = telefon.Text);
            Dispatcher.Invoke(() => ClientsView.Zakaznik.Email = email.Text);
            Dispatcher.Invoke(() => ClientsView.Zakaznik.Adresa = adresa.Text);
            Dispatcher.Invoke(() => ClientsView.Zakaznik.Poznamky = poznamka.Text);
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

        ClientsView.Edit = Dispatcher.Invoke(() => false);
        ClientsView.Zakaznik = null!;

        Dispatcher.Invoke(() => GetWindow(this)!.Close());
    }


    private void ClientEndClick(object sender, RoutedEventArgs e)
    {
        Dispatcher.Invoke(() => GetWindow(this)!.Close());
    }
}