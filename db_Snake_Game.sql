IF EXISTS(SELECT NAME FROM SYS.databases WHERE NAME = 'db_Snake_Game')
BEGIN
	DROP DATABASE db_Snake_Game;
END

CREATE DATABASE db_Snake_Game;

USE  db_Snake_Game;

CREATE TABLE Users (
    [User] VARCHAR(255) NOT NULL unique,
    Password VARCHAR(255),
	photo VARBINARY(MAX)
);

SELECT * FROM Users

CREATE TABLE HighScore (
    id INT IDENTITY(1,1) PRIMARY KEY,
    [user] VARCHAR(255),
    time VARCHAR(255),
    score VARCHAR(255),
    CONSTRAINT FK_User4 FOREIGN KEY ([user]) REFERENCES Users([user])
);

SELECT * FROM HighScore