// Purpose: Contains logic for ClientsView.xaml

using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;
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
    public static readonly ObservableCollection<Zakaznik?> ClientsList = new();

    public static Zakaznik? Zakaznik;
    public static bool Edit;

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

        lvClients.ItemsSource = ZakaznikViewModel.Zakaznici;
    }

    public static KlientMng klientMng { get; set; }
    public static AutoMng autoMng { get; set; }
    public static ServisMng servisMng { get; set; }
    public static CenaMng cenaMng { get; set; }

    private void LV_Clients_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        var threadOpen = new Thread(() =>
        {
            Zakaznik = Dispatcher.Invoke(() => (Zakaznik)lvClients.SelectedItem);
            var autoWindow = Dispatcher.Invoke(() => new AutoWindow());
            Dispatcher.Invoke(() => ClientsList.Clear());
            Dispatcher.Invoke(() => ClientsList.Add(Zakaznik));
            Dispatcher.Invoke(() => autoWindow.Show());
        });
        threadOpen.Start();
    }

    private void LV_Clients_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
    }
    
    private async Task RefreshListViewAsync()
    {
        await Task.Delay(100); // Wait for 100ms to ensure that the UI has time to update
        CollectionViewSource.GetDefaultView(lvClients.ItemsSource).Refresh();
    }

    private void Add_Button_Click(object sender, RoutedEventArgs e)
    {
        var threadOpen = new Thread(() =>
        {
            var isWindowClosed = false;
            var newClient = Dispatcher.Invoke(() => new NewClient());
            newClient.Closed += (s, args) => isWindowClosed = true;
            Dispatcher.Invoke(() => newClient.Show());
            Dispatcher.Invoke(() => lvClients.Items.Refresh());

            while (!isWindowClosed) Thread.Sleep(100);

            Thread.Sleep(1000);
        });
        threadOpen.Start();
    }

    private async void Remove_Button_Click(object sender, RoutedEventArgs e)
    {
        var selectedItem = (Zakaznik)lvClients.SelectedItem;

        ZakaznikViewModel.Zakaznici.Remove(selectedItem);
        await RefreshListViewAsync();
        MessageBox.Show($"Uživatel {selectedItem.Jmeno} byl odstraněn.","Success", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    private async void Edit_Button_Click(object sender, RoutedEventArgs e)
    {
        if (lvClients.SelectedItems.Count > 0)
        {
            Edit = true;
            Zakaznik = (Zakaznik)lvClients.SelectedItem;
            var editClientWindow = new NewClient();
            editClientWindow.Show();
            await RefreshListViewAsync();
        }
        else
        {
            MessageBox.Show("Undefined choice.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
    
    private async void Save_Button_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            await SaveDataAsync();
            MessageBox.Show("Data saved successfully.", "Save", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error saving data: {ex.Message}", "Save Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
            lvClients.ItemsSource = ZakaznikViewModel.Zakaznici;
            lvClients.Items.Refresh();
            return;
        }

        var filteredList = ZakaznikViewModel.Zakaznici.Where(z =>
            z.Jmeno.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0 ||
            z.Prijmeni.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0 ||
            z.Email.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0);

        lvClients.ItemsSource = filteredList;
        lvClients.Items.Refresh();
    }
    
    private void RefreshViewClick(object sender, RoutedEventArgs e)
    {
        try
        {
            lvClients.ItemsSource = ZakaznikViewModel.Zakaznici;
            lvClients.Items.Refresh();

            // Create a new DispatcherTimer object
            var timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(0.5); // Set the delay time to .5 second
            timer.Tick += Timer_Tick; 

            timer.Start();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void Timer_Tick(object sender, EventArgs e)
    {
        var timer = (DispatcherTimer)sender;
        timer.Stop();

        MessageBox.Show("View refreshed successfully!", "Success",MessageBoxButton.OK, MessageBoxImage.Information);
    }
    
    private void Exit_Click(object sender, RoutedEventArgs e)
    {
        ExitApplication();
    }
    
    private static void Reminder()
    {
        MessageBox.Show("Don't forget to Save data!!", "Reminder", MessageBoxButton.OK, MessageBoxImage.Warning);
    }
    
    private static void ExitApplication()
    {
        Reminder();
        var result = MessageBox.Show("Are you sure you want to exit the application?", "Confirmation",
            MessageBoxButton.YesNo, MessageBoxImage.Question);
        if (result == MessageBoxResult.Yes) Application.Current.Shutdown();
    }
}