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
    /// <summary>
    /// Логика взаимодействия для PageForPeople.xaml
    /// </summary>
    public partial class PageForPeople : Page
    {
        public PageForPeople()
        {
            InitializeComponent();
            MainDataGrid.ItemsSource = PeopleBankDB.Context.People.ToList().Select(x => new
            {

                Surname = x.Surname,
                Name = x.Name,
                Patronymic = x.Panronymic,
                BirthData = x.BirthDate,
                Cities = x.City,
                PassportData = x.PassportData,
                Gender = x.Gender,
                Phone = x.PhoneNumber,
                Status = x.Status,

            }).ToList();
        }
    }
}
