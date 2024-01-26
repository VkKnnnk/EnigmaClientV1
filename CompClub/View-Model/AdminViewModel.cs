using CompClub.Model;
using CompClub.View;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CompClub.View_Model
{
    public static class AdminViewModel
    {
        public static ObservableCollection<Checks> checks { get; set; } = Session.Context.Checks.Local;

        public static int EnableDisableCTRLALTDEL(int counter)
        {
            string subKey = @"Software\Microsoft\Windows\CurrentVersion\Policies\System";
            if (counter % 2 == 0)
            {
                try
                {
                    RegistryKey rk = Registry.CurrentUser;
                    RegistryKey sk1 = rk.OpenSubKey(subKey);
                    if (sk1 != null)
                        rk.DeleteSubKeyTree(subKey);
                    MessageBox.Show("Диспетчер задач активирован");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                RegistryKey regkey;
                string keyValueInt = "1";

                try
                {
                    regkey = Registry.CurrentUser.CreateSubKey(subKey);
                    regkey.SetValue("DisableTaskMgr", keyValueInt);
                    regkey.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

            }
            return counter;
        }
        public static void CloseProcesses()
        {
            CloseProcess.EndSession();
            MessageBox.Show("Процессы закрыты");
        }
        public static void Exit()
        {
            System.Windows.Application.Current.Shutdown();
        }
        public static void GenerateCheck()
        {
            string left = String.Empty;
            string right = String.Empty;
            string newcheck = String.Empty;
            Session.Context.Checks.Load();
            Random newrandom = new Random();
            do
            {
                newcheck = String.Empty;
                left = (newrandom.Next(00000, 99999)).ToString();
                right = (newrandom.Next(00000, 99999)).ToString();
                newcheck = left + right;
            }
            while (checks.Select(x => x.check_key).Contains(newcheck));
            MessageBox.Show(newcheck);
            checks.Add(new Checks { check_key = left + right });
            Session.Context.SaveChanges();
        }

        public static void DisableFileManagmentYandex()
        {
            string subKey = @"\SOFTWARE\Policies\YandexBrowser";

            RegistryKey regkey;
            string keyValueInt = "1";

            try
            {
                regkey = Registry.CurrentUser.CreateSubKey(subKey);
                regkey.SetValue("AllowFileSelectionDialogs", keyValueInt, RegistryValueKind.DWord);
                regkey.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
    }
}
