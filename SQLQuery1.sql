CREATE TABLE [dbo].[Restaurant] (
    [ReId]    int  identity(1,1) NOT NULL,
    [Name] VARCHAR (20)   NOT NULL,
    [Phone]     VARCHAR (20)   NOT NULL,
	[email]     VARCHAR (20)   NOT NULL,
    [postcode]   VARCHAR (20)   NOT NULL,
	[latitude]    NUMERIC (10, 8) NOT NULL,
    [longitude]   NUMERIC (11, 8) NOT NULL,
    PRIMARY KEY CLUSTERED ([ReId] ASC)
);
GO

CREATE TABLE [dbo].[Recommendation] (
    [RecId]    int identity(1,1) NOT NULL,
    [ReId]    int  NOT NULL,
    [category] VARCHAR (20)   NOT NULL,
    [Name]  VARCHAR (20)   NOT NULL,
    [price]       NUMERIC (5,2)           not NULL,
    [description]   VARCHAR (50)    NULL,
    PRIMARY KEY CLUSTERED ([RecId] ASC),
    foreign key ([ReID]) references [dbo].[Restaurant] ([ReId]) on delete cascade
);
GO

CREATE TABLE [dbo].[PersonalInfo] (
    [UserID]    NVARCHAR (128) NOT NULL,
    [FirstName] VARCHAR (20)   NOT NULL,
    [LastName]  VARCHAR (20)   NOT NULL,
    [Phone]     VARCHAR (20)   NOT NULL,
	[email]     VARCHAR (20)   NOT NULL,
    [Address]   VARCHAR (50)   NOT NULL,
    PRIMARY KEY CLUSTERED ([UserID] ASC)
);
GO

CREATE TABLE [dbo].[Reservation] (
    [UserID]    NVARCHAR (128) NOT NULL,
    [ReId]   int NOT NULL,
    [ReserId]    int identity(1,1) NOT NULL,
    [Date]    DATE    NOT NULL,
    [Seat]     VARCHAR (20)   NOT NULL,
    [reser_description]   VARCHAR (50)   NOT NULL,
	PRIMARY KEY CLUSTERED ([ReserId] ASC),
    foreign key ([ReId]) references [dbo].[Restaurant] ([ReId]) on delete cascade,
	foreign key ([UserID]) references [dbo].[PersonalInfo] ([UserID]) on delete cascade
);
GO