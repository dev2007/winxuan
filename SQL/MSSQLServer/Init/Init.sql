IF NOT EXISTS(SELECT TOP 1 1 FROM Users WHERE Name = 'admin')
INSERT INTO [Users](Name,UserName,[Password]) VALUES('admin','admin','password+2007')
GO

IF NOT EXISTS(SELECT TOP 1 1 FROM UserRoles)
INSERT INTO [UserRoles](RoleDescription)
VALUES
(
'创建者'
),
(
'上传'
),
(
'仅下载'
)
GO