/* =========================================================
   Product table + Stored Procedures
   Mirrors the existing Role table/SP pattern in the ADO database.
   Run this against the same ADO database used by the Role module.
   ========================================================= */


-- 2. CREATE --------------------------------------------------
CREATE PROCEDURE CreateProduct
    @CategoryName NVARCHAR(100),
    @ProductName  NVARCHAR(100),
    @ProductPrice DECIMAL(18,2),
    @IsActive     BIT,
    @CreatedBy    BIGINT,
    @UpdatedBy    BIGINT
AS
BEGIN
    INSERT INTO Product (CategoryName, ProductName, ProductPrice, IsActive, CreatedBy, CreatedOn, UpdatedBy, UpdatedOn)
    VALUES (@CategoryName, @ProductName, @ProductPrice, @IsActive, @CreatedBy, GETDATE(), @UpdatedBy, GETDATE());
END
GO

-- 3. UPDATE --------------------------------------------------
CREATE PROCEDURE UpdateProduct
    @ProductId    BIGINT,
    @CategoryName NVARCHAR(100),
    @ProductName  NVARCHAR(100),
    @ProductPrice DECIMAL(18,2),
    @IsActive     BIT,
    @UpdatedBy    BIGINT
AS
BEGIN
    UPDATE Product
    SET CategoryName = @CategoryName,
        ProductName  = @ProductName,
        ProductPrice = @ProductPrice,
        IsActive     = @IsActive,
        UpdatedBy    = @UpdatedBy,
        UpdatedOn    = GETDATE()
    WHERE ProductId = @ProductId;
END
GO

-- 4. DELETE --------------------------------------------------
CREATE PROCEDURE DeleteProduct
    @ProductId BIGINT,
    @UpdatedBy BIGINT
AS
BEGIN
    DELETE FROM Product WHERE ProductId = @ProductId;
END
GO

-- 5. GET BY ID -------------------------------------------------
CREATE PROCEDURE GetProductById
    @ProductId BIGINT
AS
BEGIN
    SELECT ProductId, CategoryName, ProductName, ProductPrice, IsActive
    FROM Product
    WHERE ProductId = @ProductId;
END
GO

-- 6. GET ALL ---------------------------------------------------
CREATE PROCEDURE GetProducts
AS
BEGIN
    SELECT ProductId, CategoryName, ProductName, ProductPrice, IsActive
    FROM Product;
END
GO

-- 7. SEARCH (filter by CategoryName and/or ProductName) --------
CREATE PROCEDURE SearchProducts
    @CategoryName NVARCHAR(100) = NULL,
    @ProductName  NVARCHAR(100) = NULL
AS
BEGIN
    SELECT ProductId, CategoryName, ProductName, ProductPrice, IsActive
    FROM Product
    WHERE (@CategoryName IS NULL OR CategoryName = @CategoryName)
      AND (@ProductName IS NULL OR ProductName = @ProductName);
END
GO

-- 8. DISTINCT CATEGORY NAMES (for Search dropdown) --------------
CREATE PROCEDURE GetCategoryNames
AS
BEGIN
    SELECT DISTINCT CategoryName FROM Product ORDER BY CategoryName;
END
GO

-- 9. DISTINCT PRODUCT NAMES (for Search dropdown) ----------------
CREATE PROCEDURE GetProductNames
AS
BEGIN
    SELECT DISTINCT ProductName FROM Product ORDER BY ProductName;
END
GO

-- 10. Sample data matching the screenshot ------------------------
INSERT INTO Product (CategoryName, ProductName, ProductPrice, IsActive, CreatedBy, UpdatedBy) VALUES
('Electronics', 'Smartphone', 15999.00, 1, 1, 1),
('Electronics', 'Laptop',     45999.00, 1, 1, 1),
('Electronics', 'Headphones',  2199.00, 1, 1, 1),
('Electronics', 'Smartwatch',  3999.00, 1, 1, 1);
GO
