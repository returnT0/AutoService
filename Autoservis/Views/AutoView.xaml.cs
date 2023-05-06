using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Autoservis.Model;
using Autoservis.ViewModel;

namespace Autoservis.Views;

/// <summary>
///     Interaction logic for AutoView.xaml
/// </summary>
public partial class AutoView : UserControl

{
    public static ObservableCollection<Auto> seznamVybraneAuto = new();
    public static Auto auto;
    public static bool edit;

    public AutoView()
    {
        InitializeComponent();
        lvZakaznik.ItemsSource = Dispatcher.Invoke(() => ZakaznikView.seznamVybranyZakaznik);
        lvAuta.ItemsSource =
            Dispatcher.Invoke(() => AutoViewModel.Auta.Where(x => x.IdKlienta == ZakaznikView.zakaznik.Id));
        Dispatcher.Invoke(() => lvAuta.Items.Refresh());
    }

    private void lvAuta_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        var threadDoubleClick = new Thread(() =>
        {
            if (Dispatcher.Invoke(() => lvAuta.SelectedItems.Count > 0))
            {
                auto = Dispatcher.Invoke(() => (Auto)lvAuta.SelectedItem);
                var oknoSeznamServis = Dispatcher.Invoke(() => new ServisWindow());
                Dispatcher.Invoke(() => seznamVybraneAuto.Clear());
                Dispatcher.Invoke(() => seznamVybraneAuto.Add(auto));
                Dispatcher.Invoke(() => oknoSeznamServis.Show());
            }
            else
            {
                MessageBox.Show("Není vybráno auto", "Chyba");
            }
        });
        threadDoubleClick.Start();
    }

    private void AddButtonClick(object sender, RoutedEventArgs e)
    {
        var threadOpen = new Thread(() =>
        {
            var isWindowClosed = false;
            var oknoNoveAuto = Dispatcher.Invoke(() => new NoveAuto());
            oknoNoveAuto.Closed += (s, args) => isWindowClosed = true;
            Dispatcher.Invoke(() => oknoNoveAuto.Show());
            while (!isWindowClosed) Thread.Sleep(100);
            Dispatcher.Invoke(() =>
                lvAuta.ItemsSource = AutoViewModel.Auta.Where(x => x.IdKlienta == ZakaznikView.zakaznik.Id));
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
            MessageBox.Show("Auto není vybráno", "Chyba");
        }
    }

    private async Task RefreshAutoListViewAsync()
    {
        lvAuta.ItemsSource =
            await Task.Run(() => AutoViewModel.Auta.Where(x => x.IdKlienta == ZakaznikView.zakaznik.Id));
    }

    private async void EditButtonClick(object sender, RoutedEventArgs e)
    {
        if (lvAuta.SelectedItems.Count > 0)
        {
            edit = true;
            auto = (Auto)lvAuta.SelectedItem;
            var oknoNoveAuto = new NoveAuto();
            oknoNoveAuto.ShowDialog();
            edit = false;
            await Task.Run(() => lvAuta.Dispatcher.Invoke(() => lvAuta.Items.Refresh()));
        }
        else
        {
            MessageBox.Show("Auto není vybráno", "Chyba");
        }
    }


    private void CloseWindow()
    {
        var parentWindow = Window.GetWindow(this);
        if (parentWindow != null) parentWindow.Close();
    }

    private void BackToZakazniki_Click(object sender, RoutedEventArgs e)
    {
        CloseWindow();
    }
    
}