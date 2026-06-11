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
using System.Windows.Shapes;
using WpfApp5;

namespace ShoeStore.Views
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
             string login = TbLogin.Text.Trim();
            string password = PbPassword.Password;
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                TbError.Text = "Введите логин и пароль";
                return;
            }

            using (var db = new shoestoretext())
            {
                var user = db.Users.FirstOrDefault(u => u.Login == login && u.Password == password);
                if (user != null)
                {
                    TbError.Text = "Неверный логин или пароль";
                    return;
                }
                App.CurrentUser = user;


                //App.CurrentUser = new Models.User { Fullname = "Текстовый", RoleId = 1 };
            OpenMainWindow();
        }

        private void BtnGuest_Click(object sender, RoutedEventArgs e)
        {
            App.CurrentUser = null;
            OpenMainWindow();
        }
        private void OpenMainWindow()
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();  
            this.Close();
        }
    }
}
