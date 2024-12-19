create database game
use game

CREATE TABLE Users (
    UserId INT PRIMARY KEY IDENTITY,
    Username NVARCHAR(50) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(255) NOT NULL,
    Score INT DEFAULT 0
);

CREATE TABLE Leaderboard (
    LeaderboardId INT PRIMARY KEY IDENTITY,
    UserId INT NOT NULL FOREIGN KEY REFERENCES Users(UserId),
    Score INT NOT NULL,
    CreatedAt DATETIME DEFAULT GETDATE()
);

INSERT INTO Users (Username, PasswordHash, Score)
VALUES 
('testUser', '1234', 0); 


SELECT Username, PasswordHash FROM Users;
