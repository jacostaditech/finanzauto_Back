
CREATE DATABASE finanzautoDB;
CONTAINMENT = PARTIAL
GO

USE finanzautoDB;
GO

CREATE LOGIN API_BACK WITH PASSWORD = 'PruebaTest123@Finanzauto';
GO

CREATE USER API_BACK FOR LOGIN API_BACK;
GO

ALTER ROLE db_datareader ADD MEMBER API_BACK;
ALTER ROLE db_datawriter ADD MEMBER API_BACK;

GRANT EXECUTE TO API_BACK;

GRANT VIEW DEFINITION TO API_BACK;
GO

ALTER USER API_BACK WITH LOGIN = API_BACK;
GO

EXEC sp_helprotect @username = 'API_BACK';

drop table teacher_courses;
drop table students;
drop table courses;
drop table scores;
drop table teachers;

	CREATE TABLE students(
	id INT IDENTITY(1,1) PRIMARY KEY,
	firstName NVARCHAR(50) NOT NULL,   
	lastName NVARCHAR(50) NOT NULL,
	identification int not null,
	gender CHAR(1) CHECK (gender IN ('M', 'F')),
	creationdate DATETIME NOT NULL,
	lastupdate DATETIME NOT NULL,
	createby int,
	updatedby int
	);
	
	create table courses(
	id INT IDENTITY(1,1) PRIMARY KEY,
	name NVARCHAR(100) NOT NULL,   
	description NVARCHAR(1000) NOT NULL,
	creationdate DATETIME NOT NULL,
	lastupdate DATETIME NOT NULL,
	createby int,
	updatedby int
	);
	
	create table scores(
	id INT IDENTITY(1,1) PRIMARY KEY,       
	    studentId INT NOT NULL,           
	    courseId INT NOT NULL,                
	    score DECIMAL(5,2) NOT NULL,           
	    creationdate DATETIME NOT NULL,
		lastupdate DATETIME NOT NULL,
	createby int,
	updatedby int,
	    CONSTRAINT FK_Scores_Students FOREIGN KEY (studentId)
	    REFERENCES students(id),
	    CONSTRAINT FK_Scores_Courses FOREIGN KEY (courseId)
	    REFERENCES courses(id)
	);
	
	create table teachers(
	id INT IDENTITY(1,1) PRIMARY KEY,
	firstName NVARCHAR(50) NOT NULL,   
	lastName NVARCHAR(50) NOT NULL,
	creationdate DATETIME NOT NULL,
	lastupdate DATETIME NOT NULL,
	createby int,
	updatedby int
	);
	
	CREATE TABLE teacher_courses (
	    id INT IDENTITY(1,1) PRIMARY KEY,
	    teacherId INT NOT NULL,
	    courseId INT NOT NULL,
	    creationdate DATETIME NOT NULL,
	lastupdate DATETIME NOT NULL,
	createby int,
	updatedby int,
	    CONSTRAINT FK_TeacherCourses_Teachers FOREIGN KEY (teacherId)
	    REFERENCES teachers(id),
	    CONSTRAINT FK_TeacherCourses_Courses FOREIGN KEY (courseId)
	    REFERENCES courses(id)
	);
	
	ALTER TABLE finanzautoDB.dbo.students ALTER COLUMN identification bigint NOT NULL;

	
