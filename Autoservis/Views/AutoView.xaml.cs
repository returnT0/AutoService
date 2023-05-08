using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Autoservice.Model;
using Autoservice.ViewModel;

namespace Autoservice.Views;

/// <summary>
///     Interaction logic for AutoView.xaml
/// </summary>
public partial class AutoView : UserControl

{
    public static readonly ObservableCollection<Auto?> AutoList = new();
    public static Auto? Auto;
    public static bool Edit;

    public AutoView()
    {
        InitializeComponent();
        lvZakaznik.ItemsSource = Dispatcher.Invoke(() => ClientsView.ClientsList);
        lvAuta.ItemsSource =
            Dispatcher.Invoke(() => AutoViewModel.Auta.Where(x => x.IdKlienta == ClientsView.Zakaznik!.Id));
        Dispatcher.Invoke(() => lvAuta.Items.Refresh());
    }

    private void lvAuta_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        var threadDoubleClick = new Thread(() =>
        {
            if (Dispatcher.Invoke(() => lvAuta.SelectedItems.Count > 0))
            {
                Auto = Dispatcher.Invoke(() => (Auto)lvAuta.SelectedItem);
                var oknoSeznamServis = Dispatcher.Invoke(() => new ServiceWindow());
                Dispatcher.Invoke(() => AutoList.Clear());
                Dispatcher.Invoke(() => AutoList.Add(Auto));
                Dispatcher.Invoke(() => oknoSeznamServis.Show());
            }
            else
            {
                MessageBox.Show("Unspecified choice!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        });
        threadDoubleClick.Start();
    }

    private void AddButtonClick(object sender, RoutedEventArgs e)
    {
        var threadOpen = new Thread(() =>
        {
            var isWindowClosed = false;
            var newAutoWindow = Dispatcher.Invoke(() => new NewAuto());
            newAutoWindow.Closed += (s, args) => isWindowClosed = true;
            Dispatcher.Invoke(() => newAutoWindow.Show());
            while (!isWindowClosed) Thread.Sleep(100);
            Dispatcher.Invoke(() =>
                lvAuta.ItemsSource = AutoViewModel.Auta.Where(x => x.IdKlienta == ClientsView.Zakaznik.Id));
        });
        threadOpen.Start();
    }


    private async void RemoveButtonClick(object sender, RoutedEventArgs e)
    {
        if (lvAuta.SelectedItems.Count > 0)
        {
            var selectedItems = lvAuta.SelectedItems.Cast<Auto>().ToList();
            foreach (var item in selectedItems) AutoViewModel.Auta.Remove(item);

            await RefreshAutoListViewAsync();
        }
        else
        {
            MessageBox.Show("Unspecified choice!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private async Task RefreshAutoListViewAsync()
    {
        lvAuta.ItemsSource =
            await Task.Run(() => AutoViewModel.Auta.Where(x => x.IdKlienta == ClientsView.Zakaznik!.Id));
    }

    private async void EditButtonClick(object sender, RoutedEventArgs e)
    {
        if (lvAuta.SelectedItems.Count > 0)
        {
            Edit = true;
            Auto = (Auto)lvAuta.SelectedItem;
            var editAutoWindow = new NewAuto();
            editAutoWindow.ShowDialog();
            Edit = false;
            await Task.Run(() => lvAuta.Dispatcher.Invoke(() => lvAuta.Items.Refresh()));
        }
        else
        {
            MessageBox.Show("Unspecified choice!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }


    private void CloseWindow()
    {
        var parentWindow = Window.GetWindow(this);
        parentWindow?.Close();
    }

    private void BackToClientsClick(object sender, RoutedEventArgs e)
    {
        CloseWindow();
    }
    
    private void CloseAppClick(object sender, RoutedEventArgs e)
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