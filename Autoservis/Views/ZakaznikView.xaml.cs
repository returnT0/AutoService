// Purpose: Contains logic for ZakaznikView.xaml

using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Autoservis.Manager;
using Autoservis.Model;
using Autoservis.Repository;
using Autoservis.ViewModel;

namespace Autoservis.Views;

/// <summary>
///     Interaction logic for ZakaznikView.xaml
/// </summary>
public partial class ZakaznikView : UserControl

{
    public static ObservableCollection<Zakaznik> seznamVybranyZakaznik = new();

    public static Zakaznik zakaznik;
    public static bool edit;

    public ZakaznikView()
    {
        Repo repo = new();
        InitializeComponent();
        klientMng = new KlientMng(repo);
        autoMng = new AutoMng(repo);
        servisMng = new ServisMng(repo);
        cenaMng = new CenaMng(repo);

        ServisViewModel.SeznamServisu = servisMng.GetAllServis();
        ZakaznikViewModel.Zakaznici = klientMng.GetAllKlient();
        AutoViewModel.Auta = autoMng.GetAllAuto();
        CenaViewModel.SeznamCenaServisu = cenaMng.GetAllCena();

        lvZakaznici.ItemsSource = Dispatcher.Invoke(() => ZakaznikViewModel.Zakaznici);
    }

    public static KlientMng klientMng { get; set; }
    public static AutoMng autoMng { get; set; }
    public static ServisMng servisMng { get; set; }
    public static CenaMng cenaMng { get; set; }

    private void Reminder()
    {
        MessageBox.Show("Don't forget to Save data!!", "Reminder", MessageBoxButton.OK, MessageBoxImage.Warning);
    }

    private void Add_Button_Click(object sender, RoutedEventArgs e)
    {
        var threadOpen = new Thread(() =>
        {
            var isWindowClosed = false;
            var oknoNovyKlient = Dispatcher.Invoke(() => new NovyKlient());
            oknoNovyKlient.Closed += (s, args) => isWindowClosed = true;
            Dispatcher.Invoke(() => oknoNovyKlient.Show());
            Dispatcher.Invoke(() => lvZakaznici.Items.Refresh());

            while (!isWindowClosed) Thread.Sleep(100);

            MessageBox.Show("Data successfully added.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            Thread.Sleep(1000);
            Reminder();
        });
        threadOpen.Start();
    }

    private void lvZakaznici_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        var threadOpen = new Thread(() =>
        {
            zakaznik = Dispatcher.Invoke(() => (Zakaznik)lvZakaznici.SelectedItem);
            var oknoSeznamAut = Dispatcher.Invoke(() => new AutoWindow());
            Dispatcher.Invoke(() => seznamVybranyZakaznik.Clear());
            Dispatcher.Invoke(() => seznamVybranyZakaznik.Add(zakaznik));
            Dispatcher.Invoke(() => oknoSeznamAut.Show());
        });
        threadOpen.Start();
    }

    private void lvZakaznici_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
    }

    //Delete
    private async void Remove_Button_Click(object sender, RoutedEventArgs e)
    {
        var selectedItem = (Zakaznik)lvZakaznici.SelectedItem;

        if (selectedItem != null)
        {
            ZakaznikViewModel.Zakaznici.Remove(selectedItem);
            await RefreshListViewAsync();
            MessageBox.Show($"Uživatel {selectedItem.Jmeno} byl odstraněn.");
            Thread.Sleep(1000);
            Reminder();
        }
        else
        {
            MessageBox.Show("Undefined choice.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private async Task RefreshListViewAsync()
    {
        await Task.Delay(100); // Wait for 100ms to ensure that the UI has time to update
        lvZakaznici.Items.Refresh();
    }

    //Edit
    private async void Edit_Button_Click(object sender, RoutedEventArgs e)
    {
        if (lvZakaznici.SelectedItems.Count > 0)
        {
            edit = true;
            zakaznik = (Zakaznik)lvZakaznici.SelectedItem;
            var oknoNovyKlient = new NovyKlient();
            oknoNovyKlient.Show();
            await Task.Run(() => lvZakaznici.Dispatcher.Invoke(() => lvZakaznici.Items.Refresh()));
        }
        else
        {
            MessageBox.Show("Undefined choice.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    //Uloz do db
    private async void Save_Button_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            await SaveDataAsync();
            MessageBox.Show("Data saved successfully.", "Save");
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error saving data: {ex.Message}", "Save Error");
        }
    }

    private async Task SaveDataAsync()
    {
        await Task.Run(() =>
        {
            klientMng.RemoveAllKlient();
            autoMng.RemoveAllAuto();
            servisMng.RemoveAllServis();
            cenaMng.RemoveAllCena();

            klientMng.AddAllKlient(ZakaznikViewModel.Zakaznici);
            autoMng.AddAllAuto(AutoViewModel.Auta);
            servisMng.AddAllServis(ServisViewModel.SeznamServisu);
            cenaMng.AddAllCena(CenaViewModel.SeznamCenaServisu);
        });
    }


    private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        var searchText = (sender as TextBox).Text.Trim();

        var filteredList = ZakaznikViewModel.Zakaznici.Where(z =>
            z.Jmeno.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0 ||
            z.Prijmeni.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0 ||
            z.Email.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0);

        lvZakaznici.ItemsSource = filteredList;
        lvZakaznici.Items.Refresh();
    }

    // private void RefreshButtonClick(object sender, RoutedEventArgs e)
    // {
    //     lvZakaznici.Items.Refresh();
    // }
    private void Exit_Click(object sender, RoutedEventArgs e)
    {
        ExitApplication();
    }

    public static void ExitApplication()
    {
        var result = MessageBox.Show("Are you sure you want to exit the application?", "Confirmation",
            MessageBoxButton.YesNo, MessageBoxImage.Question);
        if (result == MessageBoxResult.Yes) Application.Current.Shutdown();
    }
}