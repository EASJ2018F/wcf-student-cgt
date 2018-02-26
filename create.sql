DROP TABLE Students;
DROP TABLE Courses;
DROP TABLE CourseStudents;

CREATE TABLE Students (
	Id int IDENTITY(1,1) PRIMARY KEY,
	[Name] nvarchar(200) NOT NULL,
);

CREATE TABLE Courses (
	Id int IDENTITY(1,1) PRIMARY KEY,
	[Name] nvarchar(200) UNIQUE,
);

CREATE TABLE CourseStudents (
	StudentId int REFERENCES Students(Id),
	CourseId int REFERENCES Courses(Id),
	PRIMARY KEY (StudentId, CourseId),
);

INSERT INTO Students ([Name])
VALUES ('Andreas'), ('Børge'), ('Christian'), ('Dennis'), ('Erik');

INSERT INTO Courses ([Name])
VALUES ('Programmering'), ('Teknologi'), ('Systemudvikling');

INSERT INTO CourseStudents ([StudentId], [CourseId])
VALUES (
	(SELECT Id FROM Students WHERE [Name] = 'Andreas'),
	(SELECT Id FROM Courses WHERE [Name] = 'Teknologi')
);
INSERT INTO CourseStudents ([StudentId], [CourseId])
VALUES (
	(SELECT Id FROM Students WHERE [Name] = 'Andreas'),
	(SELECT Id FROM Courses WHERE [Name] = 'Systemudvikling')
);
INSERT INTO CourseStudents ([StudentId], [CourseId])
VALUES (
	(SELECT Id FROM Students WHERE [Name] = 'Børge'),
	(SELECT Id FROM Courses WHERE [Name] = 'Systemudvikling')
);
INSERT INTO CourseStudents ([StudentId], [CourseId])
VALUES (
	(SELECT Id FROM Students WHERE [Name] = 'Dennis'),
	(SELECT Id FROM Courses WHERE [Name] = 'Programmering')
);
INSERT INTO CourseStudents ([StudentId], [CourseId])
VALUES (
	(SELECT Id FROM Students WHERE [Name] = 'Erik'),
	(SELECT Id FROM Courses WHERE [Name] = 'Programmering')
);
INSERT INTO CourseStudents ([StudentId], [CourseId])
VALUES (
	(SELECT Id FROM Students WHERE [Name] = 'Erik'),
	(SELECT Id FROM Courses WHERE [Name] = 'Teknologi')
);
INSERT INTO CourseStudents ([StudentId], [CourseId])
VALUES (
	(SELECT Id FROM Students WHERE [Name] = 'Erik'),
	(SELECT Id FROM Courses WHERE [Name] = 'Systemudvikling')
);