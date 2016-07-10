CREATE TABLE [TeamWithUsers]
(
	Id	integer primary key AUTOINCREMENT,
	TeamId	integer not null,
	UserId integer not null,
  UserRole integer not null
)