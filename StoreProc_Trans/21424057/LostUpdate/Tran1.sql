﻿SELECT * FROM Orders WHERE DriverID = 7
DECLARE @RS AS INT
EXEC @RS = SP_Transfer1 3, 7
IF @RS = 1	PRINT N'NHẬN VẬN CHUYỂN THẤT BẠI'ELSE	PRINT N'NHẬN VẬN CHUYỂN THÀNH CÔNG'
SELECT * FROM Orders WHERE DriverID = 7