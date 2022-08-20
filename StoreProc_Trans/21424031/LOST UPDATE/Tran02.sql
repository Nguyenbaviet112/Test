SELECT * FROM Orders
--Tài xế xác nhận vận chuyển đơn hàng
EXEC USP_OrderConfirm_LU 7, 10
SELECT * FROM Orders