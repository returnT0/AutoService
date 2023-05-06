using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using Autoservis.Model;
using Autoservis.ViewModel;

namespace Autoservis.Views;

/// <summary>
///     Interaction logic for NovyKlient.xaml
/// </summary>
public partial class NovyKlient : Window
{
    public NovyKlient()
    {
        InitializeComponent();
        var threadAdd = new Thread(() =>
        {
            if (ZakaznikView.edit && ZakaznikView.zakaznik != null)
            {
                Dispatcher.Invoke(() => pridat.Content = "✔");
                Dispatcher.Invoke(() => jmeno.Text = ZakaznikView.zakaznik.Jmeno);
                Dispatcher.Invoke(() => prijmeni.Text = ZakaznikView.zakaznik.Prijmeni);
                Dispatcher.Invoke(() => telefon.Text = ZakaznikView.zakaznik.Telefon);
                Dispatcher.Invoke(() => email.Text = ZakaznikView.zakaznik.Email);
                Dispatcher.Invoke(() => adresa.Text = ZakaznikView.zakaznik.Adresa);
                Dispatcher.Invoke(() => poznamka.Text = ZakaznikView.zakaznik.Poznamky);
            }
        });
        threadAdd.Start();
    }

    private void pridat_Click(object sender, RoutedEventArgs e)
    {
        // Thread threadAdd = new Thread(() =>
        // {
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
        var emailPattern = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
        var phonePattern = @"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$";

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

        if (ZakaznikView.edit)
        {
            Dispatcher.Invoke(() => ZakaznikView.zakaznik.Jmeno = jmeno.Text);
            Dispatcher.Invoke(() => ZakaznikView.zakaznik.Prijmeni = prijmeni.Text);
            Dispatcher.Invoke(() => ZakaznikView.zakaznik.Telefon = telefon.Text);
            Dispatcher.Invoke(() => ZakaznikView.zakaznik.Email = email.Text);
            Dispatcher.Invoke(() => ZakaznikView.zakaznik.Adresa = adresa.Text);
            Dispatcher.Invoke(() => ZakaznikView.zakaznik.Poznamky = poznamka.Text);
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

        ZakaznikView.edit = Dispatcher.Invoke(() => false);
        ZakaznikView.zakaznik = null!;

        Dispatcher.Invoke(() => GetWindow(this)!.Close());
        // });
        // threadAdd.Start();
    }


    private void konec_Click(object sender, RoutedEventArgs e)
    {
        Dispatcher.Invoke(() => GetWindow(this)!.Close());
    }
}