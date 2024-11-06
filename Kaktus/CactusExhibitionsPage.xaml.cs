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
    public partial class CactusExhibitionsPage : Page
    {
        private ConnectionClass connection = new ConnectionClass();

        public CactusExhibitionsPage()
        {
            InitializeComponent();
            LoadData();
        }

        // Загрузка данных для отображения
        private void LoadData()
        {
            // Загрузка кактусов в ComboBox
            CactusComboBox.ItemsSource = connection.db.Cacti.ToList();
            // Загрузка выставок в ComboBox
            ExhibitionComboBox.ItemsSource = connection.db.Exhibitions.ToList();
            // Загрузка связей кактусов и выставок в DataGrid
            LoadCactusExhibitions();
        }

        // Загрузка связей кактусов и выставок
        private void LoadCactusExhibitions()
        {
            var cactusExhibitions = connection.db.CactusExhibitions
                .Select(ce => new
                {
                    CactusExhibitionId = ce.Id,
                    CactusName = ce.Cacti.Вид,
                    ExhibitionName = ce.Exhibitions.Название
                }).ToList();

            CactusExhibitionsDataGrid.ItemsSource = cactusExhibitions;
        }

        // Добавление новой связи
        private void AddCactusExhibition_Click(object sender, RoutedEventArgs e)
        {
            var selectedCactus = (Cacti)CactusComboBox.SelectedItem;
            var selectedExhibition = (Exhibitions)ExhibitionComboBox.SelectedItem;

            if (selectedCactus != null && selectedExhibition != null)
            {
                var cactusExhibition = new CactusExhibitions
                {
                    CactusId = selectedCactus.Id,
                    ExhibitionId = selectedExhibition.Id
                };

                connection.db.CactusExhibitions.Add(cactusExhibition);
                connection.db.SaveChanges();
                LoadCactusExhibitions();
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите кактус и выставку.");
            }
        }

        // Удаление выбранной связи
        private void DeleteCactusExhibition_Click(object sender, RoutedEventArgs e)
        {
            var selectedCactusExhibition = (dynamic)CactusExhibitionsDataGrid.SelectedItem;
            if (selectedCactusExhibition != null)
            {
                int cactusExhibitionId = selectedCactusExhibition.CactusExhibitionId;

                var cactusExhibition = connection.db.CactusExhibitions
                    .FirstOrDefault(ce => ce.Id == cactusExhibitionId);

                if (cactusExhibition != null)
                {
                    connection.db.CactusExhibitions.Remove(cactusExhibition);
                    connection.db.SaveChanges();
                    LoadCactusExhibitions(); // Обновить список
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите связь для удаления.");
            }
        }

        // Переход в главное меню
        private void GoToMainMenu_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage());
        }
    }
}
