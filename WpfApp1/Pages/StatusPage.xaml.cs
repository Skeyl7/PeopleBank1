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

    public partial class CityPage : Page
    {
        private Status _status = new Status();

        public CityPage(Status selectedStatus)
        {
            InitializeComponent();
            if (selectedStatus != null)
            {
                _status = selectedStatus;
            }
            DataContext = _status;
        }
        private void SaveChanges()
        {
            StringBuilder error = new StringBuilder();
            if (String.IsNullOrEmpty(_status.Name)) error.AppendLine("Укажите статус!");
      
            if (error.Length > 0)
            {
                MessageBox.Show(error.ToString());
                return;
            }

            if (_status.Status_Id == 0)
            {
                PeopleBankDB.Context.Status.Add(_status);
            }

            PeopleBankDB.Context.SaveChanges();

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