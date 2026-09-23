--CREATE DATABASE DelicatessenProject
--GO
USE DelicatessenProject
CREATE TABLE Categories (
    CategoryId  INT IDENTITY(1,1) PRIMARY KEY ,
    CategoryName NVARCHAR(200) NOT NULL UNIQUE
);


CREATE TABLE Products (
    ProductId INT IDENTITY(1,1) PRIMARY KEY,
    CategoryId INT NOT NULL,
    ProductName NVARCHAR(200) NOT NULL,
    ProductDescription NVARCHAR(MAX),
    ProductPrice DECIMAL(10,2) NOT NULL,
    PriceDescription NVARCHAR(300),
    HeatingInstruction NVARCHAR(300),
    Allergens NVARCHAR(MAX),
    Ingredients NVARCHAR(MAX),
    NutritionalValues NVARCHAR(MAX),
    FilterProduct NVARCHAR(100),  -- "אין" במקרה שלך
	ImageUrl NVARCHAR(300),  -- שמירת הנתיב/URL של התמונה
	--מחיקה של קטגוריה תמחק גם את כל המוצרים שלה
    FOREIGN KEY (CategoryId) REFERENCES Categories(CategoryId) ON DELETE CASCADE
);


--CREATE TABLE ProductImages (
--    ImageId INT IDENTITY PRIMARY KEY,
--    ProductId INT NOT NULL,
--    ImageUrl NVARCHAR(300) NOT NULL,
--    IsMain BIT DEFAULT 1,
--    FOREIGN KEY (ProductId) REFERENCES Products(ProductId)
--);


CREATE TABLE HighQuantity (
    HQId INT IDENTITY(1,1) PRIMARY KEY,
    ProductId INT NOT NULL,
    HighSalt BIT NOT NULL DEFAULT 0,
    HighSugar BIT NOT NULL DEFAULT 0,
    HighFat BIT NOT NULL DEFAULT 0,
    FOREIGN KEY (ProductId) REFERENCES Products(ProductId)
);


CREATE TABLE Customers (
    CustomerId INT IDENTITY(1,1) PRIMARY KEY,  -- מקביל ל-code
    PrivateName NVARCHAR(200) NOT NULL,
    LastName NVARCHAR(200) NOT NULL,
    Email NVARCHAR(200) NOT NULL UNIQUE,
    Phone NVARCHAR(20) NOT NULL,
    City NVARCHAR(200) NOT NULL,
    AddressStreet NVARCHAR(300) NOT NULL,
    HouseNumber INT NOT NULL,
    BuildEntry NVARCHAR(50) DEFAULT '',
	Apartment NVARCHAR(50) DEFAULT '',
	BuildFloor NVARCHAR(50) DEFAULT '',

    PasswordHash NVARCHAR(500) NOT NULL,   -- במקום לשמור סיסמא רגילה!
    CreatedAt DATETIME DEFAULT GETDATE()
);


CREATE TABLE Orders (
    OrderId INT IDENTITY(1,1) PRIMARY KEY,
    CustomerId INT NOT NULL,
    TotalPrice DECIMAL(10,2),
    Status NVARCHAR(100),
    CreatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (CustomerId) REFERENCES Customers(CustomerId)
);


CREATE TABLE OrderItems (
    OrderItemId INT IDENTITY(1,1) PRIMARY KEY,
    OrderId INT NOT NULL,
    ProductId INT NOT NULL,
    Quantity INT NOT NULL,
    OrderItemPrice DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (OrderId) REFERENCES Orders(OrderId),
    FOREIGN KEY (ProductId) REFERENCES Products(ProductId)
);
