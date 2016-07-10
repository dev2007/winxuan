CREATE TABLE [TeamWithUsers]
(
	Id	int identity(1,1) primary key,
	TeamId	int not null,
	UserId int not null,
	UserRole int not null
)