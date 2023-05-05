using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
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
    private string searchTerm;

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
    public string PlaceholderText { get; set; }

    private void Add_Button_Click(object sender, RoutedEventArgs e)
    {
        var threadOpen = new Thread(() =>
        {
            var oknoNovyKlient = Dispatcher.Invoke(() => new NovyKlient());
            Dispatcher.Invoke(() => oknoNovyKlient.Show());
            Dispatcher.Invoke(() => lvZakaznici.Items.Refresh());
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
    private void Remove_Button_Click(object sender, RoutedEventArgs e)
    {
        var threadDelete = new Thread(() =>
        {
            Dispatcher.Invoke(() => ZakaznikViewModel.Zakaznici.Remove((Zakaznik)lvZakaznici.SelectedItem));
        });
        threadDelete.Start();
    }

    //Edit
    private void Edit_Button_Click(object sender, RoutedEventArgs e)
    {
        var threadEdit = new Thread(() =>
        {
            if (Dispatcher.Invoke(() => lvZakaznici.SelectedItems.Count > 0))
            {
                edit = true;
                zakaznik = Dispatcher.Invoke(() => (Zakaznik)lvZakaznici.SelectedItem);
                var oknoNovyKlient = Dispatcher.Invoke(() => new NovyKlient());
                Dispatcher.Invoke(() => oknoNovyKlient.Show());
                Dispatcher.Invoke(() => lvZakaznici.Items.Refresh());
            }
            else
            {
                MessageBox.Show("Není vybráno");
            }
        });
        threadEdit.Start();
    }

    //Save to db
    private void Save_Button_Click(object sender, RoutedEventArgs e)
    {
        var threadUloz = new Thread(() =>
        {
            Dispatcher.Invoke(() => klientMng.RemoveAllKlient());
            Dispatcher.Invoke(() => autoMng.RemoveAllAuto());
            Dispatcher.Invoke(() => servisMng.RemoveAllServis());
            Dispatcher.Invoke(() => cenaMng.RemoveAllCena());

            Dispatcher.Invoke(() => klientMng.AddAllKlient(ZakaznikViewModel.Zakaznici));
            Dispatcher.Invoke(() => autoMng.AddAllAuto(AutoViewModel.Auta));
            Dispatcher.Invoke(() => servisMng.AddAllServis(ServisViewModel.SeznamServisu));
            Dispatcher.Invoke(() => cenaMng.AddAllCena(CenaViewModel.SeznamCenaServisu));
        });
        MessageBox.Show("Uloženo", "Uložení");
        threadUloz.Start();
    }

    //Nacti z db
    // private void Save_Button_Click(object sender, RoutedEventArgs e)
    // {
    //     OpenFileDialog openFileDialog = new OpenFileDialog();
    //     if (openFileDialog.ShowDialog() == true)
    //     {
    //         string filePath = openFileDialog.FileName;
    //         SQLiteConnection connection = new SQLiteConnection("Data Source=" + filePath + ";Version=3;");
    //         connection.Open();
    //         SQLiteCommand command = new SQLiteCommand(connection);
    //
    //         // Remove all existing data from the tables
    //         command.CommandText = "DELETE FROM ZakaznikViewModel";
    //         command.ExecuteNonQuery();
    //         command.CommandText = "DELETE FROM AutoViewModel";
    //         command.ExecuteNonQuery();
    //         command.CommandText = "DELETE FROM ServisViewModel";
    //         command.ExecuteNonQuery();
    //         command.CommandText = "DELETE FROM CenaViewModel";
    //         command.ExecuteNonQuery();
    //
    //         // Insert the new data into the tables
    //         foreach (Zakaznik zakaznik in ZakaznikViewModel.Zakaznici)
    //         {
    //             command.CommandText = "INSERT INTO Klient (id, jmeno, prijmeni, telefon, email, adresa, poznamky, auta) " +
    //                                   "VALUES (@id, @jmeno, @prijmeni, @telefon, @email, @adresa, @poznamky, @auta)";
    //             command.Parameters.AddWithValue("@id", zakaznik.Id);
    //             command.Parameters.AddWithValue("@Jmeno", zakaznik.Jmeno);
    //             command.Parameters.AddWithValue("@Prijmeni", zakaznik.Prijmeni);
    //             command.Parameters.AddWithValue("@Telefon", zakaznik.Telefon);
    //             command.Parameters.AddWithValue("@Email", zakaznik.Email);
    //             command.Parameters.AddWithValue("@Adresa", zakaznik.Adresa);
    //             command.Parameters.AddWithValue("@Poznamky", zakaznik.Poznamky);
    //             command.Parameters.AddWithValue("@idKlienta", zakaznik.auta);
    //
    //             command.ExecuteNonQuery();
    //             command.Parameters.Clear();
    //         }
    //
    //         foreach (Auto auto in AutoViewModel.Auta)
    //         {
    //             command.CommandText =
    //                 "INSERT INTO Auto (idVozu, znackaVozu, modelVozu, spz, vin, barva) " +
    //                 "VALUES (@idVozu, @znackaVozu, @modelVozu, @spz, @vin, @barva)";
    //             command.Parameters.AddWithValue("@idVozu", auto.IdVozu);
    //             command.Parameters.AddWithValue("@znackaVozu", auto.ZnackaVozu);
    //             command.Parameters.AddWithValue("@modelVozu", auto.ModelVozu);
    //             command.Parameters.AddWithValue("@spz", auto.Spz);
    //             command.Parameters.AddWithValue("@vin", auto.Vin);
    //             command.Parameters.AddWithValue("@barva", auto.Barva);
    //             
    //             command.ExecuteNonQuery();
    //             command.Parameters.Clear();
    //         }
    //
    //         foreach (Servis servis in ServisViewModel.SeznamServisu)
    //         {
    //             command.CommandText =
    //                 "INSERT INTO Servis (idServis, idAuto, zavada, datumServiu, tachometr, plnostNadrze, cena) " +
    //                 "VALUES (@idServis, @idAuto, @zavada, @datumServiu, @tachometr, @plnostNadrze, @cena)";
    //             command.Parameters.AddWithValue("@idServis", servis.IdServis);
    //             command.Parameters.AddWithValue("@idAuto", servis.IdAuto);
    //             command.Parameters.AddWithValue("@zavada", servis.Zavada);
    //             command.Parameters.AddWithValue("@datumServiu", servis.DatumServisu);
    //             command.Parameters.AddWithValue("@tachometr", servis.Tachometr);
    //             command.Parameters.AddWithValue("@plnostNadrze", servis.PlnostNadrze);
    //             command.Parameters.AddWithValue("@cena", servis.Cena);
    //             command.ExecuteNonQuery();
    //             command.Parameters.Clear();
    //         }
    //
    //         foreach (Cena cena in CenaViewModel.SeznamCenaServisu)
    //         {
    //             command.CommandText = "INSERT INTO Cena (item, cena, idServis) " +
    //                                   "VALUES (@item, @cena, @idServis)";
    //             command.Parameters.AddWithValue("@item", cena.Item);
    //             command.Parameters.AddWithValue("@cena", cena.CenaPolozky);
    //             command.Parameters.AddWithValue("@idServis", cena.idServis);
    //             
    //             command.ExecuteNonQuery();
    //             command.Parameters.Clear();
    //         }
    //
    //         connection.Close();
    //         MessageBox.Show("Data byla úspěšně nahrána", "Nahráno");
    //     }
    // }
    private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        var searchText = (sender as TextBox).Text.ToLower();
        var filteredList = ZakaznikViewModel.Zakaznici.Where(z =>
            z.Jmeno.ToLower().Contains(searchText) ||
            z.Prijmeni.ToLower().Contains(searchText) ||
            z.Email.ToLower().Contains(searchText));
        lvZakaznici.ItemsSource = filteredList;
        Dispatcher.Invoke(() => lvZakaznici.Items.Refresh());
    }
}