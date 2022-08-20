DECLARE @ProductID AS INT, @RS INT, @OrderID AS INT
SET @ProductID = 1
SELECT * FROM Products where ID = @ProductID
EXEC @RS = SP_Order @ProductID, 2, 13, 20000, @OrderID OUTPUT
IF @RS = 1	PRINT N'ĐẶT HÀNG THÀNH CÔNG'ELSE	PRINT N'ĐẶT HÀNG THẤT BẠI'
SELECT * FROM OrderDetails WHERE OrderID = @OrderID