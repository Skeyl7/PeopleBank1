using Microsoft.Win32;
using PeopleBank.Pages;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows;


namespace PeopleBank
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Content = new Authorization();
            Closing += ClosingRpoject;
        }
        private void ClosingRpoject(object sender, CancelEventArgs e)
        {
            const string message = "Закрыть?";
            const string caption = "Form closing";
            var result = MessageBox.Show(message, caption, MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
        }
        private static bool Tema = false;
        private void ResetTheme(object sender, RoutedEventArgs e)
        {
            Uri uri;
            if (!Tema)
                uri = new Uri(@"..\Resources\MyDictionary.xaml", UriKind.Relative);
            else uri = new Uri(@"..\Resources\Temka.xaml", UriKind.Relative);
            Tema = !(Tema);
            ResourceDictionary resDict = Application.LoadComponent(uri) as ResourceDictionary;
            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(resDict);
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (MainFrame.Content is Registration)
            {
                MainFrame.Navigate(new Authorization());
            }
            else if (MainFrame.Content is Bank)
            {
                MainFrame.Navigate(new Authorization());
            }
            else if (MainFrame.Content is EditBank)
            {
                MainFrame.Navigate(new Authorization());
            }
            else if (MainFrame.Content is PageForPeople )
            {
                MainFrame.Navigate(new Authorization());
            }

        }

        private void ExportClick(object sender, RoutedEventArgs e)
        {
            ExportData(path);
        }

        private void ImportClick(object sender, RoutedEventArgs e)
        {
            ImportData();
        }

        // Получение пути каталога в компьютере и соединение пути с файлом
        string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "exportFile.txt");

        private void ExportData(string path)
        {
            StreamWriter streamWriter = new StreamWriter(path);

            using (var context = new PeopleBank1Entities())
            {
                foreach (var element in context.Users)
                {
                    streamWriter.WriteLine($"{element.Name} {element.Login} ");
                }
            }
            streamWriter.Close();
            Process.Start("notepad.exe", path);
        }

        private void ImportData()
        {
            OpenFileDialog dialogWindow = new OpenFileDialog();
            dialogWindow.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            dialogWindow.ShowDialog();
            if (dialogWindow.FileName == "")
            {
                MessageBox.Show("Файл для импорта не выбран!");
            }
            else
            {
                string fileContent = File.ReadAllText(dialogWindow.FileName);
                MessageBox.Show(fileContent, "Содержимое файла");
            }
        }
    }
}


