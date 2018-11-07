using InternetMagazine.PL.Infrastructure;
using InternetMagazine.PL.Interfaces;
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

namespace WpfClient
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IUserService userService;
        ICategoryService categoryService;
        IOrderService order;
        public MainWindow(IUserService userService,ICategoryService cats, IOrderService or)
        {
            this.userService = userService;
            this.categoryService = cats;
            this.order = or;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (userService.LoginVerify(Login.Text, Password.Password))
                {
                    new SelectOperation(userService.getUserByName(Login.Text),categoryService ,order).Show();
                }
                else
                {
                    Password.Background = Brushes.DarkRed;
                    Password.Foreground = Brushes.White;
                }
            }catch(UserNotFoundExaption ex)
            {
                Login.Background = Brushes.DarkRed;
                Password.Background = Brushes.DarkRed;
                Login.Foreground = Brushes.White;
                Password.Foreground = Brushes.White;
            }
            
        }

    }
}
