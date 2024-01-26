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
    /// Логика взаимодействия для Settings.xaml
    /// </summary>
    public partial class Settings : Page
    {
        public bool temp { get; set; }
        public Settings()
        {
            this.DataContext = MainWindow.global_user;
            temp = false;
            InitializeComponent();
        }

        private void ChangePassword_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            OldPassContainer.Visibility = Visibility.Visible;
            NewPassContainer.Visibility = Visibility.Visible;
            temp = true;
        }
        private void SaveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            SettingsViewModel.ChangeData(MainWindow.global_user, UserNameText.Text, OldPasswordText.Password, NewPasswordText.Password, temp);
        }
    }
}
