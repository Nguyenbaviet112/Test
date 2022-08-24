using BookingAndDelivery.Model._21424031;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingAndDelivery.Model._21424031
{
    public class OrderDAO031
    {
        private BookingAndTransferFoodsEntities db;

        public void GetInfoDriver()
        {

        }

        public bool CancelOrder_21424031(long orderID)
        {
            bool isSuccess = db.Database.ExecuteSqlCommand("EXEC USP_21424031_CancelOrder_LU @OrderID", new SqlParameter("@OrderID", orderID)) == 1 ? true : false;
            return isSuccess;
        }
        public bool CancelOrderFix_21424031(long orderID)
        {
            bool isSuccess = db.Database.ExecuteSqlCommand("EXEC USP_21424031_CancelOrder_LU_Fix @OrderID", new SqlParameter("@OrderID", orderID)) == 1 ? true : false;
            return isSuccess;
        }

        public bool ConfirmTranferOrder_21424031(long orderID, long driverID)
        {
            bool isSuccess = db.Database.ExecuteSqlCommand("EXEC USP_21424031_OrderConfirm_LU @OrderID, @DriverID", new SqlParameter("@OrderID", orderID), new SqlParameter("@DriverID", driverID)) == 1 ? true : false;
            return isSuccess;

        }

        public bool ConfirmTranferOrderFix_21424031(long orderID, long driverID)
        {
            bool isSuccess = db.Database.ExecuteSqlCommand("EXEC USP_21424031_OrderConfirm_LU_Fix @OrderID, @DriverID", new SqlParameter("@OrderID", orderID), new SqlParameter("@DriverID", driverID)) == 1 ? true : false;
            return isSuccess;

        }

        public bool CancelOrder_DL_21424031(long orderID)
        {
            bool isSuccess = db.Database.ExecuteSqlCommand("EXEC USP_21424031_CancelOrder_DL @OrderID", new SqlParameter("@OrderID", orderID)) == 1 ? true : false;
            return isSuccess;
        }
        public bool CancelOrderFix_DL_21424031(long orderID)
        {
            bool isSuccess = db.Database.ExecuteSqlCommand("EXEC USP_21424031_CancelOrder_DL_Fix @OrderID", new SqlParameter("@OrderID", orderID)) == 1 ? true : false;
            return isSuccess;
        }

        public bool ConfirmTranferOrder_DL_21424031(long orderID, long driverID)
        {
            bool isSuccess = db.Database.ExecuteSqlCommand("EXEC USP_21424031_OrderConfirm_DL @OrderID, @DriverID", new SqlParameter("@OrderID", orderID), new SqlParameter("@DriverID", driverID)) == 1 ? true : false;
            return isSuccess;

        }

        public bool ConfirmTranferOrderFix_DL_21424031(long orderID, long driverID)
        {
            bool isSuccess = db.Database.ExecuteSqlCommand("EXEC USP_21424031_OrderConfirm_DL_Fix @OrderID, @DriverID", new SqlParameter("@OrderID", orderID), new SqlParameter("@DriverID", driverID)) == 1 ? true : false;
            return isSuccess;

        }

        public bool checkCancelOrder(long orderID)
        {
            Order order = db.Orders.Where(od => od.ID == orderID).Single();
            return order.Status == 0;
        }
        public bool setOrderStatus(long orderID, int status)
        {
            Order order = db.Orders.Where(od => od.ID == orderID).Single();

            order.Status = status;

            return db.SaveChanges() > 0;
        }

        // Kiểm tra có đơn hàng đang vận chuyển
        public bool checkProductIsOrder(long driverID)
        {
            return db.Orders.Where(od => od.DriverID == driverID && (od.Status == 2 || od.Status == 3)).ToList().Count == 0;
        }

        public ObservableCollection<OrderInformation> getListOrderByLocation(int cityID, int districtID, long driverID)
        {
            //var sqlRes = db.Database.ExecuteSqlCommand("select od.ID,us.FullName,us.Address,us.Phone,od.CustomerAddress,od.Status from Orders as od join Users as us on od.CustomerID = us.ID  join Branch as br on od.BranchID = br.ID where br.CityID=@CityID and br.DistrictID=@DistrictID", new SqlParameter("@CityID", cityID), new SqlParameter("@DistrictID", districtID));
            var result = (from od in db.Orders
                          join us in db.Users on od.CustomerID equals us.ID
                          join br in db.Branches on od.BranchID equals br.ID
                          where br.CityID == cityID && br.DistrictID == districtID && (od.Status == 1 || od.Status == 2 || od.Status == 3) && (od.DriverID == null || od.DriverID == driverID)
                          select new OrderInformation
                          {
                              ID = od.ID,
                              nameCus = us.FullName,
                              addressCus = us.Address,
                              addressOrder = od.CustomerAddress,
                              phoneCus = us.Phone,
                              status = od.Status,
                              total = (int)od.TotalAmount
                          }).Distinct().ToList();

            return new ObservableCollection<OrderInformation>(result);
        }

        public ObservableCollection<PartnerInformation> GetListPartner()
        {
            //var sqlRes = db.Database.ExecuteSqlCommand("select od.ID,us.FullName,us.Address,us.Phone,od.CustomerAddress,od.Status from Orders as od join Users as us on od.CustomerID = us.ID  join Branch as br on od.BranchID = br.ID where br.CityID=@CityID and br.DistrictID=@DistrictID", new SqlParameter("@CityID", cityID), new SqlParameter("@DistrictID", districtID));
            var result = (from us in db.Users
                          join br in db.Branches on us.ID equals br.PartnerID
                          where us.RoleID == 3
                          group br by new { us.ID, us.FullName, us.Address, us.Phone } into uss
                          select new PartnerInformation
                          {
                              ID = uss.Key.ID,
                              name = uss.Key.FullName,
                              address = uss.Key.Address,
                              phone = uss.Key.Phone,
                              numBranch = uss.Count()
                          }).Distinct().ToList();

            return new ObservableCollection<PartnerInformation>(result);
        }

        public ObservableCollection<Branch> GetListBranchByPartner(long partnerID)
        {
            var result = db.Branches.Where(br => br.PartnerID == partnerID).Distinct().ToList();
            return new ObservableCollection<Branch>(result);
        }

        public ObservableCollection<ProductInformation> GetListProductByBranch(long branchID)
        {
            var result = (from brPd in db.ProductBranches
                          join pd in db.Products on brPd.ProductID equals pd.ID
                          where brPd.BranchID == branchID
                          select new ProductInformation
                          {
                              ID = pd.ID,
                              name = pd.Name,
                              price = pd.Price != null ? (long)pd.Price : 0,
                              quantityStock = brPd.Quantity != null ? (int)brPd.Quantity : 0,
                              quantityBuy = 0
                          }).Distinct().ToList();

            return new ObservableCollection<ProductInformation>(result);
        }

        public bool orderProduct(ObservableCollection<ProductInformation> products, long branchID, User customer, int total)
        {
            bool isSuccess = db.Database.ExecuteSqlCommand("EXEC USP_21424031_OrderProduct @BranchID, @TotalAmount, @CustomerID,@CustomerCityID,@CustomerDistrictID, @CustomerAddress", new SqlParameter("@BranchID", branchID), new SqlParameter("@TotalAmount", total), new SqlParameter("@CustomerID", customer.ID), new SqlParameter("@CustomerCityID", customer.CityID), new SqlParameter("@CustomerDistrictID", customer.District), new SqlParameter("@CustomerAddress", customer.Address)) == 1 ? true : false;

            Order order = getOrderInformation(branchID, customer.ID, 1);

            foreach (ProductInformation item in products)
            {
                isSuccess = db.Database.ExecuteSqlCommand("EXEC USP_21424031_OrderProductDetail @OrderID, @ProductID, @Quantity, @Price, @Amount", new SqlParameter("@OrderID", order.ID), new SqlParameter("@ProductID", item.ID), new SqlParameter("@Quantity", item.quantityBuy), new SqlParameter("@Price", item.price), new SqlParameter("@Amount", item.price * item.quantityBuy)) == 1 ? true : false;
                if (isSuccess == false)
                    break;
            }

            return isSuccess;
        }

        public Order getOrderInformation(long branchID, long customerID, int status)
        {
            return db.Orders.Where(od => od.BranchID == branchID && od.CustomerID == customerID && od.Status == status).FirstOrDefault();
        }

        public OrderDAO031()
        {
            db = new BookingAndTransferFoodsEntities();
        }
    }
}

public class OrderInformation
{
    public long ID { get; set; }
    public string nameCus { get; set; }
    public string phoneCus { get; set; }
    public string addressCus { get; set; }
    public string addressOrder { get; set; }
    public int status { get; set; }
    public int total { get; set; }
}

public class PartnerInformation
{
    public long ID { get; set; }
    public string name { get; set; }
    public string phone { get; set; }
    public string address { get; set; }
    public int numBranch { get; set; }
}

public class ProductInformation
{
    public long ID { get; set; }
    public string name { get; set; }
    public long price { get; set; }
    public int quantityStock { get; set; }
    public int quantityBuy { get; set; }
}