--CREATE 
ALTER
PROC USP_21424069_READ_DATA_USERS
AS
BEGIN TRAN
	BEGIN TRY

	DECLARE @COUNT_STAFF INT = (SELECT COUNT(*) FROM Users U WHERE U.RoleID = 2)

	WAITFOR DELAY '0:0:10'
	
	SELECT * FROM Users U WHERE U.RoleID = 2

	
	END TRY
	BEGIN CATCH
		PRINT N'LỖI HỆ THỐNG'
		ROLLBACK TRAN
		RETURN 1	
	END CATCH
COMMIT TRAN
RETURN 0
GO

--CREATE 
ALTER
PROC USP_21424069_INSERT_DATA_USERS
	@FullName nvarchar(max),
	@Phone varchar(12),
	@Address nvarchar(MAX),
	@Email nvarchar(50),
	@NameOfBank nvarchar(200),
	@BankAccount varchar(50),
	@NumOfOrder int,
	@RoleID int
AS
BEGIN TRAN
	BEGIN TRY

		INSERT INTO Users(FullName, Phone, Address, Email, NameOfBank, BankAccount, NumOfOrder, RoleID)
		VALUES(@FullName, @Phone, @Address, @Email, @NameOfBank, @BankAccount, @NumOfOrder, @RoleID)


	END TRY
	BEGIN CATCH
		PRINT N'LỖI HỆ THỐNG'
		ROLLBACK TRAN
		RETURN 1
	END CATCH
COMMIT TRAN
RETURN 0
GO
