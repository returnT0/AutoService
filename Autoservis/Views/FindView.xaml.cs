using System.Linq;
using System.Threading;
using System.Windows;
using Autoservis.Model;
using Autoservis.Repository;
using Autoservis.ViewModel;

namespace Autoservis.Views;

public partial class FindView : Window
{
    public FindView()
    {
        InitializeComponent();
    }
    
    private void find_Click(object sender, RoutedEventArgs e)
    {
        Thread threadAdd = new Thread(() =>
        {
            Zakaznik zakaznik = FindZakaznikByEmail(email.Text);
            if (zakaznik != null)
            {
                ZakaznikView.zakaznik = zakaznik;
                ZakaznikView.edit = true;
                Dispatcher.Invoke(() => FindView.GetWindow(this)?.Close());
            }
            else
            {
                MessageBox.Show("Zákazník nebyl nalezen");
            }
        });
        threadAdd.Start();
    }
    
    public static Zakaznik FindZakaznikByEmail(string email)
    {
        return ZakaznikViewModel.Zakaznici.FirstOrDefault(z => z.Email == email);
    }
    
    private void konec_Click(object sender, RoutedEventArgs e)
    {
        Dispatcher.Invoke(() => FindView.GetWindow(this)?.Close());
    }
}