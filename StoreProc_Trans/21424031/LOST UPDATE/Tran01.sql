SELECT * FROM Orders
--Khách hàng hủy đơn hàng
EXEC USP_CancelOrder_LU 7
SELECT * FROM Orders