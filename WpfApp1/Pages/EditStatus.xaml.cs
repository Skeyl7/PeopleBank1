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
    /// Логика взаимодействия для EditStatus.xaml
    /// </summary>
    public partial class EditStatus: Page
    {
        public EditStatus()
        {
            InitializeComponent();
            DataGridStatus.ItemsSource = PeopleBankDB.Context.Status.ToList();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CityPage((sender as Button).DataContext as Status));
        }
        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                PeopleBankDB.Context.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
                DataGridStatus.ItemsSource = PeopleBankDB.Context.Status.ToList();
            }
            var view = CollectionViewSource.GetDefaultView(DataGridStatus.ItemsSource);
            view.Refresh();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CityPage(null));
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show($"Вы точно хотите удлить запись?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                var selectedItems = DataGridStatus.SelectedItems;
                if (selectedItems != null && selectedItems.Count > 0)
                {
                    foreach (var item in selectedItems.Cast<dynamic>().ToList())
                    {
                        PeopleBankDB.Context.Status.Remove(item);
                    }
                    PeopleBankDB.Context.SaveChanges();
                    DataGridStatus.ItemsSource = PeopleBankDB.Context.Status.ToList();
                }
            }

        }
    }
}