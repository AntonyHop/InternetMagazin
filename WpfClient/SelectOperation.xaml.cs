using InternetMagazine.PL.DTO;
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
using System.Windows.Shapes;

namespace WpfClient
{
    /// <summary>
    /// Логика взаимодействия для SelectOperation.xaml
    /// </summary>
    public partial class SelectOperation : Window
    {
        UserDTO user = null;
        ICategoryService CatSvc;
        IOrderService OrSvc;

        public SelectOperation(UserDTO user,ICategoryService catSvc,IOrderService OrSvc)
        {
            InitializeComponent();
            this.user = user;
            this.CatSvc = catSvc;
            this.OrSvc = OrSvc;

            Username.Content += " "+user.NickName;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void GetMed_Click(object sender, RoutedEventArgs e)
        {
            new AddMed(CatSvc).Show();
        }

        private void SetMed_Click(object sender, RoutedEventArgs e)
        {
            MakeOrder mo = new MakeOrder(OrSvc, CatSvc, user);
            mo.Show();
        }

        private void AllMed_Click(object sender, RoutedEventArgs e)
        {
            new All(CatSvc).Show();
        }
    }
}
