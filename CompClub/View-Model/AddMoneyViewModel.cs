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
    public class AddMoneyViewModel
    {
        public static ObservableCollection<Users> users { get; set; } = Session.Context.Users.Local;
        public static void AddMoney(Users user, string money)
        {
            Session.Context.Users.Load();
            if (money == string.Empty)
                MyMessageBox.Show("invalid", "Неккоректная сумма", MessageBoxButton.OK);
            else
            {
                if (money.Contains(' '))
                    money = money.Replace(" ", "");
                if (money == string.Empty)
                    MyMessageBox.Show("invalid", "Неккоректная сумма", MessageBoxButton.OK);
                else
                {
                    user.cash += int.Parse(money);
                    Session.Context.SaveChanges();
                    MyMessageBox.Show("valid", "Ваш счёт пополнен", MessageBoxButton.OK);
                }

            }
        }
    }
}
