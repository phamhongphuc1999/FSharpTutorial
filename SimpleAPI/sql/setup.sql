CREATE DATABASE FsharpApp;
GO

USE FsharpApp;
GO

CREATE TABLE Employees (
    id INT PRIMARY KEY AUTO_INCREMENT,
    username VARCHAR(30) NOT NULL,
    password VARCHAR(100) NOT NULL,
    email VARCHAR(50)
);
GO

INSERT INTO Employees (username, password, email)
VALUES ('PhamHongPhuc', '123456789', 'php@gmail.com'),
        ('PhamHongPhuc1', '123456789', 'php@gmail.com'),
        ('PhamHongPhuc2', '123456789', 'php@gmail.com'),
        ('PhamHongPhuc3', '123456789', 'php@gmail.com'),
        ('PhamHongPhuc4', '123456789', 'php@gmail.com');
GO

CREATE TABLE Productions (
    id INT PRIMARY KEY AUTO_INCREMENT,
    name VARCHAR(30) NOT NULL,
    amount INT NOT NULL
);
GO

INSERT INTO Productions (name, amount)
VALUES ('production1', 100),
        ('production2', 200),
        ('production3', 1000);