using CompClub.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Security.Principal;
using System.Diagnostics;
using CompClub.View_Model;

namespace CompClub
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static ObservableCollection<Sessions> sessions { get; set; } = Session.Context.Sessions.Local;

        protected override void OnStartup(StartupEventArgs e)
        {
            AdminViewModel.EnableDisableCTRLALTDEL(1);
            AdminViewModel.DisableFileManagmentYandex();
            Session.Context.Sessions.Load();
            var valid_sessions = sessions.Where(x => x.status == true).ToList();
            foreach (var item in sessions)
            {
                if (item.end_session < DateTime.Now)
                    item.status = false;
            }
            Session.Context.SaveChanges();
            base.OnStartup(e);
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            AdminViewModel.EnableDisableCTRLALTDEL(0);
        }
    }
}
