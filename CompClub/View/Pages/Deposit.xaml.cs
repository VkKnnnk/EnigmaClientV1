using CompClub.Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CompClub.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для Deposit.xaml
    /// </summary>
    public partial class Deposit : Page
    {
        public Deposit()
        {
            InitializeComponent();
        }

        private void EnterDeposButton_Click(object sender, RoutedEventArgs e)
        {
            AddMoneyViewModel.AddMoney(MainWindow.global_user, MoneyText.Text);
        }
        private void MoneyText_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "0123456789".IndexOf(e.Text) < 0;
        }
    }
}
