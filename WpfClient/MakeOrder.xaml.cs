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
    /// Логика взаимодействия для MakeOrder.xaml
    /// </summary>
    public partial class MakeOrder : Window
    {
        IOrderService os;
        ICategoryService cs;
        List<OrderItemDTO> order = new List<OrderItemDTO>();
        UserDTO user;

        public MakeOrder(IOrderService os, ICategoryService cs,UserDTO user)
        {
            InitializeComponent();
            this.cs = cs;
            this.os = os;
            this.user = user;
            OrderTable.ItemsSource = order;

            foreach(ProductDTO p in cs.Products("id"))
            {
                SelectProduct.Items.Add(p.Name);
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            OrderItemDTO orderitem = new OrderItemDTO();
            orderitem.Product = (ProductDTO) cs.Search((String)SelectProduct.SelectedValue).ToArray()[0];
            orderitem.User = user;
            order.Add(orderitem);
            OrderTable.Items.Refresh();

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
    }
}
