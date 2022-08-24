using BookingAndDelivery.Model.OrderManagement;
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

namespace BookingAndDelivery.Views.Orders
{
    /// <summary>
    /// Interaction logic for OrderManagement.xaml
    /// </summary>
    public partial class OrderManagement : Page
    {
        private OrderDAO OrderDAO = new OrderDAO();
        public OrderManagement()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            lvLstProduct.ItemsSource = OrderDAO.GetProducts();
            lvOrder.ItemsSource = OrderDAO.GetListOrder();
        }

        private void btnShowOrder_Click(object sender, RoutedEventArgs e)
        {
            var ts = lvLstProduct.SelectedItem as ListOfProduct;
            if (ts != null)
            {
                var dia = DialogNhapDiem;
                dia.IsOpen = true;
                txtID.Text = ts.ID.ToString();
                txtName.Text = ts.Name;
                txtPrice.Text = ts.Price.ToString();
                txtQuantity.Text = "0";
                unfUpdate.Visibility = Visibility.Visible;
                btnOrder.Visibility = Visibility.Visible;
                btnOrder_Fix.Visibility = Visibility.Collapsed;
            }
        }

        private void btnShowOrder_Fix_Click(object sender, RoutedEventArgs e)
        {
            var ts = lvLstProduct.SelectedItem as ListOfProduct;
            if (ts != null)
            {
                var dia = DialogNhapDiem;
                dia.IsOpen = true;
                txtID.Text = ts.ID.ToString();
                txtName.Text = ts.Name;
                txtPrice.Text = ts.Price.ToString();
                txtQuantity.Text = "0";
                unfUpdate.Visibility = Visibility.Visible;
                btnOrder.Visibility = Visibility.Collapsed;
                btnOrder_Fix.Visibility = Visibility.Visible;
            }
        }

        private void Refresh()
        {
            lvLstProduct.ItemsSource = OrderDAO.GetProducts();
            lvOrder.ItemsSource = OrderDAO.GetListOrder();
        }
        private void btnOrder_Click(object sender, RoutedEventArgs e)
        {
            var productid = txtID.Text;
            var quantity = txtQuantity.Text;
            long user = long.Parse(Application.Current.Properties["UserID"].ToString());
            var rs = OrderDAO.Order(int.Parse(productid), int.Parse(quantity), user, 20000);
            if(rs)
            {
                DialogNhapDiem.IsOpen = false;
                MessageBox.Show("Đặt hàng thành công");
                Refresh();
            }
            else
            {
                DialogNhapDiem.IsOpen = false;
                MessageBox.Show("Đặt hàng thất bại");
                Refresh();
            }
        }

        private void btnOrder_Fix_Click(object sender, RoutedEventArgs e)
        {
            var productid = txtID.Text;
            var quantity = txtQuantity.Text;
            long user = long.Parse(Application.Current.Properties["UserID"].ToString());
            var rs = OrderDAO.Order_Fix(int.Parse(productid), int.Parse(quantity), user, 20000);
            if (rs)
            {
                DialogNhapDiem.IsOpen = false;
                MessageBox.Show("Đặt hàng thành công");
                
                Refresh();
            }
            else
            {
                DialogNhapDiem.IsOpen = false;
                MessageBox.Show("Đặt hàng thất bại");
                Refresh();
            }
        }
    }
}
