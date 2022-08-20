DECLARE @ProductID AS INT, @RS INT
SET @ProductID = 1
SELECT * FROM Products where ID = @ProductID
EXEC @RS = SP_UPDATE_PRICE @ProductID, 45000
IF @RS = 1	PRINT N'CẬP NHẬT GIÁ THÀNH CÔNG'ELSE	PRINT N'CẬP NHẬT GIÁ THẤT BẠI'SELECT * FROM Products where ID = @ProductID