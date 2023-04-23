using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
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

namespace PeopleBank.Pages
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Page
    {
        public Authorization()
        {
            InitializeComponent();

        }

        private void RegistrationClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Registration());
        }

        private void InputClick(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(LoginTB.Text) || String.IsNullOrEmpty(PasswordTB.Text))
            {
                MessageBox.Show("Есть незаполненные поля", "Ошибка авторизации", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            using (var db = new PeopleBank1Entities())
            {
                var user = db.Users.FirstOrDefault(u => u.Login == LoginTB.Text);

                if (user == null)
                {
                    MessageBox.Show("Пользователь с такими данными не найден!");
                    return;
                }

                using (var sha256 = SHA256.Create())
                {
                    var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(PasswordTB.Text));
                    var hashedPassword = Convert.ToBase64String(hashedBytes);

                    if (user.Password != hashedPassword)
                    {
                        MessageBox.Show("Неверный пароль");
                        return;
                    }
                }
                if (user.Name == "Пользователь")
                {

                    NavigationService.Navigate(new PageForPeople());
                }
                else if (user.Name == "Администратор")
                {

                    NavigationService.Navigate(new PageForAdmin());
                }
            }
        }
    }
}







 