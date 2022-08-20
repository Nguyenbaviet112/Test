SELECT * FROM Orders
--Tài xế xác nhận vận chuyển đơn hàng
EXEC USP_21424031_OrderConfirm_DL 7, 10
-- EXEC USP_21424031_OrderConfirm_DL_Fix 7, 10
SELECT * FROM Orders