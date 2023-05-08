// Purpose: Contains logic for ClientsView.xaml

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Autoservis;
using Autoservis.Manager;
using Autoservis.Model;
using Autoservis.Repository;
using Autoservis.ViewModel;

namespace Autoservice.Views;

/// <summary>
///     Interaction logic for ClientsView.xaml
/// </summary>
public partial class ClientsView : UserControl

{
    public static ObservableCollection<Zakaznik> seznamVybranyZakaznik = new();

    public static Zakaznik zakaznik;
    public static bool edit;

    public ClientsView()
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

        lvZakaznici.ItemsSource = ZakaznikViewModel.Zakaznici;
        
    }

    public static KlientMng klientMng { get; set; }
    public static AutoMng autoMng { get; set; }
    public static ServisMng servisMng { get; set; }
    public static CenaMng cenaMng { get; set; }

    private static void Reminder()
    {
        MessageBox.Show("Don't forget to Save data!!", "Reminder", MessageBoxButton.OK, MessageBoxImage.Warning);
    }

    private void Add_Button_Click(object sender, RoutedEventArgs e)
    {
        var threadOpen = new Thread(() =>
        {
            var isWindowClosed = false;
            var newClient = Dispatcher.Invoke(() => new NewClient());
            newClient.Closed += (s, args) => isWindowClosed = true;
            Dispatcher.Invoke(() => newClient.Show());
            Dispatcher.Invoke(() => lvZakaznici.Items.Refresh());

            while (!isWindowClosed) Thread.Sleep(100);

            Thread.Sleep(1000);
        });
        threadOpen.Start();
    }

    private void LV_Clients_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        var threadOpen = new Thread(() =>
        {
            zakaznik = Dispatcher.Invoke(() => (Zakaznik)lvZakaznici.SelectedItem);
            var autoWindow = Dispatcher.Invoke(() => new AutoWindow());
            Dispatcher.Invoke(() => seznamVybranyZakaznik.Clear());
            Dispatcher.Invoke(() => seznamVybranyZakaznik.Add(zakaznik));
            Dispatcher.Invoke(() => autoWindow.Show());
        });
        threadOpen.Start();
    }

    private void LV_Clients_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
    }

    private async void Remove_Button_Click(object sender, RoutedEventArgs e)
    {
        var selectedItem = (Zakaznik)lvZakaznici.SelectedItem;

        ZakaznikViewModel.Zakaznici.Remove(selectedItem);
        await RefreshListViewAsync();
        MessageBox.Show($"Uživatel {selectedItem.Jmeno} byl odstraněn.");
        Thread.Sleep(1000);
    }

    private async Task RefreshListViewAsync()
    {
        await Task.Delay(100); // Wait for 100ms to ensure that the UI has time to update
        CollectionViewSource.GetDefaultView(lvZakaznici.ItemsSource).Refresh();
    }
    private async void Edit_Button_Click(object sender, RoutedEventArgs e)
    {
        if (lvZakaznici.SelectedItems.Count > 0)
        {
            edit = true;
            zakaznik = (Zakaznik)lvZakaznici.SelectedItem;
            var editClientWindow = new NewClient();
            editClientWindow.Show();
            await Task.Run(() => lvZakaznici.Dispatcher.Invoke(() => lvZakaznici.Items.Refresh()));
        }
        else
        {
            MessageBox.Show("Undefined choice.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
    
    private void RefreshViewClick(object sender, RoutedEventArgs e)
    {
        try
        {
            lvZakaznici.ItemsSource = ZakaznikViewModel.Zakaznici;
            lvZakaznici.Items.Refresh();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}", "Error");
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

    private static async Task SaveDataAsync()
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
        var searchText = (sender as TextBox)!.Text.Trim();

        if (searchText.Length == 0)
        {
            lvZakaznici.ItemsSource = ZakaznikViewModel.Zakaznici;
            lvZakaznici.Items.Refresh();
            return;
        }

        var filteredList = ZakaznikViewModel.Zakaznici.Where(z =>
            z.Jmeno.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0 ||
            z.Prijmeni.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0 ||
            z.Email.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0);

        lvZakaznici.ItemsSource = filteredList;
        lvZakaznici.Items.Refresh();
    }

    private void Exit_Click(object sender, RoutedEventArgs e)
    {
        ExitApplication();
    }

    private static void ExitApplication()
    {
        Reminder();
        var result = MessageBox.Show("Are you sure you want to exit the application?", "Confirmation",
            MessageBoxButton.YesNo, MessageBoxImage.Question);
        if (result == MessageBoxResult.Yes) Application.Current.Shutdown();
    }

}