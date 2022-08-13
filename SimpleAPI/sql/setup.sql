CREATE DATABASE SimpleApp;
GO

USE SimpleApp;
GO

CREATE TABLE Employees (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Name VARCHAR(30) NOT NULL,
    Username VARCHAR(30) NOT NULL,
    Password VARCHAR(100) NOT NULL,
    Gender VARCHAR(10) NOT NULL,
    Phone VARCHAR(30) NOT NULL,
    Email VARCHAR(50) NOT NULL
);
GO

CREATE TABLE Productions (
    NameProduction VARCHAR(30) NOT NULL,
    Amount INT NOT NULL,
    Status BIT,
    PRIMARY KEY (NameProduction)
);
GO

CREATE TABLE Bills (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    EmployeeId INT NOT NULL,
    ProductionId VARCHAR(30) NOT NULL,
    DaySell VARCHAR(20) NOT NULL,
    Status BIT NOT NULL
);
GO