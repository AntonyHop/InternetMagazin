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
    /// Логика взаимодействия для All.xaml
    /// </summary>
    public partial class All : Window
    {
        public All(ICategoryService cs)
        {
            InitializeComponent();
            products.ItemsSource = cs.Products("id");
        }
    }
}
