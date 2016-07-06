CREATE TABLE [Users]
(
	Id int identity(1,1) not null primary key,
	Name nvarchar(20) not null,
	UserName nvarchar(20) not null,
	[Password] varchar(100) not null
)