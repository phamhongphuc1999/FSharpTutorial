USE SimpleApp;
GO

INSERT INTO Employees (Name, Username, Password, Gender, Phone, Email)
VALUES ('Pham Hong Phuc', 'phamhongphuc', '123456789', 'male', '123456789', 'php@gmail.com'),
        ('Pham Hong Phuoc', 'phamhongphuoc', '123456789', 'male', '123456789', 'php1@gmail.com'),
        ('Nguyen Van Anh', 'nguyenvananh', '123456789', 'female', '123456789', 'nva@gmail.com'),
        ('Phan Van Nam', 'phamvannam', '123456789', 'male', '123456789', 'pvn@gmail.com'),
        ('Khuong Trung Quoc', 'khuongtrungquoc', '123456789', 'male', '123456789', 'ktq@gmail.com');
GO

INSERT INTO Productions (NameProduction, Amount, Status)
VALUES ('production1', 100, NULL),
        ('production2', 200, NULL),
        ('production3', 1000, NULL),
        ('production4', 100, NULL),
        ('production5', 200, 1),
        ('production6', 1000, 0);
GO

INSERT INTO Bills (EmployeeId, ProductionId, DaySell, Status)
VALUES (1, 'production1', '1659754943', 1),
        (2, 'production2', '1659754943', 1),
        (3, 'production3', '1659754943', 1),
        (1, 'production4', '1659754943', 1);