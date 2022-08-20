﻿CREATE 
--ALTER
PROC USP_21424028_Contract_Extension_Fix
	@ID BIGINT,
	@FromDate DATE,
	@ToDate DATE
AS
BEGIN TRAN
	BEGIN TRY
		IF NOT EXISTS (SELECT * 
				FROM Contract
				WHERE ID = @ID)
		BEGIN
			PRINT N'HỢP ĐỒNG KHÔNG TỒN TẠI'
			ROLLBACK TRAN
			RETURN 1
		END
		

		UPDATE Contract
		SET FromDate = @FromDate, ToDate = @ToDate
		WHERE ID = @ID

		WAITFOR DELAY '00:00:05'

		IF @ToDate < @FromDate
		BEGIN
			ROLLBACK TRAN
			RETURN 1
		END

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
PROC USP_21424028_Update_Contract_Fix
	@ID BIGINT
AS
BEGIN TRAN
SET TRAN ISOLATION LEVEL READ COMMITTED
	BEGIN TRY
		SELECT *
		FROM Contract
		WHERE ID = @ID

	END TRY
	BEGIN CATCH
		PRINT N'LỖI HỆ THỐNG'
		ROLLBACK TRAN
		RETURN 1	
	END CATCH
COMMIT TRAN
RETURN 0
GO