using CompClub.Model;
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
using CompClub.View.Pages;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Threading;
using CompClub.View_Model;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using Microsoft.Win32;

namespace CompClub.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow
    {
        ///////////////////////////////////////////////////<!--Код ниже скрывает приложение из панели задач-->///////////////////////////////////////////////
        #region Window styles
        [Flags]
        public enum ExtendedWindowStyles
        {
            // ...
            WS_EX_TOOLWINDOW = 0x00000080,
            // ...
        }

        public enum GetWindowLongFields
        {
            // ...
            GWL_EXSTYLE = (-20),
            // ...
        }

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowLong(IntPtr hWnd, int nIndex);

        public static IntPtr SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong)
        {
            int error = 0;
            IntPtr result = IntPtr.Zero;
            // Win32 SetWindowLong doesn't clear error on success
            SetLastError(0);

            if (IntPtr.Size == 4)
            {
                // use SetWindowLong
                Int32 tempResult = IntSetWindowLong(hWnd, nIndex, IntPtrToInt32(dwNewLong));
                error = Marshal.GetLastWin32Error();
                result = new IntPtr(tempResult);
            }
            else
            {
                // use SetWindowLongPtr
                result = IntSetWindowLongPtr(hWnd, nIndex, dwNewLong);
                error = Marshal.GetLastWin32Error();
            }

            if ((result == IntPtr.Zero) && (error != 0))
            {
                throw new System.ComponentModel.Win32Exception(error);
            }

            return result;
        }

        [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr", SetLastError = true)]
        private static extern IntPtr IntSetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll", EntryPoint = "SetWindowLong", SetLastError = true)]
        private static extern Int32 IntSetWindowLong(IntPtr hWnd, int nIndex, Int32 dwNewLong);

        private static int IntPtrToInt32(IntPtr intPtr)
        {
            return unchecked((int)intPtr.ToInt64());
        }

        [DllImport("kernel32.dll", EntryPoint = "SetLastError")]
        public static extern void SetLastError(int dwErrorCode);
        #endregion
        public Sessions current_session { get; set; } //глобальная переменная текущей сессии
        public static Users global_user { get; set; } //глобальная переменная текущего пользователя
        public ObservableCollection<Sessions> sessions { get; set; } = Session.Context.Sessions.Local;
        DispatcherTimer timer = new DispatcherTimer();
        public MainWindow(Users user)
        {
            InitializeComponent();
            Session.Context.Sessions.Load();
            HelperBindingClass bind = new HelperBindingClass();
            bind._user = user;
            Sessions current_session = null;
            current_session = sessions.Where(x => x.status == true).Where(x => x.userId == user.userId).FirstOrDefault();
            bind.session = current_session;
            global_user = user;
            this.DataContext = bind;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult mboxResult = MyMessageBox.Show("Exit", "Вы уверены, что хотите выйти из учетной записи?", MessageBoxButton.YesNo);
            if (mboxResult == MessageBoxResult.Yes)
            {
                this.Close();
                NavigationService.NavigateBack();
            }
        }

        private void TarifsNavigateButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new Uri("/View/Pages/Tarif.xaml", UriKind.Relative));
        }

        private void GamesNavigateButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new Uri("/View/Pages/Games.xaml", UriKind.Relative));
        }

        private void DepositButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new Uri("/View/Pages/Deposit.xaml", UriKind.Relative));

        }

        private void ActivateTariff_Click(object sender, RoutedEventArgs e)
        {
            if (sessions.Where(x => x.status == true).Select(x => x.userId).Contains(global_user.userId))
            {
                HelperBindingClass bind = new HelperBindingClass();
                bind._user = global_user;
                current_session = sessions.Where(x => x.status == true).Where(x => x.userId == global_user.userId).FirstOrDefault();
                current_session.end_session = DateTime.Now.AddHours(current_session.Tarrifs.duration);
                current_session.start_session = DateTime.Now;
                Session.Context.SaveChanges();
                bind.session = current_session;
                timer.Start();
                this.DataContext = bind;
                GamesNavigateButton.IsEnabled = true;
                ActivateTariff.Visibility = Visibility.Collapsed;
                TarrifStackPanel.Visibility = Visibility.Visible;
                TimerStackPanel.Visibility = Visibility.Visible;
            }
            else
                MyMessageBox.Show("no_tarif", "Чтобы активировать тариф, сначала его купите", MessageBoxButton.OK);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (sessions.Where(x => x.status == true).Select(x => x.userId).Contains(global_user.userId))
            {
                HelperBindingClass bind = new HelperBindingClass();
                bind._user = global_user;
                current_session = sessions.Where(x => x.status == true).Where(x => x.userId == global_user.userId).FirstOrDefault();
                bind.session = current_session;
                this.DataContext = bind;
                timer.Start();
                GamesNavigateButton.IsEnabled = true;
                ActivateTariff.Visibility = Visibility.Collapsed;
                TarrifStackPanel.Visibility = Visibility.Visible;
                TimerStackPanel.Visibility = Visibility.Visible;
            }
            else
            {
                ActivateTariff.Visibility = Visibility.Visible;
                GamesNavigateButton.IsEnabled = false;
                TarrifStackPanel.Visibility = Visibility.Collapsed;
                TimerStackPanel.Visibility = Visibility.Collapsed;
            }
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += dtTicker;

            WindowInteropHelper wndHelper = new WindowInteropHelper(this);

            int exStyle = (int)GetWindowLong(wndHelper.Handle, (int)GetWindowLongFields.GWL_EXSTYLE);

            exStyle |= (int)ExtendedWindowStyles.WS_EX_TOOLWINDOW;
            SetWindowLong(wndHelper.Handle, (int)GetWindowLongFields.GWL_EXSTYLE, (IntPtr)exStyle);
        }
        private void dtTicker(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            current_session = sessions.Where(x => x.status == true).Where(x => x.userId == global_user.userId).FirstOrDefault();
            if (current_session != null && DateTime.Now >= current_session.end_session)
            {
                ActivateTariff.Visibility = Visibility.Visible;
                GamesNavigateButton.IsEnabled = false;
                timer.Stop();
                current_session.status = false;
                TarrifStackPanel.Visibility = Visibility.Collapsed;
                TimerStackPanel.Visibility = Visibility.Collapsed;
                CloseProcess.EndSession();
                MainFrame.NavigationService.Navigate(new Uri("/View/Pages/Tarif.xaml", UriKind.Relative));
                MyMessageBox.Show("end_session", "Время действия сессии закончилось", MessageBoxButton.OK);
            }
            else if (current_session != null && Math.Round(DateTime.Now.TimeOfDay.TotalSeconds) == Math.Round(current_session.end_session.TimeOfDay.TotalSeconds - 5*60))
                MyMessageBox.Show("five_minutes_to_end", "До конца сессии осталось 5 минут", MessageBoxButton.OK);
                string show__time = now.AddSeconds(1).TimeOfDay.ToString().Substring(0, 8);
            TimerLabel.Content = show__time;
        }
        private void SettingButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new Uri("/View/Pages/Settings.xaml", UriKind.Relative));

        }
    }
}
