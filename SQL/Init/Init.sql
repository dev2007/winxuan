IF NOT EXISTS(SELECT TOP 1 1 FROM Users WHERE Name = 'admin')
INSERT INTO [Users](Name,UserName) VALUES('admin','admin')
GO

IF NOT EXISTS(SELECT TOP 1 1 FROM UserSecurities T1 INNER JOIN Users T2 on T1.UserId = T2.Id)
INSERT INTO [UserSecurities](UserId,[Password]) SELECT ID,'password+2007' FROM Users WHERE Name = 'admin'
GO