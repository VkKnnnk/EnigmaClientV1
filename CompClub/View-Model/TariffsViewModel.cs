using CompClub.Model;
using CompClub.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CompClub.View_Model
{
    class TariffsViewModel
    {
        public static ObservableCollection<Tarrifs> programms { get; set; } = Session.Context.Tarrifs.Local;
        public static ObservableCollection<Sessions> sessions { get; set; } = Session.Context.Sessions.Local;

        public static List<Tarrifs> Load(List<Tarrifs> ls)
        {
            DateTime current_date = DateTime.Now;
            int dayofweek = (int)current_date.DayOfWeek;
            if (dayofweek == 6 || dayofweek == 7)
            {
                ls = ls.Where(x => x.name.Contains("Выходные дни")).ToList();
            }
            else
                ls = ls.Where(x => x.name.Contains("Будние дни")).ToList();
            TimeSpan now = DateTime.Now.TimeOfDay;
            if (now.TotalSeconds < 86399 && now.TotalSeconds > 82800)
            {
                return ls.Where(x => x.name.Contains("Ночной пакет")).ToList();
            }
            else if (now.TotalSeconds < 25200)
                return ls.Where(x => x.name.Contains("Ночной пакет")).ToList();
            else
                return ls.Where(x => !x.name.Contains("Ночной пакет")).ToList();
        }
        public static bool AddSession(Users user, Tarrifs tarrif)
        {
            Session.Context.Tarrifs.Load();
            Session.Context.Sessions.Load();
            Session.Context.Users.Load();
            if (user.cash < tarrif.cost)
                MyMessageBox.Show("no_cash", "Не хватает средств, пополните баланс", MessageBoxButton.OK);
            else if (sessions.Where(x => x.status == true).Select(x => x.userId).Contains(user.userId))
            {
                if (sessions.Where(x => x.status == true).Where(x => x.userId == user.userId).Select(x => x.Tarrifs.tariff_typeId).FirstOrDefault() != tarrif.tariff_typeId)
                    MyMessageBox.Show("", "У вас уже приобритен другой тариф, дождесь его окончания.", MessageBoxButton.OK);
                //добавление времени к существующей активной сессии
                else if (sessions.Select(x => x.userId).Contains(user.userId))
                {
                    var current_session = sessions.Where(x => x.userId == user.userId).Where(x => x.status == true).FirstOrDefault();
                    if (current_session != null)
                    {
                        current_session.end_session = current_session.end_session.AddHours(tarrif.duration);
                        user.cash -= tarrif.cost;
                        MyMessageBox.Show("no_cash", $"К тарифу добавлено {tarrif.duration} час(ов)", MessageBoxButton.OK);
                        Session.Context.SaveChanges();
                    }
                }
            }
            //создание новой сессии
            else
            {
                DateTime now = DateTime.Now;
                //У каждого пк в названии будет его уникальный идентификатор
                //Таким образом программа сможет определить, под каким ПК приобритен тариф и какой тариф предложить пользователю.
                string name_pc = System.Net.Dns.GetHostName();
                user.cash -= tarrif.cost;
                sessions.Add(
                    new Sessions
                    {
                        tariffId = tarrif.tarrifId,
                        userId = user.userId,
                        start_session = now,
                        end_session = now.AddHours(tarrif.duration),
                        computerId = 1,
                        status = true
                    });
                Session.Context.SaveChanges();
                MyMessageBox.Show("all_ok_tarrif", "Тариф успешно приобритен", MessageBoxButton.OK);
            }
            return true;
        }
    }
}
