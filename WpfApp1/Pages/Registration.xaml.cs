using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security.Cryptography;
using System.Security.Policy;
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
using System.Xml.Linq;

namespace PeopleBank.Pages
{

    public partial class Registration : Page
    {
        private readonly PeopleBank1Entities database;

        public Registration()
        {
            InitializeComponent();
            database = new PeopleBank1Entities();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTB.Text;
            string password = PasswordTB.Text;
            string confirmPassword = ConfirmPasswordTB.Text;
           
            if (new[] { login, password, confirmPassword }.Any(x => String.IsNullOrWhiteSpace(x)))
            {
                MessageBox.Show("Есть незаполненные поля", "Ошибка авторизации", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (new[] { login, password, confirmPassword }.Where(x => x.Length < 6).Any())
            {
                MessageBox.Show("Поля должны быть боьше 6", "Ошибка авторизации", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Пароли не совпадают", "Ошибка авторизации", MessageBoxButton.OK, MessageBoxImage.Information);
                PasswordTB.Text = "";
                ConfirmPasswordTB.Text = "";
                return;
            }

            if (UserExists(login))
            {
                MessageBox.Show("Пользователь с таким логином уже существует", "Ошибка регистрации", MessageBoxButton.OK, MessageBoxImage.Error);
                LoginTB.Text = "";
                return;
            }


            RegisterUser(new Users { Login = login, Password = password });

            MessageBox.Show("Вы успешно зарегистрировались", "Регистрация", MessageBoxButton.OK, MessageBoxImage.Information);
            NavigationService.Navigate(new Authorization());
        }

        private bool UserExists(string login)
        {
            return database.Users.FirstOrDefault(x => x.Login == login) != null;
        }

        private void RegisterUser(Users user)
        {
            try
            {
                using (var sha256 = SHA256.Create())
                {
                    var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(user.Password));
                    user.Password = Convert.ToBase64String(hashedBytes);
                }

                user.Name = "Пользователь";
                database.Users.Add(user);
                database.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }


    }
}

