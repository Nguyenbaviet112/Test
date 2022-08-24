using BookingAndDelivery.Model.ProductManagement;
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

namespace BookingAndDelivery.Views.Product
{
    /// <summary>
    /// Interaction logic for ProductManagement.xaml
    /// </summary>
    public partial class ProductManagement : Page
    {
        private ProductDAO ProductDAO = new ProductDAO();
        public ProductManagement()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            lvProduct.ItemsSource = ProductDAO.GetProducts();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            var ts = lvProduct.SelectedItem as ProductVM;
            if (ts != null)
            {
                var dia = DialogNhapDiem;
                dia.IsOpen = true;
                txtID.Text = ts.ID.ToString();
                txtName.Text = ts.Name;
                txtPrice.Text = ts.Price.ToString();
                unfUpdate.Visibility = Visibility.Visible;
                btnUpdateProduct.Visibility = Visibility.Visible;
                btnUpdateProduct_Fix.Visibility = Visibility.Collapsed;
            }
        }

    private void ReFeshList()
        {
            var lst = ProductDAO.GetProducts();

            lvProduct.ItemsSource = lst;
        }

        private void btnEdit_Fix_Click(object sender, RoutedEventArgs e)
        {
            var ts = lvProduct.SelectedItem as ProductVM;
            if (ts != null)
            {
                var dia = DialogNhapDiem;
                dia.IsOpen = true;
                txtID.Text = ts.ID.ToString();
                txtName.Text = ts.Name;
                txtPrice.Text = ts.Price.ToString();
                unfUpdate.Visibility = Visibility.Visible;
                btnUpdateProduct.Visibility = Visibility.Collapsed;
                btnUpdateProduct_Fix.Visibility = Visibility.Visible;
            }
        }

        private void btnUpdateProduct_Click(object sender, RoutedEventArgs e)
        {
            var id = txtID.Text;
            var price = txtPrice.Text;
            var m = ProductDAO.UpdatePrice(int.Parse(id), decimal.Parse(price));
            if (m)
            {
                var dia = DialogNhapDiem;
                dia.IsOpen = false;
                MessageBox.Show("Cập nhật thành công");
                ReFeshList();
            }
            else
            {
                var dia = DialogNhapDiem;
                dia.IsOpen = false;
                MessageBox.Show("Cập nhật thất bại");
            }
        }

        private void btnUpdateProduct_Fix_Click(object sender, RoutedEventArgs e)
        {
            var id = txtID.Text;
            var price = txtPrice.Text;
            var m = ProductDAO.UpdatePrice_Fix(int.Parse(id), decimal.Parse(price));
            if (m)
            {
                var dia = DialogNhapDiem;
                dia.IsOpen = false;
                MessageBox.Show("Cập nhật thành công");
                ReFeshList();
            }
            else
            {
                var dia = DialogNhapDiem;
                dia.IsOpen = false;
                MessageBox.Show("Cập nhật thất bại");
            }
        }
    }
}
