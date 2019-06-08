/*
 
    FOR JSON PATH - Full code example
 
*/
 
PRINT ('FOR JSON PATH - Full code example' + CHAR(13) + '--------------------------------------------');
 
-- 1) Create the 'JsonForTestDatabase', if required
IF (DB_ID('JsonForTestDatabase') IS NULL)
BEGIN
 
    PRINT ('Creating the ''JsonForTestDatabase'' sample database.');
 
    CREATE DATABASE JsonForTestDatabase;
 
END;
GO
 
-- 2) Explicitly switch the session to 'point' at the JsonForTestDatabase
USE [JsonForTestDatabase]
GO
 
-- 3) Create the test tables for example purposes (Robot/House tables)
IF(OBJECT_ID('Robots', 'U') IS NULL)
BEGIN
 
    PRINT ('Creating the ''Robots'' table.');
 
    CREATE TABLE dbo.Robots
    (
        Id                  BIGINT          NOT NULL    PRIMARY KEY     IDENTITY(1, 1)
        , [Name]            NVARCHAR(20)    NOT NULL
        , Active            BIT             NOT NULL    DEFAULT(1)
    );
 
END;
 
IF(OBJECT_ID('RobotDanceMoves', 'U') IS NULL)
BEGIN
 
    PRINT ('Creating the ''RobotDanceMoves'' table.');
 
    CREATE TABLE dbo.RobotDanceMoves
    (
        Id                  BIGINT          NOT NULL    PRIMARY KEY     IDENTITY(1, 1)
        , RobotId           BIGINT          FOREIGN KEY (RobotId) REFERENCES dbo.Robots(Id)
        , [Name]            NVARCHAR(20)    NOT NULL
        , FavouriteMove     BIT             NOT NULL    DEFAULT(0)
    );
 
END;
 
IF(OBJECT_ID('Houses', 'U') IS NULL)
BEGIN
 
    PRINT ('Creating the ''Houses'' table.');
 
    CREATE TABLE dbo.Houses
    (
        Id                  BIGINT          PRIMARY KEY     IDENTITY(1, 1)
        , [Name]            NVARCHAR(20)
    );
 
END;
 
IF(OBJECT_ID('Rooms', 'U') IS NULL)
BEGIN
 
    PRINT ('Creating the ''Rooms'' table.');
 
    CREATE TABLE dbo.Rooms 
    ( 
        Id                  BIGINT          PRIMARY KEY     IDENTITY(1, 1)
        , HouseId           BIGINT          FOREIGN KEY (HouseId) REFERENCES dbo.Houses(Id)
        , [Name]            NVARCHAR(20)
    );
 
END;
 
IF(OBJECT_ID('RoomObjects', 'U') IS NULL)
BEGIN
 
    PRINT ('Creating the ''RoomObjects'' table.');
 
    CREATE TABLE dbo.RoomObjects 
    ( 
        Id                  BIGINT          PRIMARY KEY     IDENTITY(1, 1)
        , RoomId            BIGINT          FOREIGN KEY (RoomId) REFERENCES dbo.Rooms(Id)
        , [Name]            NVARCHAR(20)
    );
 
END;
 
-- 4) Drop/recreate the Utility SPs
IF(OBJECT_ID('GetRobotsWithFavouriteDanceMoveJson', 'P') IS NOT NULL)
BEGIN
     
    PRINT ('Dropping the ''GetRobotsWithFavouriteDanceMoveJson'' stored procedure.');
 
    DROP PROCEDURE dbo.GetRobotsWithFavouriteDanceMoveJson;
 
END;
 
PRINT ('Creating the ''GetRobotsWithFavouriteDanceMoveJson'' stored procedure.');
GO
 
CREATE PROCEDURE dbo.GetRobotsWithFavouriteDanceMoveJson
AS
BEGIN
 
    -- SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements.
    SET NOCOUNT ON;
 
    -- Produce a very simple, flat representation of a Robot and their favourite dance move
    SELECT
    r.Id
    , r.[Name]
    , r.Active
    , rdm.[Name] AS [FavouriteDanceMove]
    FROM dbo.Robots r
        INNER JOIN dbo.RobotDanceMoves rdm ON
        (
            r.Id = rdm.RobotId
            AND rdm.FavouriteMove = 1
        )
    FOR JSON PATH;
 
    -- Return execution status of success
    RETURN 0;
 
END;
GO
 
IF(OBJECT_ID('GetFormattedRobotsWithFavouriteDanceMoveJson', 'P') IS NOT NULL)
BEGIN
     
    PRINT ('Dropping the ''GetFormattedRobotsWithFavouriteDanceMoveJson'' stored procedure.');
 
    DROP PROCEDURE dbo.GetFormattedRobotsWithFavouriteDanceMoveJson;
 
END;
     
PRINT ('Creating the ''GetFormattedRobotsWithFavouriteDanceMoveJson'' stored procedure.');
GO
 
CREATE PROCEDURE dbo.GetFormattedRobotsWithFavouriteDanceMoveJson
AS
BEGIN
 
    -- SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements.
    SET NOCOUNT ON;
 
    -- Illustration of how the JSON can be manipulated and formatted further by altering column aliases
    SELECT
    r.Id AS [Id]
    , r.[Name] AS [RobotCoreDetails.Name]
    , r.Active AS [RobotCoreDetails.Active]
    , rdm.[Name] AS [RobotDanceMove.FavouriteDanceMove]
    FROM dbo.Robots r
        INNER JOIN dbo.RobotDanceMoves rdm ON
        (
            r.Id = rdm.RobotId
            AND rdm.FavouriteMove = 1
        )
    FOR JSON PATH;
 
    -- Return execution status of success
    RETURN 0;
 
END;
GO
 
IF(OBJECT_ID('GetRobotsWithFavouriteDanceMoveAndRootJson', 'P') IS NOT NULL)
BEGIN
 
    PRINT ('Dropping the ''GetRobotsWithFavouriteDanceMoveAndRootJson'' stored procedure.');
     
    DROP PROCEDURE dbo.GetRobotsWithFavouriteDanceMoveAndRootJson;
 
END;
 
PRINT ('Creating the ''GetRobotsWithFavouriteDanceMoveAndRootJson'' stored procedure.');
GO
 
CREATE PROCEDURE dbo.GetRobotsWithFavouriteDanceMoveAndRootJson
AS
BEGIN
 
    -- SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements.
    SET NOCOUNT ON;
 
    -- Illustration of adding a ROOT clause to wrap the produced JSON content
    SELECT
    r.Id AS [Id]
    , r.[Name] AS [RobotCoreDetails.Name]
    , r.Active AS [RobotCoreDetails.Active]
    , rdm.[Name] AS [RobotDanceMove.FavouriteDanceMove]
    FROM dbo.Robots r
        INNER JOIN dbo.RobotDanceMoves rdm ON
        (
            r.Id = rdm.RobotId
            AND rdm.FavouriteMove = 1
        )
    FOR JSON PATH, ROOT('Robots');
 
    -- Return execution status of success
    RETURN 0;
 
END;
GO
 
IF(OBJECT_ID('GetFullRobotDetailAsJson', 'P') IS NOT NULL)
BEGIN
 
    PRINT ('Dropping the ''GetFullRobotDetailAsJson'' stored procedure.');
     
    DROP PROCEDURE dbo.GetFullRobotDetailAsJson;
 
END;
 
PRINT ('Creating the ''GetFullRobotDetailAsJson'' stored procedure.');
GO
 
CREATE PROCEDURE dbo.GetFullRobotDetailAsJson
AS
BEGIN
 
    -- SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements.
    SET NOCOUNT ON;
 
    /*
    -- Represents the incorrect way to produce correctly nested results, on this occassion
    SELECT 
    r.Id AS [Id]
    , r.[Name] AS [RobotCoreDetails.Name]
    , r.Active AS [RobotCoreDetails.Active]
    , rdm.[Name] AS [RobotDanceMove.FavouriteDanceMove]
    FROM dbo.Robots r
        INNER JOIN dbo.RobotDanceMoves rdm ON r.Id = rdm.RobotId
    FOR JSON PATH, ROOT('Robots');
 
    -- Correctly nested results, illustration one
    SELECT 
    r.Id AS [Id]
    , r.[Name] AS [RobotCoreDetails.Name]
    , r.Active AS [RobotCoreDetails.Active] 
    ,
    (
        SELECT
        rdm.[Name]
        , 
        -- Use of CASE statement just to show that normal manipulation of data is possible, as expected
        CASE
            WHEN rdm.FavouriteMove = 1
            THEN 'Yep'
            ELSE 'Nope'
        END AS [FavouriteMove]
        FROM dbo.RobotDanceMoves rdm
        WHERE rdm.RobotId = r.Id
        FOR JSON PATH
    ) AS [RobotDanceMoves]
    FROM dbo.Robots r
    FOR JSON PATH, ROOT('Robots');
    */
 
    -- Correctly nested results, illustration two (RobotDanceMoves abstracted into 'Move' using aliases)
    SELECT
    r.Id AS [Id]
    , r.[Name] AS [RobotCoreDetails.Name]
    , r.Active AS [RobotCoreDetails.Active] 
    ,
    (
        SELECT
        rdm.[Name] AS [Move.Name]
        , 
        -- Use of CASE statement just to show that normal manipulation of data is possible, as expected
        CASE
            WHEN rdm.FavouriteMove = 1
            THEN 'Yep'
            ELSE 'Nope'
        END AS [Move.Favourite]
        FROM dbo.RobotDanceMoves rdm
        WHERE rdm.RobotId = r.Id
        FOR JSON PATH
    ) AS [RobotDanceMoves]
    FROM dbo.Robots r
    FOR JSON PATH, ROOT('Robots');
 
    -- Return execution status of success
    RETURN 0;
 
END;
GO
 
IF(OBJECT_ID('GetHouseDetailAsJson', 'P') IS NOT NULL)
BEGIN
 
    PRINT ('Dropping the ''GetHouseDetailAsJson'' stored procedure.');
     
    DROP PROCEDURE dbo.GetHouseDetailAsJson;
 
END;
 
PRINT ('Creating the ''GetHouseDetailAsJson'' stored procedure.');
GO
 
CREATE PROCEDURE dbo.GetHouseDetailAsJson
AS
BEGIN
 
    -- SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements.
    SET NOCOUNT ON;
 
    /*
    -- Illustration of producing a query that handles one-to-many relationships gracefully
    SELECT 
    h.Id AS [Id]
    , h.[Name] AS [Name]
    ,
    (
        SELECT
        r.Id AS [Id]
        , r.[Name] AS [Name]
        ,
        (
            SELECT
            ro.Id AS [Id]
            , ro.[Name] AS [Name]
            FROM dbo.RoomObjects ro
            WHERE ro.RoomId = r.Id
            FOR JSON PATH
        ) AS [RoomObjects]
        FROM dbo.Rooms r
        WHERE r.HouseId = h.Id
        FOR JSON PATH
    ) AS [Rooms]
    FROM dbo.Houses h
    FOR JSON PATH, ROOT('Houses');
    */
 
    -- Example extended with some more aliases to format the structure further 
    -- (full nesting of House -> Rooms -> Room -> RoomObjects -> Object)
    SELECT
    h.Id AS [House.Id]
    , h.[Name] AS [House.Name]
    ,
    (
        SELECT
        r.Id AS [Room.Id]
        , r.[Name] AS [Room.Name]
        ,
        (
            SELECT
            ro.Id AS [Object.Id]
            , ro.[Name] AS [Object.Name]
            FROM dbo.RoomObjects ro
            WHERE ro.RoomId = r.Id
            FOR JSON PATH
        ) AS [Room.RoomObjects]
        FROM dbo.Rooms r
        WHERE r.HouseId = h.Id
        FOR JSON PATH
    ) AS [House.Rooms]
    FROM dbo.Houses h
    FOR JSON PATH, ROOT('Houses');
 
    -- Return execution status of success
    RETURN 0;
 
END;
GO
 
-- 5) Insert sample data into the test tables...part 1, robots...
IF NOT EXISTS
(
    SELECT 1
    FROM dbo.Robots
)
BEGIN
 
    PRINT ('Adding dbo.Robot sample data.');
 
    INSERT INTO dbo.Robots
    (
        [Name]
        , Active
    )
    VALUES
    ('Barry', 1)
    , ('Steve', 0)
    , ('Dave', 1)
    , ('Zoe', 1)
    , ('Claire', 1)
    , ('Tracey', 0);
 
END;
 
IF NOT EXISTS
(
    SELECT 1
    FROM dbo.RobotDanceMoves
)
BEGIN
 
    PRINT ('Adding dbo.RobotDanceMoves sample data.');
     
    --If RobotDanceMoves have been inserted yet the produce a mock sample of randomised data where Robots receive
    --all three dance moves but a 'favourite' on the fly
    INSERT INTO dbo.RobotDanceMoves
    (
         RobotId
         , [Name]
         , FavouriteMove
    )
    SELECT
    r.Id
    , rm.DanceMove
    , rm.FavouriteMove
    FROM dbo.Robots r
        CROSS APPLY 
        ( 
            SELECT
            TOP 3 
            am.DanceMove
            , 
            CASE
                WHEN ROW_NUMBER() OVER (ORDER BY NEWID()) = 3
                THEN CAST(1 AS BIT)
                ELSE CAST(0 AS BIT)
            END AS [FavouriteMove]
            FROM
            (
                SELECT
                'Moonwalk' AS [DanceMove]
                UNION
                SELECT
                'Thunder Clap'
                UNION
                SELECT
                'The Robot'
            ) AS am
            WHERE r.Id = r.Id
            ORDER BY NEWID()
        ) AS rm;
 
END;
 
--...Part two, houses (very fixed, just as an illustration)
IF NOT EXISTS
(
    SELECT 1
    FROM dbo.Houses
)
BEGIN
 
    PRINT ('Adding dbo.Houses sample data.');
 
    INSERT INTO dbo.Houses
    (
        [Name]
    )
    VALUES
    ('House One')
    , ('House Two');
 
END;
 
IF NOT EXISTS
(
    SELECT 1
    FROM dbo.Rooms
)
BEGIN
 
    PRINT ('Adding dbo.Rooms sample data.');
 
    INSERT INTO dbo.Rooms
    (
        HouseId
        , [Name]
    )
    VALUES
    (1, 'Lounge')
    , (1, 'Kitchen')
    , (2, 'Lounge Diner')
    , (2, 'Kitchen Utility');
 
END;
 
IF NOT EXISTS
(
    SELECT 1
    FROM dbo.RoomObjects
)
BEGIN
 
    PRINT ('Adding dbo.RoomObjects sample data.');
 
    INSERT INTO dbo.RoomObjects
    (
        RoomId
        , [Name]
    )
    VALUES
    (1, 'Lamp')
    , (1, 'Sofa')
    , (2, 'Knife')
    , (2, 'Kettle')
    , (3, 'Coffee Table')
    , (3, 'Armchair')
    , (4, 'Coffee Machine')
    , (4, 'Microwave');
 
END;
GO
 
--6) Execute all of the sample stored procedures
PRINT (CHAR(13) + 'Processing complete...running example stored procedures...');
 
EXEC dbo.GetRobotsWithFavouriteDanceMoveJson;
EXEC dbo.GetFormattedRobotsWithFavouriteDanceMoveJson;
EXEC dbo.GetRobotsWithFavouriteDanceMoveAndRootJson;
EXEC dbo.GetFullRobotDetailAsJson;
EXEC dbo.GetHouseDetailAsJson;