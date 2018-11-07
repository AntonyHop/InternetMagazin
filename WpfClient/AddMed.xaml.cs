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
    /// Логика взаимодействия для AddMed.xaml
    /// </summary>
    public partial class AddMed : Window
    {
        ICategoryService svc;
        public AddMed(ICategoryService svc)
        {
            InitializeComponent();
            this.svc = svc;
            ReloadTable();
        }

        private void ReloadTable()
        {
            Products.ItemsSource = svc.Products("id");
            foreach(CategoryDTO c in svc.Categories())
            {
                Cats.Items.Add(c.Name);
            }
        }

        private void DataGrid_OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "CategoryId")
            {
                e.Column = null;
            }
            if (e.PropertyName == "ImgUrl")
            {
                e.Column = null;
            }
            if (e.PropertyName == "Category")
            {
                e.Column = null;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ProductDTO p = new ProductDTO() { Name = Pname.Text, Author = Proiz.Text, CategoryId = Cats.SelectedIndex + 1, Desc = Desc.Text, Price = Decimal.Parse( Price.Text) };
            svc.AddProduct(p);
            ReloadTable();
            Products.Items.Refresh();
        }
    }
}
