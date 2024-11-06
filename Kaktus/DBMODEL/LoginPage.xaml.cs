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

namespace Kaktus.DBMODEL
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
   public partial class LoginPage : Page
{
    private ConnectionClass connection = new ConnectionClass();

    public LoginPage()
    {
        InitializeComponent();
    }

    private void LoginButton_Click(object sender, RoutedEventArgs e)
    {
        string login = LoginTextBox.Text;
        string password = PasswordBox.Password;

        var user = connection.AuthenticateUser(login, password);
        if (user == null)
        {
            ErrorMessage.Text = "Неверный логин или пароль. Пожалуйста, попробуйте снова.";
        }
        else
        {
            // Приветствие пользователя и переход на главную страницу
            MessageBox.Show($"Добро пожаловать, {user.Логин}!");
            NavigationService.Navigate(new MainPage());
        }
    }
}
}
