using System;
using System.Linq;
using System.Text.RegularExpressions;
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
        datumServisu.Text = ServiceView.Service.DatumServisu;
        tachometr.Text = ServiceView.Service.Tachometr + "";
        plnostNadrze.Text = ServiceView.Service.PlnostNadrze;
        add.Content = "✔";
    }

    private void AddClick(object sender, RoutedEventArgs e)
    {
        try
        {
            // Check if all required fields are entered
            if (string.IsNullOrWhiteSpace(zavada.Text) ||
                string.IsNullOrWhiteSpace(datumServisu.Text) ||
                string.IsNullOrWhiteSpace(tachometr.Text) ||
                string.IsNullOrWhiteSpace(plnostNadrze.Text))
            {
                MessageBox.Show("Please enter all required information.", "Missing information", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }

            // Validate input fields
            if (!int.TryParse(tachometr.Text, out var tachometer))
            {
                MessageBox.Show("Please enter a valid tachometer value.", "Invalid tachometer value",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!Regex.IsMatch(datumServisu.Text, @"^(0[1-9]|[1-2]\d|3[0-1])\.(0[1-9]|1[0-2])\.\d{4}$"))
            {
                MessageBox.Show("Please enter a valid date format (dd.mm.yyyy).", "Invalid date format",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Add or edit the service
            if (ServiceView.EditService)
            {
                ServiceView.Service.Zavada = zavada.Text;
                ServiceView.Service.DatumServisu = datumServisu.Text;
                ServiceView.Service.Tachometr = tachometer;
                ServiceView.Service.PlnostNadrze = plnostNadrze.Text;
            }
            else
            {
                ServisViewModel.SeznamServisu.Add(new Service
                {
                    IdServis = ServisViewModel.SeznamServisu.Count() + 1,
                    IdAuto = AutoView.Auto!.IdVozu,
                    Zavada = zavada.Text,
                    DatumServisu = datumServisu.Text,
                    Tachometr = tachometer,
                    PlnostNadrze = plnostNadrze.Text,
                    PricePolozky = null!
                });
            }
        }
        catch (Exception exception)
        {
            MessageBox.Show(exception.Message);
        }

        ServiceView.EditService = false;
        ServiceView.Service = null!;
        GetWindow(this)?.Close();
    }


    private void CloseClick(object sender, RoutedEventArgs e)
    {
        Dispatcher.Invoke(() => GetWindow(this)?.Close());
    }
}