﻿CREATE 
--ALTER
PROC USP_21424069_READ_DATA_Orders
	@ORDERS_ID BIGINT

AS
SET TRAN ISOLATION LEVEL REPEATABLE READ
BEGIN TRAN
	BEGIN TRY
	IF NOT EXISTS (SELECT * 
				FROM Orders O
				WHERE O.ID = @ORDERS_ID )
	BEGIN
		PRINT N' Không Tồn Tại Đơn Hàng'
		ROLLBACK TRAN
		RETURN 1
	END

	

	SELECT Amount FROM Orders WHERE ID = @ORDERS_ID 

	WAITFOR DELAY '0:0:10'
	
	SELECT * FROM OrderDetails  WHERE OrderID = @ORDERS_ID 

	
	END TRY
	BEGIN CATCH
		PRINT N'LỖI HỆ THỐNG'
		ROLLBACK TRAN
		RETURN 1	
	END CATCH
COMMIT TRAN
RETURN 0
GO

CREATE 
--ALTER
PROC USP_21424069_WRITE_DATA_OrdersDetail
	@ID BIGINT,
	@ORDERS_ID BIGINT,
	@PRODUCT_ID BIGINT,
	@QUANTITY INT,
	@PRICE DECIMAL(18,2),
	@AMOUNT DECIMAL(18,2)
AS
BEGIN TRAN
	BEGIN TRY

		INSERT INTO OrderDetails
		VALUES(@ORDERS_ID, @PRODUCT_ID, @QUANTITY, @PRICE, @AMOUNT)


	END TRY
	BEGIN CATCH
		PRINT N'LỖI HỆ THỐNG'
		ROLLBACK TRAN
		RETURN 1
	END CATCH
COMMIT TRAN
RETURN 0
GO

