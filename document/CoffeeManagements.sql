CREATE DATABASE CoffeeManagements;
USE CoffeeManagements;

CREATE TABLE [Table]
(
	Id INT IDENTITY PRIMARY KEY,
	Name NVARCHAR(100) NOT NULL DEFAULT N'N/A',
	Status tinyint NOT NULL DEFAULT 0, --0: available, 1: unavailable
)
GO

CREATE TABLE Account
(
	Id INT IDENTITY PRIMARY KEY,
	UserName NVARCHAR(100) UNIQUE NOT NULL,
	DisplayName NVARCHAR(100) NOT NULL DEFAULT N'N/A',
	PassWord NVARCHAR(200) NOT NULL DEFAULT 0,
	Type TINYINT NOT NULL DEFAULT 0,  --1:admin, 0:staff
)
GO

CREATE TABLE Category
(
	Id INT IDENTITY PRIMARY KEY,
	Name NVARCHAR(100) NOT NULL DEFAULT N'N/A',

)
GO

CREATE TABLE Product
(
	Id INT IDENTITY PRIMARY KEY,
	Name NVARCHAR(100) NOT NULL DEFAULT N'N/A' ,
	IdCategory INT  NOT NULL,
	Price FLOAT  NOT NULL,
	FOREIGN KEY (IdCategory) REFERENCES Category(Id),
)
GO

CREATE TABLE Bill
(
	Id INT IDENTITY PRIMARY KEY,
	DateCheckIn Date NOT NULL DEFAULT GetDate(),
	DateCheckOut Date,
	IdTable INT NOT NULL,
	Status INT NOT NULL DEFAULT 0, --1:da tt, 0:chua tt
	Created_by INT,
	FOREIGN KEY (IdTable) REFERENCES [Table](Id),
	FOREIGN KEY (Created_by) REFERENCES Account(Id)
	 
)

Drop table bill
GO

CREATE TABLE BillInfo
(
	Id INT IDENTITY PRIMARY KEY,
	IdBill INT NOT NULL,
	IdProduct INT NOT NULL,
	Count INT NOT NULL DEFAULT 0,
	FOREIGN KEY (IdBill) REFERENCES Bill(Id),
	FOREIGN KEY (IdProduct) REFERENCES Product(Id),

)

drop table BillInfo
GO

INSERT INTO Account(UserName,DisplayName,PassWord,Type)
VALUES (N'duc',N'NgocDuc',N'123456',1)
GO
INSERT INTO Account(UserName,DisplayName,PassWord,Type)
VALUES (N'hi',N'hihi',N'123456',0)
GO
INSERT INTO Account(UserName,DisplayName,PassWord,Type)
VALUES (N'hung',N'hungnguyen',N'123456',0)
GO
INSERT INTO Account(UserName,DisplayName,PassWord,Type)
VALUES (N'Admin',N'Admin',N'Admin',1)
GO
INSERT INTO Account(UserName,DisplayName,PassWord,Type)
VALUES (N'User',N'User',N'User',0)

GO

CREATE PROC USP_Login
@UserName nvarchar(100), @PassWord nvarchar(100)
AS
BEGIN
SELECT *FROM Account WHERE UserName=@UserName AND PassWord=@PassWord
END

GO

INSERT [Table] (name)
VALUES(N'Ban 1')
GO
INSERT [Table] (name)
VALUEs(N'Bàn 2')
GO
INSERT [Table] (name)
VALUEs(N'Bàn 3')
GO
INSERT [Table] (name)
VALUEs(N'Bàn 4')
GO
INSERT [Table] (name)
VALUEs(N'Bàn 5')
GO
INSERT [Table] (name)
VALUEs(N'Bàn 6')
GO
INSERT [Table] (name)
VALUEs(N'Bàn 7')
GO
INSERT [Table] (name)
VALUEs(N'Bàn 8')
GO
INSERT [Table] (name)
VALUEs(N'Bàn 9')
GO
INSERT [Table] (name)
VALUEs(N'Bàn 10')
GO
INSERT [Table] (name)
VALUEs(N'Bàn 11')
GO
INSERT [Table] (name)
VALUEs(N'Bàn 12')
GO
INSERT [Table] (name)
VALUEs(N'Bàn 13')
GO
INSERT [Table] (name)
VALUEs(N'Bàn 14')
GO
INSERT [Table] (name)
VALUEs(N'Bàn 15')
GO
INSERT [Table] (name)
VALUEs(N'Bàn 16')
GO
INSERT [Table] (name)
VALUEs(N'Bàn 17')
GO
INSERT [Table] (name)
VALUEs(N'Bàn 18')
GO
INSERT [Table] (name)
VALUEs(N'Bàn 19')
GO
INSERT [Table] (name)
VALUEs(N'Bàn 20')
GO

CREATE PROC USP_GetTableList
AS SELECT *FROM [Table]
GO
--EXEC USP_GetTableList

GO
---
INSERT Category(Name)
VALUES (N'Cà Phê')
GO
INSERT Category(Name)
VALUES (N'Đồ Ăn Nhanh')
GO
INSERT Category(Name)
VALUES (N'Nước Ép Trái Cây')
GO
INSERT Category(Name)
VALUES (N'Trà Sữa')
GO
INSERT Category(Name)
VALUES (N'Bánh Ngọt')
GO
INSERT Category(Name)
VALUES (N'Đồ Ăn Vặt')
GO
--select *from Category
---

INSERT Product(name,IdCategory,Price)
VALUES (N'Cafe đen nóng/đá',1,15000)
GO

INSERT Product(name,IdCategory,Price)
VALUES (N'Cafe sữa nóng/đá',1,15000)
GO

INSERT Product(name,IdCategory,Price)
VALUES (N'Bạc xỉu',1,20000)
GO

INSERT Product(name,IdCategory,Price)
VALUES (N'Cafe pha máy',1,20000)
GO

INSERT Product(name,IdCategory,Price)
VALUES (N'Cafe xay Chocolate',1,25000)
GO

INSERT Product(name,IdCategory,Price)
VALUES (N'Americano',1,35000)
GO

INSERT Product(name,IdCategory,Price)
VALUES (N'Cappuccino',1,35000)
GO

---

INSERT Product(name,IdCategory,Price)
VALUES (N'Xúc xích',2,10000)
GO

INSERT Product(name,IdCategory,Price)
VALUES (N'Nem chua rán',2,10000)
GO
----
INSERT Product(name,IdCategory,Price)
VALUES (N'Nước ép dưa hấu',3,25000)
GO

INSERT Product(name,IdCategory,Price)
VALUES (N'Nước ép dứa',3,25000)
GO
INSERT Product(name,IdCategory,Price)
VALUES (N'Nước ép xoài',3,25000)
GO
INSERT Product(name,IdCategory,Price)
VALUES (N'Nước ép thập cẩm',3,30000)
GO

------
INSERT Product(name,IdCategory,Price)
VALUES (N'Trà sữa trân châu đường đen',4,30000)
GO
INSERT Product(name,IdCategory,Price)
VALUES (N'Trà sữa hoa quả',4,30000)
GO
INSERT Product(name,IdCategory,Price)
VALUES (N'Trà sữa bạc hà',4,30000)
GO
INSERT Product(name,IdCategory,Price)
VALUES (N'Trà sữa dâu',4,30000)
GO
INSERT Product(name,IdCategory,Price)
VALUES (N'Trà sữa Matcha',4,30000)
GO

----
INSERT Product(name,IdCategory,Price)
VALUES (N'Bánh chuối',5,15000)
GO
INSERT Product(name,IdCategory,Price)
VALUES (N'Bánh phô mai cafe',5,15000)
GO
INSERT Product(name,IdCategory,Price)
VALUES (N'Bánh ngọt',5,10000)
GO
--------
INSERT Product(name,IdCategory,Price)
VALUES (N'Hướng dương ',6,5000)
GO
INSERT Product(name,IdCategory,Price)
VALUES (N'Khô gà',6,10000)
GO
INSERT Product(name,IdCategory,Price)
VALUES (N'Khô bò',6,10000)
GO

----  insert bill
INSERT INTO BILL (DateCheckIn,DateCheckOut,IdTable,Status)
VALUES (GETDATE(),Null,1,0)

INSERT INTO BILL (DateCheckIn,DateCheckOut,IdTable,Status)
VALUES (GETDATE(),Null,2,0)

INSERT INTO BILL (DateCheckIn,DateCheckOut,IdTable,Status)
VALUES (GETDATE(),GETDATE(),2,1)
GO
INSERT INTO BILL (DateCheckIn,DateCheckOut,IdTable,Status)
VALUES (GETDATE(),GETDATE(),3,1)
GO
---insert bill info
INSERT BillInfo(IdBill,IdProduct,[Count])
VALUES (1,1,2)
GO
INSERT BillInfo(IdBill,IdProduct,[Count])
VALUES (1,3,4)
GO
INSERT BillInfo(IdBill,IdProduct,[Count])
VALUES (1,6,3)
GO

INSERT BillInfo(IdBill,IdProduct,[Count])
VALUES (1,8,6)
GO

INSERT BillInfo(IdBill,IdProduct,[Count])
VALUES (2,9,2)
GO
INSERT BillInfo(IdBill,IdProduct,[Count])
VALUES (2,17,1)
GO
INSERT BillInfo(IdBill,IdProduct,[Count])
VALUES (3,10,3)
GO
INSERT BillInfo(IdBill,IdProduct,[Count])
VALUES (3,20,3)
--GO



--GO

--CREATE PROC USP_InsertBill
--@idTable INT
--AS 
--BEGIN 
--	INSERT Bill(DateCheckIn,DateCheckOut,IdTable,Status,Discount)
--	VALUES (GETDATE(),NULL,@idTable,0,0)
--END 

--GO 

--CREATE PROC USP_InsertBillInfo
--@idBill INT, @idProduct INT, @count INT
--AS 
--BEGIN 
--	INSERT BillInfo(IdBill,IdProduct,Count)
--	VALUES (@idBill,@idProduct,@count)
--END 

--GO 

----SELECT MAX(Id) FROM Bill;

--GO


--AlTER PROC USP_InsertBillInfo
--@IdBill INT, @IdProduct INT, @Count INT
--AS
--BEGIN

--	DECLARE @isExitsBillInfo INT
--	DECLARE @productCount INT = 1
	
--	SELECT @isExitsBillInfo = id, @productCount = b.count 
--	FROM dbo.BillInfo AS b 
--	WHERE idBill = @idBill AND IdProduct = @IdProduct

--	IF (@isExitsBillInfo > 0)
--	BEGIN
--		DECLARE @newCount INT = @productCount + @count
--		IF (@newCount > 0)
--			UPDATE dbo.BillInfo	SET count = @productCount + @count WHERE IdProduct = @IdProduct
--		ELSE
--			DELETE dbo.BillInfo WHERE idBill = @idBill AND IdProduct = @IdProduct
--	END
--	ELSE
--	BEGIN
--		INSERT	dbo.BillInfo
--        ( idBill, IdProduct, count )
--		VALUES  ( @idBill, -- idBill - int
--          @IdProduct, -- idFood - int
--          @count  -- count - int
--          )
--	END
--END

--GO

--CREATE TRIGGER UTG_UpdateBillInfo
--ON BillInfo FOR INSERT,UPDATE
--AS
--BEGIN
--	DEClARE @idBill INT
--	SELECT @idBill=IdBill from inserted
--	DEClARE @idTable INT
--	SELECT @idTable =IdTable from Bill WHERE id =@idBill AND status =0
--	UPDATE [Table] SET status=N'Có người' WHERE id=@idTable
--END
--GO


--CREATE TRIGGER UTG_UpdateBill
--ON Bill FOR UPDATE
--AS
--BEGIN
--	DEClARE @idBill INT
--	SELECT @idBill =id FROM inserted
	
--	DECLARE @idTable INT
--	SELECT @idTable =IdTable from Bill WHERE id =@idBill

--	DECLARE @count INT =0
--	SELECT @count = COUNT(*) FROM Bill WHERE IdTable=@idTable AND status =0
--	IF(@count=0)
--		UPDATE [Table] SET Status=N'Trống' WHERE Id=@idTable

--	END

--GO

--GO
--CREATE PROC USP_GetListBillByDateForReport
--@checkIn date, @checkOut date
--AS 
--BEGIN
--	SELECT t.name AS [Name Table], b.TotalBill AS [Total Bill], DateCheckIn AS[Date Checkin], DateCheckOut AS[Date Checkout], discount AS [Discount]
--	FROM Bill AS b,[Table] AS t
--	WHERE DateCheckIn >= @checkIn AND DateCheckOut <= @checkOut AND b.status = 1
--	AND t.id = b.idTable
--END
--GO

--CREATE PROC USP_UpdateAccount
--@userName NVARCHAR(100), @displayName NVARCHAR(100), @passWord NVARCHAR(100), @newPassWoord NVARCHAR(100)
--As 
--BEGIN
--	DECLARE @isRightPass int =0
--	SELECT @isRightPass = COUNT(*) FROM Account WHERE UserName =@userName AND PassWord = @passWord
--	IF(@isRightPass=1)
--	BEGIN
--		IF(@newPassWoord = null OR @newPassWoord ='')
--			BEGIN
--				UPDATE Account SET DisplayName=@displayName WHERE UserName =@userName
--			END
--			ELSE
--				UPDATE Account SET DisplayName=@displayName, PassWord = @newPassWoord WHERE UserName= @userName
--	END
--END
--GO






