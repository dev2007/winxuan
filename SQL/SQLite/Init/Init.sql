REPLACE INTO [Users](Name,UserName,[Password]) VALUES('admin','admin','password+2007');

REPLACE INTO [UserRoles](RoleDescription)
VALUES
('创建者'),('上传'),('仅下载')