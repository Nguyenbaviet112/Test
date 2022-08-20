CREATE 
--ALTER
PROC USP_21424028_Insert_Order
	@BranchID BIGINT,
	@Payment INT,
	@Amount DECIMAL(18,2),
	@TotalAmount DECIMAL(18,2),
	@CustomerCityID INT,
	@CustomerDistrictID INT,
	@ProductFee DECIMAL(18,2),
	@TransferFee DECIMAL(18,2),
	@CustomerAddress NVARCHAR(MAX),
	@Status INT,
	@DriverID BIGINT,
	@CustomerID BIGINT,
	@ProductID BIGINT,
	@QuantityOrder INT,
	@Price DECIMAL(18,2),
	@AmountProduct DECIMAL(18,2)
AS
BEGIN TRAN
	BEGIN TRY
	IF NOT EXISTS (SELECT * 
				FROM ProductBranch
				WHERE BranchID = @BranchID
				AND ProductID = @ProductID)
	BEGIN
		PRINT N'CHI NHÁNH SẢN PHẨM KHÔNG TỒN TẠI'
		ROLLBACK TRAN
		RETURN 1
	END
	IF @Payment IS NULL 
	BEGIN
		PRINT N'PHƯƠNG THỨC THANH TOÁN KHÔNG ĐƯỢC TRỐNG'
		ROLLBACK TRAN
		RETURN 1
	END
	IF @CustomerCityID IS NULL 
	BEGIN
		PRINT N'TỈNH/THÀNH PHỐ KHÔNG ĐƯỢC ĐỂ TRỐNG'
		ROLLBACK TRAN
		RETURN 1
	END
	IF @CustomerDistrictID IS NULL 
	BEGIN
		PRINT N'QUẬN/HUYỆN/XÃ KHÔNG ĐƯỢC ĐỂ TRỐNG'
		ROLLBACK TRAN
		RETURN 1
	END
	IF @ProductFee IS NULL 
	BEGIN
		PRINT N'PHÍ SẢN PHẨM KHÔNG ĐƯỢC ĐỂ TRỐNG'
		ROLLBACK TRAN
		RETURN 1
	END
	IF @TransferFee IS NULL 
	BEGIN
		PRINT N'PHÍ VẬN CHUYỂN KHÔNG ĐƯỢC ĐỂ TRỐNG'
		ROLLBACK TRAN
		RETURN 1
	END
	IF @CustomerAddress IS NULL 
	BEGIN
		PRINT N'ĐỊA CHỈ KHÔNG ĐƯỢC ĐỂ TRỐNG'
		ROLLBACK TRAN
		RETURN 1
	END
	IF @CustomerID IS NULL 
	BEGIN
		PRINT N'MÃ KHÁCH HÀNG KHÔNG ĐƯỢC ĐỂ TRỐNG'
		ROLLBACK TRAN
		RETURN 1
	END

	DECLARE @QuantityProduct INT
	SET @QuantityProduct = (SELECT Quantity 
				FROM ProductBranch
				WHERE ProductID = @ProductID
				AND BranchID = @BranchID)
	IF @QuantityProduct - @QuantityOrder < 0
	BEGIN
		PRINT N'SỐ LƯỢNG SẢN PHẨM KHÔNG ĐỦ'
		ROLLBACK TRAN
		RETURN 1
	END
	
	WAITFOR DELAY '0:0:05'
	
	UPDATE ProductBranch
	SET Quantity = @QuantityProduct - @QuantityOrder
	WHERE ProductID = @ProductID
	AND BranchID = @BranchID

	INSERT Orders
	VALUES(@BranchID, @Payment, @Amount, @TotalAmount, @CustomerCityID, @CustomerDistrictID, @ProductFee,
			@TransferFee, @CustomerAddress, @Status, @DriverID, @CustomerID)

	INSERT INTO OrderDetails
	VALUES(SCOPE_IDENTITY(),@ProductID, @QuantityOrder,@Price, @AmountProduct)
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
PROC USP_21424028_Update_Quantity_Product
	@BranchID BIGINT,
	@ProductID BIGINT,
	@Quantity INT,
	@isActive BIT
AS
BEGIN TRAN
	BEGIN TRY
		IF NOT EXISTS (SELECT * 
				FROM ProductBranch
				WHERE BranchID = @BranchID
				AND ProductID = @ProductID)
		BEGIN
			PRINT N'CHI NHÁNH SẢN PHẨM KHÔNG TỒN TẠI'
			ROLLBACK TRAN
			RETURN 1
		END
		IF @Quantity < 0
		BEGIN
			PRINT N'SỐ LƯỢNG SẢN PHẨM KHÔNG HỢP LỆ'
			ROLLBACK TRAN
			RETURN 1
		END
		IF @isActive IS NULL
		BEGIN
			PRINT N'TÌNH TRẠNG SẢN PHẨM KHÔNG ĐƯỢC ĐỂ TRỐNG'
			ROLLBACK TRAN
			RETURN 1
		END

		UPDATE ProductBranch
		SET Quantity = @Quantity
		WHERE ProductID = @ProductID
		AND BranchID = @BranchID

	END TRY
	BEGIN CATCH
		PRINT N'LỖI HỆ THỐNG'
		ROLLBACK TRAN
		RETURN 1	
	END CATCH
COMMIT TRAN
RETURN 0
GO
