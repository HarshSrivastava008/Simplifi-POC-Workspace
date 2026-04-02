-- ============================================
-- Sample Stored Procedures for WrapSP POC
-- Run this against your target SQL Server database
-- ============================================

-- Create sample table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Orders')
BEGIN
    CREATE TABLE Orders (
        OrderId     INT IDENTITY(1,1) PRIMARY KEY,
        CustomerName NVARCHAR(200) NOT NULL,
        ProductName  NVARCHAR(200) NOT NULL,
        Quantity     INT NOT NULL,
        Price        DECIMAL(18,2) NOT NULL,
        CreatedAt    DATETIME2 NOT NULL DEFAULT GETUTCDATE()
    );
END
GO

-- =====================
-- QUERY: Get All Orders
-- =====================
CREATE OR ALTER PROCEDURE sp_GetAllOrders
AS
BEGIN
    SET NOCOUNT ON;
    SELECT OrderId, CustomerName, ProductName, Quantity, Price, CreatedAt
    FROM Orders
    ORDER BY CreatedAt DESC;
END
GO

-- ========================
-- QUERY: Get Order By Id
-- ========================
CREATE OR ALTER PROCEDURE sp_GetOrderById
    @OrderId INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT OrderId, CustomerName, ProductName, Quantity, Price, CreatedAt
    FROM Orders
    WHERE OrderId = @OrderId;
END
GO

-- =======================
-- COMMAND: Create Order
-- =======================
CREATE OR ALTER PROCEDURE sp_CreateOrder
    @CustomerName NVARCHAR(200),
    @ProductName  NVARCHAR(200),
    @Quantity     INT,
    @Price        DECIMAL(18,2)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Orders (CustomerName, ProductName, Quantity, Price)
    VALUES (@CustomerName, @ProductName, @Quantity, @Price);

    SELECT SCOPE_IDENTITY();
END
GO

-- =======================
-- COMMAND: Update Order
-- =======================
CREATE OR ALTER PROCEDURE sp_UpdateOrder
    @OrderId      INT,
    @CustomerName NVARCHAR(200),
    @ProductName  NVARCHAR(200),
    @Quantity     INT,
    @Price        DECIMAL(18,2)
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Orders
    SET CustomerName = @CustomerName,
        ProductName  = @ProductName,
        Quantity     = @Quantity,
        Price        = @Price
    WHERE OrderId = @OrderId;
END
GO

-- =======================
-- COMMAND: Delete Order
-- =======================
CREATE OR ALTER PROCEDURE sp_DeleteOrder
    @OrderId INT
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM Orders WHERE OrderId = @OrderId;
END
GO
