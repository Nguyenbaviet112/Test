-- Khách hàng hủy đơn hàng 
CREATE 
PROC USP_21424031_CancelOrder_DL
	@OrderID AS INT
AS
BEGIN TRAN
	set tran isolation level Repeatable read
	
	BEGIN TRY
	--  Kiểm tra tồn tại thông tin đơn hàng và trạng thái đơn hàng là chờ vận chuyển
		IF EXISTS (SELECT * FROM Orders WHERE ID = @OrderID AND Status = 1 AND DriverID is null)
		BEGIN
			WAITFOR DELAY '0:0:05'
			-- Cập nhật trạng thái hủy cho đơn hàng
			UPDATE Orders SET Status = 0 WHERE ID = @OrderID	
		END
	END TRY
	BEGIN CATCH
		DECLARE @ErrorMsg VARCHAR(2000)
		SELECT @ErrorMsg = N'Lỗi: ' + ERROR_MESSAGE()
		RAISERROR(@ErrorMsg, 16,1)
		ROLLBACK TRAN
		RETURN 0
	END CATCH

COMMIT TRAN
RETURN 1
GO


-- Tài xế xác nhận chọn đơn hàng
CREATE 
PROC USP_21424031_OrderConfirm_DL
	@OrderID AS INT,
	@DriverID AS INT
AS
BEGIN TRAN
	set tran isolation level Repeatable read
	
	BEGIN TRY
	--   Kiểm tra thông tin xem đơn hàng có tồn tại, chưa bị hủy và chưa được Driver nào khác vận chuyển  
		IF EXISTS (SELECT * FROM Orders WHERE ID = @OrderID AND Status = 1 AND DriverID is null)
		BEGIN
		WAITFOR DELAY '0:0:05'
			-- Cập nhật trạng thái xác nhận giao đơn hàng với tài xế này
			UPDATE Orders SET Status = 2, DriverID = @DriverID WHERE ID = @OrderID	
		END
	END TRY
	BEGIN CATCH
		DECLARE @ErrorMsg VARCHAR(2000)
		SELECT @ErrorMsg = N'Lỗi: ' + ERROR_MESSAGE()
		RAISERROR(@ErrorMsg, 16,1)
		ROLLBACK TRAN
		RETURN 0
	END CATCH

COMMIT TRAN
RETURN 1
GO

