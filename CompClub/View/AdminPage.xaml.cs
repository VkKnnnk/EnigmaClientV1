using CompClub.View_Model;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace CompClub.View
{
    /// <summary>
    /// Логика взаимодействия для AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Window
    {
        int counter = 0;
        public AdminPage()
        {
            InitializeComponent();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            AdminViewModel.Exit();
        }

        private void CloseProcesses_Click(object sender, RoutedEventArgs e)
        {
            AdminViewModel.CloseProcesses();
        }

        private void BanUnBanAdminPanelButton_Click(object sender, RoutedEventArgs e)
        {
            if (AdminViewModel.EnableDisableCTRLALTDEL(counter) % 2 == 0)
            {
                BanUnBanAdminPanelButton.Background = Brushes.Red;
                BanUnBanAdminPanelButton.Content = "Заблокировать диспетчер задач";
            }
            else
            {
                BanUnBanAdminPanelButton.Background = Brushes.Green;
                BanUnBanAdminPanelButton.Content = "Разблокировать диспетчер задач";
            }
            counter++;
        }

        private void GenerateCheck_Click(object sender, RoutedEventArgs e)
        {
            AdminViewModel.GenerateCheck();
        }
    }
}
