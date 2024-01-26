using CompClub.Model;
using CompClub.View_Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Diagnostics;
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
    /// Логика взаимодействия для Games.xaml
    /// </summary>
    public partial class Games : Page
    {
        public static ObservableCollection<Programms> programms { get; set; } = Session.Context.Programms.Local;
        public Games()
        {
            InitializeComponent();
            Session.Context.Programms.Load();
            list.ItemsSource = programms;
        }
        private void list_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
        }

        private void GamesPrButton_Click(object sender, RoutedEventArgs e)
        {
            list.ItemsSource = programms.Where(x => x.category == "Game");
        }

        private void ProgrammsButton_Click(object sender, RoutedEventArgs e)
        {
            list.ItemsSource = programms.Where(x => x.category == "Programm");
        }

        private void BrowsersPrButton_Click(object sender, RoutedEventArgs e)
        {
            list.ItemsSource = programms.Where(x => x.category == "Browser");
        }

        private void AllPrButton_Click(object sender, RoutedEventArgs e)
        {
            list.ItemsSource = programms;
        }

        private void OpenProgramm_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Programms temp = (Programms)list.SelectedItem;
            if (GamesViewModel.StartProgramm(temp.idProgramm))
                list.SelectedItem = null;
        }
    }
}
