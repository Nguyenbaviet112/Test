SELECT * FROM Orders
--Khách hàng hủy đơn hàng
EXEC USP_21424031_CancelOrder_DL 7
-- EXEC USP_21424031_CancelOrder_DL_Fix 7
SELECT * FROM Orders