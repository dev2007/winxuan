CREATE TABLE [Users]
(
 Id integer primary key AUTOINCREMENT,
 Name nvarchar(20) not null,
 UserName nvarchar(20) not null,
 Password varchar(100) not null
)