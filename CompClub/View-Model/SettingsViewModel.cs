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
    public static class SettingsViewModel
    {
        public static ObservableCollection<Users> users { get; set; } = Session.Context.Users.Local;

        public static void ChangeData(Users user, string name, string oldpass, string newpass, bool change_name_or_pass)
        {
            Session.Context.Users.Load();
            if (name == String.Empty)
                MyMessageBox.Show("name_empty", "Поле 'Никнейм' пустое, заполните его", MessageBoxButton.OK);
            else if (change_name_or_pass == true)
            {
                if (oldpass == String.Empty)
                    MyMessageBox.Show("oldpass_empty", "Поле 'Старый пароль' пустое, заполните его", MessageBoxButton.OK);
                else if (HashingPassword.ToHash(oldpass) != user.AuthData.password)
                    MyMessageBox.Show("incorrect_oldpass", "Неверный пароль", MessageBoxButton.OK);
                else if (newpass == String.Empty)
                    MyMessageBox.Show("newpass_empty", "Поле 'Новый пароль' пустое, заполните его", MessageBoxButton.OK);
                else
                {
                    user.name = name;
                    user.AuthData.password = newpass;
                    Session.Context.SaveChanges();
                    MyMessageBox.Show("all_ok", "Данные изменены", MessageBoxButton.OK);
                }
            }
            else
            {
                user.name = name;
                Session.Context.SaveChanges();
                MyMessageBox.Show("all_ok", "Данные изменены", MessageBoxButton.OK);
            }
        }
    }
}
