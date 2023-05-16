using System;
using System.Collections.Generic;
using System.Linq;
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

    public partial class EditBank : Page
    {
        private readonly People _people = new People();
        private string[] Genders = new string[] { "Мужской", "Женский" };

        public EditBank()
        {
            InitializeComponent();
            GenderCB.ItemsSource = Genders;
            CityCB.ItemsSource = PeopleBankDB.Context.People.Select(x => x.Name).ToList();
            
            _people = new People();
            DataContext = _people;
        }

        public EditBank(People selectedPeople) : this()
        {
            if (selectedPeople != null)
            {
                _people = selectedPeople;
            }
            DataContext = _people; 
        }
            private void SaveChanges()
            {
                StringBuilder error = new StringBuilder();
                if (String.IsNullOrEmpty(_people.Surname)) error.AppendLine("Фамилия не может быть пустой.");
                if (String.IsNullOrEmpty(_people.Name)) error.AppendLine("Имя не может быть пустым.");
                if (String.IsNullOrEmpty(_people.Panronymic)) error.AppendLine("Отчество не может быть пустым.");
                if (String.IsNullOrEmpty(_people.PassportData)) error.AppendLine("Данные паспорта не могут быть пустыми.");
                if (_people.BirthDate == null || _people.BirthDate == DateTime.MinValue) error.AppendLine("Дата рождения не может быть пустой.");
                if (String.IsNullOrEmpty(_people.PhoneNumber)) error.AppendLine("Номер телефона не может быть пустым.");
                if (String.IsNullOrEmpty(_people.Gender)) error.AppendLine("Пол не может быть пустым.");
                if (_people.City == null || String.IsNullOrEmpty(_people.City.Name)) error.AppendLine("Город не может быть пустым.");
                if (String.IsNullOrEmpty(_people.Street)) error.AppendLine("Улица не может быть пустой.");
                if (String.IsNullOrEmpty(_people.Building)) error.AppendLine("Номер дома не может быть пустым.");
                if (error.Length > 0)
                {
                    MessageBox.Show(error.ToString());
                    return;
                }

                if (_people.PeopleId == 0)
                {
                    PeopleBankDB.Context.People.Add(_people);
                }

                try
                {
                    PeopleBankDB.Context.SaveChanges();
                    MessageBox.Show("Данные сохранены!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
           

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveChanges();
                MessageBox.Show("Данные сохранены!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }

}


