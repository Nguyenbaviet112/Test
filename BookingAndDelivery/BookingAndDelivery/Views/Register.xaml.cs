using BookingAndDelivery.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace BookingAndDelivery.Views
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Page
    {
        static string[] arrHexa = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F" };

        public ObservableCollection<Dictionary> listDistrict { get; private set; }

        public ObservableCollection<Dictionary> listCity { get; private set; }

        public Register()
        {
            InitializeComponent();
            initData();
            this.DataContext = this;
        }

        private void initData()
        {
            try
            {
                using (var db = new BookingAndTransferFoodsEntities())
                {
                    listDistrict = new ObservableCollection<Dictionary>(db.Dictionaries.Where(x => x.Type == "DISTRICT").Select(x => x).ToList());
                    listCity = new ObservableCollection<Dictionary>(db.Dictionaries.Where(x => x.Type == "CITY").Select(x => x).ToList());
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCustomerRegister_Click(object sender, RoutedEventArgs e)
        {
            string username = tbCustomerUsername.Text;
            string password = pwbCustomerPassword.Password;
            string confirmpass = pwbCustomerConfirmPassword.Password;
            if (password.Equals(confirmpass))
            {
                // Hash password
                string strPwHash = hashPassword(password);

                try
                {
                    using (var db = new BookingAndTransferFoodsEntities())
                    {

                        var findUser = db.Users.SingleOrDefault(u => u.UserName == username);

                        if (findUser != null)
                        {
                            MessageBox.Show("Username already exist");
                        }
                        else
                        {
                            User user = new User();
                            user.UserName = username;
                            user.Password = strPwHash;
                            user.FullName = tbCustomerFullName.Text;
                            user.Phone = tbCustomerPhone.Text;
                            user.Email = tbCustomerEmail.Text;
                            user.Address = tbCustomerAddress.Text;
                            user.RoleID = 4;
                            db.Users.Add(user);

                            db.SaveChanges();
                            MessageBox.Show("Register success");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.InnerException.ToString());
                }
            }
            else
            {
                MessageBox.Show("Incorrect Confirm Password");
            }
        }

        private void btnPartnerRegister_Click(object sender, RoutedEventArgs e)
        {
            string username = tbPartnerUsername.Text;
            string password = pwbPartnerPassword.Password;
            string confirmpass = pwbPartnerConfirmPassword.Password;
            if (cbbPartnerCity.SelectedIndex > -1 && cbbPartnerDistrict.SelectedIndex > -1)
            {
                if (password.Equals(confirmpass))
                {
                    // Hash password
                    string strPwHash = hashPassword(password);

                    try
                    {
                        using (var db = new BookingAndTransferFoodsEntities())
                        {

                            var findUser = db.Users.SingleOrDefault(u => u.UserName == username);

                            if (findUser != null)
                            {
                                MessageBox.Show("Username already exist");
                            }
                            else
                            {
                                User user = new User();
                                user.UserName = username;
                                user.Password = strPwHash;
                                user.FullName = tbPartnerFullName.Text;
                                user.Representative = tbPartnerRepresentative.Text;
                                user.CityID = int.Parse(cbbPartnerCity.SelectedValue.ToString());
                                user.District = int.Parse(cbbPartnerDistrict.SelectedValue.ToString());
                                user.TypeTransferID = int.Parse(tbPartnerShippingType.Text);
                                user.NumOfOrder = int.Parse(tbPartnerNumOfOrderPerDay.Text);
                                user.NumBranch = int.Parse(tbPartnerBranchNumber.Text);
                                user.Address = tbPartnerBusinessAddress.Text;
                                user.Phone = tbPartnerPhone.Text;
                                user.Email = tbPartnerEmail.Text;
                                user.RoleID = 3;
                                db.Users.Add(user);

                                db.SaveChanges();
                                MessageBox.Show("Register success");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.InnerException.ToString());
                    }
                }
                else
                {
                    MessageBox.Show("Incorrect Confirm Password");
                }
            }
            else
            {
                MessageBox.Show("Please choose City And District");
            }
        }

        private void btnShipperRegister_Click(object sender, RoutedEventArgs e)
        {
            string username = tbShipperUsername.Text;
            string password = pwbShipperPassword.Password;
            string confirmpass = pwbShipperConfirmPassword.Password;
            if (cbbShipperCity.SelectedIndex > -1 && cbbShipperDistrict.SelectedIndex > -1)
            {
                if (password.Equals(confirmpass))
                {
                    // Hash password
                    string strPwHash = hashPassword(password);

                    try
                    {
                        using (var db = new BookingAndTransferFoodsEntities())
                        {

                            var findUser = db.Users.SingleOrDefault(u => u.UserName == username);

                            if (findUser != null)
                            {
                                MessageBox.Show("Username already exist");
                            }
                            else
                            {
                                User user = new User();
                                user.UserName = username;
                                user.Password = strPwHash;
                                user.FullName = tbShipperFullName.Text;
                                user.LicensePlates = tbShipperLicensePlates.Text;
                                user.Identity = tbShipperIdentityCard.Text;
                                user.CityID = int.Parse(cbbShipperCity.SelectedValue.ToString());
                                user.District = int.Parse(cbbShipperDistrict.SelectedValue.ToString());
                                user.Address = tbShipperAddress.Text;
                                user.Phone = tbShipperPhone.Text;
                                user.BankAccount = tbShipperATMCard.Text;
                                user.RoleID = 5;
                                db.Users.Add(user);

                                db.SaveChanges();
                                MessageBox.Show("Register success");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.InnerException.ToString());
                    }
                }
                else
                {
                    MessageBox.Show("Incorrect Confirm Password");
                }
            }
            else
            {
                MessageBox.Show("Please choose City And District");
            }
        }

        private void btnStaffRegister_Click(object sender, RoutedEventArgs e)
        {
            string username = tbStaffUsername.Text;
            string password = pwbStaffPassword.Password;
            string confirmpass = pwbStaffConfirmPassword.Password;
            if (password.Equals(confirmpass))
            {
                // Hash password
                string strPwHash = hashPassword(password);

                try
                {
                    using (var db = new BookingAndTransferFoodsEntities())
                    {

                        var findUser = db.Users.SingleOrDefault(u => u.UserName == username);

                        if (findUser != null)
                        {
                            MessageBox.Show("Username already exist");
                        }
                        else
                        {
                            User user = new User();
                            user.UserName = username;
                            user.Password = strPwHash;
                           
                            user.RoleID = 2;
                            db.Users.Add(user);

                            db.SaveChanges();
                            MessageBox.Show("Register success");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.InnerException.ToString());
                }
            }
            else
            {
                MessageBox.Show("Incorrect Confirm Password");
            }
        }

        private void btnAdminRegister_Click(object sender, RoutedEventArgs e)
        {
            string username = tbAdminUsername.Text;
            string password = pwbAdminPassword.Password;
            string confirmpass = pwbAdminConfirmPassword.Password;
            if (password.Equals(confirmpass))
            {
                // Hash password
                string strPwHash = hashPassword(password);

                try
                {
                    using (var db = new BookingAndTransferFoodsEntities())
                    {

                        var findUser = db.Users.SingleOrDefault(u => u.UserName == username);

                        if (findUser != null)
                        {
                            MessageBox.Show("Username already exist");
                        }
                        else
                        {
                            User user = new User();
                            user.UserName = username;
                            user.Password = strPwHash;

                            user.RoleID = 1;
                            db.Users.Add(user);

                            db.SaveChanges();
                            MessageBox.Show("Register success");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.InnerException.ToString());
                }
            }
            else
            {
                MessageBox.Show("Incorrect Confirm Password");
            }
        }

        private string hashPassword(string password)
        {
            var sha1 = new SHA1CryptoServiceProvider();
            var bytesPw = Encoding.ASCII.GetBytes(password);

            //salt
            var salt = DateTime.Now.Millisecond.ToString();
            var bytesSalt = Encoding.ASCII.GetBytes(salt);

            //
            //hash
            var bytesPwhashed = sha1.ComputeHash(bytesPw);

            //gắn salt lưu xuống database
            var bytesResult = new byte[bytesPwhashed.Length + bytesSalt.Length];
            Array.Copy(bytesPwhashed, bytesResult, bytesPwhashed.Length);
            Array.Copy(bytesSalt, 0, bytesResult, bytesPwhashed.Length, bytesSalt.Length);

            var strPwHash = ArrByteToString(bytesResult);

            return strPwHash;
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
    }
}
