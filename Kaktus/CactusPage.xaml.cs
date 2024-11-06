using Kaktus.DBMODEL;
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

namespace Kaktus
{
    /// <summary>
    /// Логика взаимодействия для CactusPage.xaml
    /// </summary>
    public partial class CactusPage : Page
    {
        private ConnectionClass connection = new ConnectionClass();

        public CactusPage()
        {
            InitializeComponent();
            LoadCacti();
        }

        private void LoadCacti()
        {
            CactusDataGrid.ItemsSource = connection.db.Cacti.ToList();
        }

        // Добавление нового кактуса
        private void AddCactus_Click(object sender, RoutedEventArgs e)
        {
            // Логика добавления нового кактуса
            connection.AddCactus("Новый Кактус", "Мексика", 3, 150.50m, "Регулярный полив");
            LoadCacti();
        }

        // Удаление выбранного кактуса
        private void DeleteCactus_Click(object sender, RoutedEventArgs e)
        {
            var selectedCactus = (Cacti)CactusDataGrid.SelectedItem;
            if (selectedCactus != null)
            {
                connection.db.Cacti.Remove(selectedCactus);
                connection.db.SaveChanges();
                LoadCacti();  // Обновить список
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите кактус для удаления.");
            }
        }

        // Переход в главное меню
        private void GoToMainMenu_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage());
        }
    }

}
