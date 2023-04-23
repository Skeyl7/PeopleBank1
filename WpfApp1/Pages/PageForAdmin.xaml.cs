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
    /// Логика взаимодействия для PageForAdmin.xaml
    /// </summary>
    public partial class PageForAdmin : Page
    {
        public PageForAdmin()
        {
            InitializeComponent();
        }
        private void Bank_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Bank());
        }

        private void EditBank_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditBank());
        }
    }

}
