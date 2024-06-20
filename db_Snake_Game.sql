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