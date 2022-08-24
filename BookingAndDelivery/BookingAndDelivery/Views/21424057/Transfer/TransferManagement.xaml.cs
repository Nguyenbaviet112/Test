using BookingAndDelivery.Model.TransferMangement;
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

namespace BookingAndDelivery.Views.Transfer
{
    /// <summary>
    /// Interaction logic for TransferManagement.xaml
    /// </summary>
    public partial class TransferManagement : Page
    {
        private TransferDAO TransferDAO = new TransferDAO();
        public TransferManagement()
        {
            InitializeComponent();
            Init();
        }

        private void Refresh()
        {
            lvLstOrder.ItemsSource = TransferDAO.GetListOrder();
            lvLstTransfer.ItemsSource = TransferDAO.GetListTranferByID(int.Parse(Application.Current.Properties["UserID"].ToString()));
        }
        private void Init()
        {
            lvLstOrder.ItemsSource = TransferDAO.GetListOrder();
            lvLstTransfer.ItemsSource = TransferDAO.GetListTranferByID(int.Parse(Application.Current.Properties["UserID"].ToString()));
        }
        private void btnTransfer_Click(object sender, RoutedEventArgs e)
        {
            var ts = lvLstOrder.SelectedItem as OrderVM;
            if (ts != null)
            {
                var dia = DialogNhapDiem;
                dia.IsOpen = true;
                txtID.Text = ts.ID.ToString();
                txtAmount.Text = ts.Amount.ToString();
                txtFee.Text = ts.TransferFee.ToString();
                txtTotalAmount.Text = ts.TotalAmount.ToString();
                unfUpdate.Visibility = Visibility.Visible;
                btnTransfer1.Visibility = Visibility.Visible;
                btnTransfer2.Visibility = Visibility.Visible;
                btnTransfer1_Fix.Visibility = Visibility.Collapsed;
                btnTransfer2_Fix.Visibility = Visibility.Collapsed;
            }
        }

        private void btnTransfer_Fix_Click(object sender, RoutedEventArgs e)
        {
            var ts = lvLstOrder.SelectedItem as OrderVM;
            if (ts != null)
            {
                var dia = DialogNhapDiem;
                dia.IsOpen = true;
                txtID.Text = ts.ID.ToString();
                txtAmount.Text = ts.Amount.ToString();
                txtFee.Text = ts.TransferFee.ToString();
                txtTotalAmount.Text = ts.TotalAmount.ToString();
                unfUpdate.Visibility = Visibility.Visible;
                btnTransfer1.Visibility = Visibility.Collapsed;
                btnTransfer2.Visibility = Visibility.Collapsed;
                btnTransfer1_Fix.Visibility = Visibility.Visible;
                btnTransfer2_Fix.Visibility = Visibility.Visible;
            }
        }

        private void btnTransfer1_Click(object sender, RoutedEventArgs e)
        {
            var orderID = txtID.Text;
            var DriverID = Application.Current.Properties["UserID"].ToString();
            if (TransferDAO.ConfirmTransfer1(int.Parse(orderID), int.Parse(DriverID)))
            {
                DialogNhapDiem.IsOpen = false;
                MessageBox.Show("Xác nhận vận chuyển thành công");
                Refresh();
            }
            else
            {
                DialogNhapDiem.IsOpen = false;
                MessageBox.Show("Xác nhận vận chuyển thất bại");
                Refresh();
            }
        }

        private void btnTransfer2_Click(object sender, RoutedEventArgs e)
        {
            var orderID = txtID.Text;
            var DriverID = Application.Current.Properties["UserID"].ToString();
            if (TransferDAO.ConfirmTransfer2(int.Parse(orderID), int.Parse(DriverID)))
            {
                DialogNhapDiem.IsOpen = false;
                MessageBox.Show("Xác nhận vận chuyển thành công");
                Refresh();
            }
            else
            {
                DialogNhapDiem.IsOpen = false;
                MessageBox.Show("Xác nhận vận chuyển thất bại");
                Refresh();
            }
        }

        private void btnTransfer1_Fix_Click(object sender, RoutedEventArgs e)
        {
            var orderID = txtID.Text;
            var DriverID = Application.Current.Properties["UserID"].ToString();
            if (TransferDAO.ConfirmTransfer1_Fix(int.Parse(orderID), int.Parse(DriverID)))
            {
                DialogNhapDiem.IsOpen = false;
                MessageBox.Show("Xác nhận vận chuyển thành công");
                Refresh();
            }
            else
            {
                DialogNhapDiem.IsOpen = false;
                MessageBox.Show("Xác nhận vận chuyển thất bại");
                Refresh();
            }
        }

        private void btnTransfer2_Fix_Click(object sender, RoutedEventArgs e)
        {
            var orderID = txtID.Text;
            var DriverID = Application.Current.Properties["UserID"].ToString();
            if (TransferDAO.ConfirmTransfer2_Fix(int.Parse(orderID), int.Parse(DriverID)))
            {
                DialogNhapDiem.IsOpen = false;
                MessageBox.Show("Xác nhận vận chuyển thành công");
                Refresh();
            }
            else
            {
                DialogNhapDiem.IsOpen = false;
                MessageBox.Show("Xác nhận vận chuyển thất bại");
                Refresh();
            }
        }
    }
}
