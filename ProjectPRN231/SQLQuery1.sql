drop database toDo2
create database toDo2
CREATE TABLE Users (
    Id INT IDENTITY  PRIMARY KEY , 
    RoleId INT,
    FirstName NVARCHAR(100),
    LastName NVARCHAR(100),
    Mobile NVARCHAR(20),
    Email NVARCHAR(100),
    UserName NVARCHAR(50),
    Password NVARCHAR(255),
    Address NVARCHAR(MAX),
    Dob DATE,
    FOREIGN KEY (RoleId) REFERENCES Roles(Id)
);
CREATE TABLE Roles (
    Id INT IDENTITY  PRIMARY KEY,
    Name NVARCHAR(50)
);
CREATE TABLE Plants (
    Id INT IDENTITY  PRIMARY KEY,
    Name NVARCHAR(100),
    Description NVARCHAR(MAX),
    Title NVARCHAR(100),
    UserId INT,
	    MilestoneId INT,
		    FOREIGN KEY (MilestoneId) REFERENCES Milestones(Id),

    FOREIGN KEY (UserId) REFERENCES Users(Id)
);
CREATE TABLE Tasks (
    Id INT IDENTITY  PRIMARY KEY,
    Name NVARCHAR(100),
    Title NVARCHAR(100),
    Description NVARCHAR(MAX),
    PlantId INT,
    MilestoneId INT,
    TaskParentId INT,
    FOREIGN KEY (PlantId) REFERENCES Plants(Id),
    FOREIGN KEY (MilestoneId) REFERENCES Milestones(Id),
    FOREIGN KEY (TaskParentId) REFERENCES Tasks(Id)
);

CREATE TABLE Tags (
    Id INT IDENTITY  PRIMARY KEY,
    Name NVARCHAR(50)
);
CREATE TABLE Milestones (
    Id INT IDENTITY  PRIMARY KEY,
    Type NVARCHAR(50),
    CreatedAt DATETIME,
    UpdatedAt DATETIME,
    PlannedStartDate DATETIME,
    PlannedEndDate DATETIME,
    ActualStartDate DATETIME,
    ActualEndDate DATETIME,
    Status NVARCHAR(50),
);
CREATE TABLE Plant_Tag (
    Id INT IDENTITY  PRIMARY KEY,
    PlantId INT,
    TagId INT,
    FOREIGN KEY (PlantId) REFERENCES Plants(Id),
    FOREIGN KEY (TagId) REFERENCES Tags(Id)
);

CREATE TABLE Task_Tag (
    Id INT IDENTITY  PRIMARY KEY,
    TaskId INT,
    TagId INT,
    FOREIGN KEY (TaskId) REFERENCES Tasks(Id),
    FOREIGN KEY (TagId) REFERENCES Tags(Id)
);

CREATE TABLE Comments (
    Id INT IDENTITY  PRIMARY KEY,
    TaskId INT,
    UserId INT,
    Content NVARCHAR(MAX),
    CreatedAt DATETIME,
    FOREIGN KEY (TaskId) REFERENCES Tasks(Id),
    FOREIGN KEY (UserId) REFERENCES Users(Id)
);
CREATE TABLE Attachments (
    Id INT IDENTITY  PRIMARY KEY,
    TaskId INT,
    FilePath NVARCHAR(MAX),
    UploadedBy INT,
    UploadedAt DATETIME,
    FOREIGN KEY (TaskId) REFERENCES Tasks(Id),
    FOREIGN KEY (UploadedBy) REFERENCES Users(Id)
);
CREATE TABLE UserSettings (
    Id INT IDENTITY  PRIMARY KEY,
    UserId INT,
    DarkMode bit,
    EmailPopup bit,
    Popup bit,
    FOREIGN KEY (UserId) REFERENCES Users(Id)
);
