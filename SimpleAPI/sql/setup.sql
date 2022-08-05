CREATE DATABASE FsharpApp;
GO

USE FsharpApp;
GO

CREATE TABLE Employees (
    id INT PRIMARY KEY AUTO_INCREMENT,
    username VARCHAR(30) NOT NULL,
    password VARCHAR(100) NOT NULL,
    email VARCHAR(50) NOT NULL
);
GO

INSERT INTO Employees (username, password, email)
VALUES ('Pham Hong Phuc', '123456789', 'php@gmail.com'),
        ('Pham Hong Phuoc', '123456789', 'php1@gmail.com'),
        ('Nguyen Van Anh', '123456789', 'nva@gmail.com'),
        ('Phan Van Nam', '123456789', 'pvn@gmail.com'),
        ('Khuong Trung Quoc', '123456789', 'ktq@gmail.com');
GO

CREATE TABLE Productions (
    name VARCHAR(30) NOT NULL,
    amount INT NOT NULL,
    PRIMARY KEY (name)
);
GO

INSERT INTO Productions (name, amount)
VALUES ('production1', 100),
        ('production2', 200),
        ('production3', 1000),
        ('production4', 100),
        ('production5', 200),
        ('production6', 1000);
GO

CREATE TABLE Bills (
    id INT PRIMARY KEY AUTO_INCREMENT,
    employeeId INT NOT NULL,
    productionId VARCHAR(30) NOT NULL
);
GO

INSERT INTO Bills (employeeId, productionId)
VALUES (1, 'production1'),
        (2, 'production2'),
        (3, 'production3'),
        (1, 'production4');