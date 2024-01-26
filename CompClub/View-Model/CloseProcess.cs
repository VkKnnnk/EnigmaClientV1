using CompClub.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CompClub.View_Model
{
    public static class CloseProcess
    {
        public static ObservableCollection<Programms> programms { get; set; } = Session.Context.Programms.Local;

        public static void EndSession()
        {
            Session.Context.Programms.Load();
            Process[] runningProcesses = Process.GetProcesses();
            var processes = programms.Select(x => x.process_name).ToList();
            foreach (Process process in runningProcesses)
            {
                foreach (var item in processes)
                    if (process.ProcessName == item)
                    {
                        process.Kill();
                    }
            }
        }
    }
}
