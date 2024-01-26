using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CompClub.Model;
using CompClub.View;

namespace CompClub.View_Model
{
    public class AuthViewModel
    {
        public static ObservableCollection<AuthData> authDatas { get; set; } = Session.Context.AuthData.Local;
        public static ObservableCollection<Checks> checks { get; set; } = Session.Context.Checks.Local;
        public static ObservableCollection<Users> user { get; set; } = Session.Context.Users.Local;


        public static bool Log_In(string login, string password, string check)
        {
            bool valid = false;
            if (login == "viktor")
                if (password == "viktor")
                {
                    AdminPage admin = new AdminPage();
                    admin.ShowDialog();
                    valid = true;
                }
                else { }
            else
            {
                Session.Context.AuthData.Load();
                Session.Context.Checks.Load();
                Session.Context.Users.Load();
                if (check != "")
                {
                    var valid_check = checks.Where(x => x.check_key == check).FirstOrDefault();
                    if (!checks.Select(x => x.check_key).Contains(check))
                        MyMessageBox.Show("msg", "Неккоректный чек", MessageBoxButton.OK);
                    else if (authDatas.Select(x => x.idCheck).Contains(valid_check.idCheck))
                    {
                        MyMessageBox.Show("msg", "Чек уже был использован", MessageBoxButton.OK);
                    }
                    else
                    {
                        RegPage rg = new RegPage(valid_check.idCheck);
                        rg.ShowDialog();
                    }
                }
                else
                {
                    if (authDatas.Select(x => x.login).Contains(login))
                    {
                        if (authDatas.Where(x => x.login == login).Select(x => x.password).First() == HashingPassword.ToHash(password))
                        {
                            Users user_cor = user.Where(x => x.authId == authDatas.Where(y => y.login == login).Select(y => y.authId).FirstOrDefault()).FirstOrDefault();
                            MainWindow mw = new MainWindow(user_cor);
                            NavigationService.NavigateTo(mw);
                            valid = true;
                        }
                        else
                            MyMessageBox.Show("msg", "Неверный логин или пароль", MessageBoxButton.OK);
                    }
                    else
                        MyMessageBox.Show("msg", "Неверный логин или пароль", MessageBoxButton.OK);

                }
            }
            return valid;
        }

        public static bool RegNewUser(string name, string login, string password, int idCheck)
        {
            bool valid = false;
            Session.Context.AuthData.Load();
            Session.Context.Users.Load();
            if (name == String.Empty)
                MyMessageBox.Show("null_name", "Поле 'Имя пользователя' пустое", MessageBoxButton.OK);
            else if (login == String.Empty)
                MyMessageBox.Show("null_login", "Поле 'Логин' пустое", MessageBoxButton.OK);
            else if (password == String.Empty)
                MyMessageBox.Show("null_password", "Поле 'Пароль' пустое", MessageBoxButton.OK);
            else
            {
                if (!authDatas.Select(x => x.login).Contains(login))
                {
                    if (login.IndexOf(' ') >= 0)
                    {
                        MyMessageBox.Show("invalid_login", "Логин содержит пробелы, измените поле 'Логин'", MessageBoxButton.OK);
                        return valid;
                    }
                    else
                    {
                        authDatas.Add(new AuthData() { login = login, password = HashingPassword.ToHash(password), idCheck = idCheck });
                        Session.Context.SaveChanges();
                        int idAuth = authDatas.FirstOrDefault(x => x.login == login).authId;
                        user.Add(new Users() { name = name, cash = 0, authId = idAuth });
                        Session.Context.SaveChanges();
                        valid = true;
                    }
                }
                else
                    MyMessageBox.Show("taked_login", "Логин уже занят, придумайте новый", MessageBoxButton.OK);
            }
            return valid;
        }
    }
}
