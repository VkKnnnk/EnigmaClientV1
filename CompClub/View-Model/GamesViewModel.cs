using CompClub.Model;
using CompClub.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CompClub.View_Model
{
    public class GamesViewModel
    {
        public static ObservableCollection<Programms> programms { get; set; } = Session.Context.Programms.Local;


        public static bool StartProgramm(int id)
        {
            bool is_proc_running = false;
            Programms selected_programm = programms.Where(x => x.idProgramm == id).Select(x => x).FirstOrDefault();
            Process[] runningProcesses = Process.GetProcesses();
            foreach (Process process in runningProcesses)
            {
                if (process.ProcessName == selected_programm.process_name)
                {
                    if (selected_programm.process_name == "steam")
                        Process.Start(selected_programm.file_path, "");
                    WinAPI.ShowWindow(process.MainWindowHandle, WinAPI.Consts.SHOWWINDOW.SW_MAXIMIZE);
                    WinAPI.SetForegroundWindow(process.MainWindowHandle);
                    is_proc_running = true;
                }
            }
            if (!is_proc_running)
            {
                try
                {
                    Process.Start(selected_programm.file_path, "");
                }
                catch
                {
                    MyMessageBox.Show("Ошибка","Возникла ошибка при запуске программы, возможно, она не установлена", MessageBoxButton.OK);
                }
            }
            return true;
        }

    }
}
