CREATE TABLE [Teams]
(
	Id	int identity(1,1) primary key,
	TeamName nvarchar(40) not null,
	TeamDescription nvarchar(200) not null
)