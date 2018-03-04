DROP TABLE IF EXISTS Students;

CREATE TABLE Students (
	Id int IDENTITY(1,1) PRIMARY KEY,
	[Name] nvarchar(200) NOT NULL,
	Email nvarchar(200) NOT NULL UNIQUE,
);

INSERT INTO Students ([Name], Email)
VALUES
	('Andreas', 'andreas@edu.example.com'),
	('Børge', 'boerge@edu.example.com'),
	('Christian', 'christian@edu.example.com'),
	('Dennis', 'dennis@edu.example.com'),
	('Erik', 'erik@edu.example.com')
;