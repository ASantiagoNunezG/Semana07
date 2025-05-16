use Semana07
CREATE PROCEDURE USP_listar_customers
AS
BEGIN
    SELECT customer_id, name, address, phone, active
    FROM customers
    WHERE active = 1;
END
GO

USP_listar_customers;

CREATE PROCEDURE USP_find_customers_by_name
    @nombre NVARCHAR(255)
AS
BEGIN
    SELECT customer_id, name, address, phone, active
    FROM customers
    WHERE active = 1 AND name LIKE '%' + @nombre + '%';
END
GO

USP_find_customers_by_name 'DOE'

CREATE OR ALTER PROCEDURE USP_insert_customer
    @name NVARCHAR(255),
    @address NVARCHAR(255),
    @phone NVARCHAR(15)
AS
BEGIN
    INSERT INTO customers (name, address, phone, active)
    VALUES (@name, @address, @phone, 1)
END


CREATE PROCEDURE USP_update_customer
    @customer_id INT,
    @name NVARCHAR(255),
    @address NVARCHAR(255),
    @phone NVARCHAR(15)
AS
BEGIN
    UPDATE customers
    SET name = @name,
        address = @address,
        phone = @phone
    WHERE customer_id = @customer_id
END

EXEC USP_update_customer 
    @customer_id = 4,
    @name = 'Santiago Nuñez Garcia',
    @address = 'Cascanueces 123',
    @phone = '999888999';

--Eliminando lógicamente --
CREATE PROCEDURE USP_delete_customer
    @customer_id INT
AS
BEGIN
    UPDATE customers
    SET active = 0
    WHERE customer_id = @customer_id
END

USP_delete_customer 5

select * from customers