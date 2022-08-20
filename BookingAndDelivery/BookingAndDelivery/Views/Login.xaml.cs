using BookingAndDelivery.Model;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace BookingAndDelivery.Views
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        static string[] arrHexa = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F" };
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = tbUsername.Text;
            string password = pwbPassword.Password;

            using (var db = new BookingAndTransferFoodsEntities())
            {
                try
                {
                    var user = db.Users.SingleOrDefault(u => u.UserName == username);

                    if (user != null)
                    {
                        var sha1 = new SHA1CryptoServiceProvider();
                        var bytesPwhashSalt = StringToByteArray(user.Password);
                        //SHA1 hash -> 20 byte => bytesPwhashSalt - 20 = byteSalt
                        var LenPwhash = 20;
                        var bytesPwhash = new byte[LenPwhash];
                        Array.Copy(bytesPwhashSalt, bytesPwhash, LenPwhash);

                        //hash password user input
                        var PwUserInput = ASCIIEncoding.ASCII.GetBytes(password);
                        var bytesPwUserHash = sha1.ComputeHash(PwUserInput);

                        // chuyển array byte to string
                        var strPwhash = ArrByteToString(bytesPwhash);
                        var strPwUserIpHash = ArrByteToString(bytesPwUserHash);

                        //Login Success
                        if (strPwhash == strPwUserIpHash)
                        {
                            //Role User 
                            switch (user.RoleID)
                            {
                                //Role Admin
                                case 1:
                                    break;
                                //Role Staff
                                case 2:
                                    break;
                                // Role Partner
                                case 3:
                                    Navigation form = new Navigation();
                                    App.Current.MainWindow.Close();
                                    form.Show();
                                    break;
                                // Role Customer
                                case 4:
                                    break;
                                // Role Driver
                                case 5:
                                    break;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Password incorrect!!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Username does not exist!");
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private string ArrByteToString(Byte[] arrByte)
        {
            StringBuilder sb = new StringBuilder();

            foreach (byte b in arrByte)
            {
                //get 4 bit first
                sb.Append(arrHexa[(b >> 4)]);
                //get 4 bit second
                sb.Append(arrHexa[(b & 15)]);
            }

            return sb.ToString();
        }

        private byte[] StringToByteArray(string str)
        {
            return Enumerable.Range(0, str.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(str.Substring(x, 2), 16))
                             .ToArray();
        }
    }
}
