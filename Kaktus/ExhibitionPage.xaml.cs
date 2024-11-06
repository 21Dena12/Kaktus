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
    public partial class ExhibitionPage : Page
    {
        private ConnectionClass connection = new ConnectionClass();

        public ExhibitionPage()
        {
            InitializeComponent();
            LoadExhibitions();
        }

        private void LoadExhibitions()
        {
            ExhibitionDataGrid.ItemsSource = connection.db.Exhibitions.ToList();
        }

        // Добавление новой выставки
        private void AddExhibition_Click(object sender, RoutedEventArgs e)
        {
            connection.db.Exhibitions.Add(new Exhibitions
            {
                Название = "Новая выставка",
                Дата = DateTime.Now,
                Место_проведения = "Москва",
                Награды = "Лучший кактус",
                Комментарии = "Прошла успешно"
            });
            connection.db.SaveChanges();
            LoadExhibitions();
        }

        // Удаление выбранной выставки
        private void DeleteExhibition_Click(object sender, RoutedEventArgs e)
        {
            var selectedExhibition = (Exhibitions)ExhibitionDataGrid.SelectedItem;
            if (selectedExhibition != null)
            {
                connection.db.Exhibitions.Remove(selectedExhibition);
                connection.db.SaveChanges();
                LoadExhibitions();  // Обновить список
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите выставку для удаления.");
            }
        }

        // Переход в главное меню
        private void GoToMainMenu_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage());
        }
    }
}