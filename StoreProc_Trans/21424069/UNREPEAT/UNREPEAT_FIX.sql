﻿CREATE 
--ALTER
PROC USP_21424069_READ_DATA_PRODUCT_Fix
	@PRODUCT_ID BIGINT
AS
BEGIN TRAN
	BEGIN TRY
		IF NOT EXISTS(SELECT * 
					FROM Products P 
					WHERE P.ID = @PRODUCT_ID)
		BEGIN
			PRINT N'SẢN PHẨM KHÔNG TỒN TẠI'
			ROLLBACK TRAN
			RETURN 
		END
		
		ELSE
			SELECT P.Name FROM Products P WITH(ROWLOCK, XLOCK) WHERE P.ID = @PRODUCT_ID
		--ĐỂ TEST
		WAITFOR DELAY '0:0:05'
		---------
		SELECT * FROM Products P WHERE P.ID = @PRODUCT_ID

	END TRY
	BEGIN CATCH
		PRINT N'LỖI HỆ THỐNG'
		ROLLBACK TRAN
	END CATCH
COMMIT TRAN
GO

CREATE 
--ALTER
PROC USP_21424069_DELETE_DATA_PRODUCT_Fix
	@PRODUCT_ID BIGINT
AS
BEGIN TRAN
	BEGIN TRY
		IF NOT EXISTS (SELECT *
						FROM Products P
						WHERE P.ID = @PRODUCT_ID)
		BEGIN
			PRINT  N'SẢN PHẨM KHÔNG TỒN TẠI'
			ROLLBACK TRAN
			RETURN 1
		END

		DELETE 
		FROM Products
		WHERE ID = @PRODUCT_ID

		-- XEM SQL PHÁT KHÓA 
		SELECT 
		OBJECT_NAME(p.OBJECT_ID) AS TableName,
		resource_type, request_status, request_mode,request_session_id, 
		DB_NAME(RESOURCE_DATABASE_ID)NAME
		FROM 
		sys.dm_tran_locks l 
		JOIN sys.partitions p ON l.resource_associated_entity_id = p.hobt_id

	END TRY
	BEGIN CATCH
		PRINT N'LỖI HỆ THỐNG'
		ROLLBACK TRAN
		RETURN 1
	END CATCH
	
COMMIT TRAN
RETURN 0
GO
