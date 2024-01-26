using CompClub.Model;
using CompClub.View_Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CompClub.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для Tarifs.xaml
    /// </summary>
    public partial class Tarifs : Page
    {
        public static ObservableCollection<Tarrifs> tarifs { get; set; } = Session.Context.Tarrifs.Local;
        public Tarifs()
        {
            InitializeComponent();
            Session.Context.Tarrifs.Load();
            Session.Context.Users.Load();
            list.ItemsSource = TariffsViewModel.Load(tarifs.Where(x => x.tariff_typeId == 1).ToList());
        }
        private void StandartTButton_Click(object sender, RoutedEventArgs e)
        {
            list.ItemsSource = TariffsViewModel.Load(tarifs.Where(x => x.tariff_typeId == 1).ToList());
        }
        private void StandartPTButton_Click(object sender, RoutedEventArgs e)
        {
            list.ItemsSource = TariffsViewModel.Load(tarifs.Where(x => x.tariff_typeId == 2).ToList());
        }
        private void CybersportTButton_Click(object sender, RoutedEventArgs e)
        {
            list.ItemsSource = TariffsViewModel.Load(tarifs.Where(x => x.tariff_typeId == 3).ToList());
        }
        private void AllStarTButton_Click(object sender, RoutedEventArgs e)
        {
            list.ItemsSource = TariffsViewModel.Load(tarifs.Where(x => x.tariff_typeId == 4).ToList());
        }
        private void VipTButton_Click(object sender, RoutedEventArgs e)
        {
            list.ItemsSource = TariffsViewModel.Load(tarifs.Where(x => x.tariff_typeId == 5).ToList());
        }
        private void list_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
        }
        private void MakeOrderButton_Click(object sender, RoutedEventArgs e)
        {
            Tarrifs temp = (Tarrifs)list.SelectedItem;
            if (TariffsViewModel.AddSession(MainWindow.global_user, temp))
                list.SelectedItem = null;
        }
    }
}
