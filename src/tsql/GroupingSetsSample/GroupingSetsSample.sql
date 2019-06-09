/* 
 
    Lewis Grint - 18/03/2017
 
    GROUPING SETS WORKING SAMPLE 
    -------------------------------------------------
    1) This example uses four mock database tables (created internally by the script, no work required :op). Run this on a test database of your choice.
        - Staff.
        - Perk.
        - HolidayRequest.
        - SalaryPayment.
    2) Utility, mock data is also inserted by this script (table drop statements, if required, can be found just below this header).
    3) The sample data is then aggregated using GROUP BY via two traditionally structured stored procedures (GetHolidayRequestDaysSumInfo and GetSalarySumInfo). Data is returned (for each grouping) using a separate result set.
    4) Stored procedure variants are then presented that using GROUPING SETS - All GROUP BY variations are returned in a single result set (for illustration purposes only, not saying my grouping selections are actually that helpful in the real world!).
 
*/
 
/*
-- Drop table statements, as and when required
DROP TABLE dbo.HolidayRequest;
DROP TABLE dbo.SalaryPayment;
DROP TABLE dbo.Staff;
DROP TABLE dbo.Perk;
 
*/
 
-- Habitual setting of preferred QUOTED_IDENTIFIER/ANSI_NULL configuration (even if it's not important for this example)
SET QUOTED_IDENTIFIER ON;
GO
SET ANSI_NULLS ON;
GO
 
-- Utility variables for gathering up the correct StaffId, when required (and payment dates)
DECLARE
@StaffId                            INT
, @FebPayDate                       DATE = '2017-02-28'
, @JanPayDate                       DATE = '2017-01-31'
, @DecPayDate                       DATE = '2016-12-31';
 
-- 1) Create sample tables to illustrate how GROUPING SETS work
 
-- Perk table added for nothing more than extra kicks. A staff member can select a company perk (referenced from the Staff table)
IF (OBJECT_ID('dbo.Perk', 'U') IS NULL)
BEGIN
 
    CREATE TABLE dbo.Perk
    (
        Id                          INT             PRIMARY KEY     IDENTITY(1, 1)  NOT NULL
        , PerkName                  NVARCHAR(30)                                    NOT NULL
        , INDEX IX_Perk_PerkName    NONCLUSTERED (PerkName)                 
    );
 
END;
 
-- NOTE: I won't be including more indexes from this point forward, to try and keep the example light(er)
 
-- Staff table, which for the sake of our example contains first name, last name, job title, department and a fk to 'perks'
IF (OBJECT_ID('dbo.Staff', 'U') IS NULL)
BEGIN
 
    CREATE TABLE dbo.Staff
    (
        Id                          INT             PRIMARY KEY     IDENTITY(1, 1)  NOT NULL
        , FirstName                 NVARCHAR(50)                                    NOT NULL
        , LastName                  NVARCHAR(50)                                    NOT NULL
        , JobTitle                  NVARCHAR(50)                                    NOT NULL
        , Department                NVARCHAR(50)                                    NOT NULL
        , Perk                      INT                                             NOT NULL
        , CONSTRAINT FK_Staff_Perk  FOREIGN KEY (Perk) REFERENCES dbo.Perk (Id)
    );
 
END;
 
-- SalaryPayment table, which contains multiple rows for each staff member (one for each salary payment)
IF (OBJECT_ID('dbo.SalaryPayment', 'U') IS NULL)
BEGIN
 
    CREATE TABLE dbo.SalaryPayment
    (
        Id                          INT             PRIMARY KEY     IDENTITY(1, 1)  NOT NULL
        , StaffId                   INT                                             NOT NULL
        , PaymentDate               DATE                                            NOT NULL
        , Amount                    MONEY
        , CONSTRAINT FK_SalaryPayment_Staff         FOREIGN KEY (StaffId) REFERENCES dbo.Staff (Id)
    );
 
END;
 
-- HolidayRequest table, which contains multiple rows for each staff member (one for each holiday request)
IF (OBJECT_ID('dbo.HolidayRequest', 'U') IS NULL)
BEGIN
 
    CREATE TABLE dbo.HolidayRequest
    (
        Id                          INT             PRIMARY KEY     IDENTITY(1, 1)  NOT NULL
        , StaffId                   INT                                             NOT NULL
        , DateFrom                  DATE                                            NOT NULL
        , DateTo                    DATE                                            NOT NULL
        -- Final column is computed based on the difference between the start and end date (requested), in days. Simple, but fits for the purposes of the example (obviously, doesn't take into account weekends, bank holiday, etc.)
        , NumberOfDaysRequested     AS (DATEDIFF(DAY, DateFrom, DateTo) + 1)
        , CONSTRAINT FK_HolidayRequest_Staff        FOREIGN KEY (StaffId) REFERENCES dbo.Staff (Id)
    );
 
END;
 
--2) Insert test data into each table
 
-- Perk table test data
IF NOT EXISTS
(
    SELECT *
    FROM dbo.Perk p
)
BEGIN
 
    INSERT INTO dbo.Perk
    (
        PerkName
    )
    VALUES
    (
        'Free ice cream'
    )
    ,
    (
        'Free parking'
    )
    ,
    (
        'Beer on arrival'
    )
    ,
    (
        'Pat on the back'
    );
 
END;
 
-- Staff table test data (including perk info)
IF NOT EXISTS
(
    SELECT *
    FROM dbo.Staff s
)
BEGIN
 
    INSERT INTO dbo.Staff
    (
        FirstName
        , LastName
        , JobTitle
        , Department
        , Perk  
    )
    VALUES
    (
        'Steve'
        , 'Stevenson'
        , 'Head Honcho'
        , 'Ivory Tower'
        , 
        (
            SELECT 
            Id 
            FROM dbo.Perk p 
            WHERE p.PerkName = 'Free ice cream'
        )
    )
    ,
    (
        'Marie'
        , 'Pritchard'
        , 'Team Manager'
        , 'Ivory Tower'
        , 
        (
            SELECT 
            Id 
            FROM dbo.Perk p 
            WHERE p.PerkName = 'Free parking'
        )
    )
    ,
    (
        'Judy'
        , 'Dench'
        , 'Team Manager'
        , 'Island Retreat'
        , 
        (
            SELECT 
            Id 
            FROM dbo.Perk p 
            WHERE p.PerkName = 'Free ice cream'
        )
    )
    ,
    (
        'Dave'
        , 'Dodger'
        , 'Chief Work Dodger'
        , 'Store Cupboard'
        , 
        (
            SELECT 
            Id 
            FROM dbo.Perk p 
            WHERE p.PerkName = 'Beer on arrival'
        )
    )
    -- There's another Dave Dodger at the company, but he is actually a pretty useful chap
    ,
    (
        'Dave'
        , 'Dodger'
        , 'Hard Worker'
        , 'Store Cupboard'
        , 
        (
            SELECT
            Id 
            FROM dbo.Perk p 
            WHERE p.PerkName = 'Pat on the back'
        )
    )
    ,
    (
        'Bob'
        , 'Boots'
        , 'Handle Cranker'
        , 'Main Office'
        ,
        (
            SELECT
            Id 
            FROM dbo.Perk p 
            WHERE p.PerkName = 'Pat on the back'
        )
    )
    ,
    (
        'Janet'
        , 'Timms'
        , 'Handle Cranker'
        , 'Main Office'
        , 
        (
            SELECT
            Id 
            FROM dbo.Perk p 
            WHERE p.PerkName = 'Pat on the back'
        )
    );
 
END;
 
-- SalaryPayment table test data
IF NOT EXISTS
(
    SELECT *
    FROM dbo.SalaryPayment sp
)
BEGIN
     
    -- Steve Stevenson | Head Honcho
    SELECT
    @StaffId = 
    s.Id
    FROM dbo.Staff s
    WHERE
    (
        s.FirstName = 'Steve'
        AND s.LastName = 'Stevenson'
        AND s.JobTitle = 'Head Honcho'
    );
 
    INSERT INTO dbo.SalaryPayment
    (
        StaffId
        , PaymentDate
        , Amount
    )
    VALUES
    (
        @StaffId
        , @DecPayDate
        , 5580.50
    )
    ,
    (
     
        @StaffId
        , @JanPayDate
        , 5240.50
    )
    ,
    (
     
        @StaffId
        , @FebPayDate
        , 5580.50
    );
 
    -- Marie Pritchard | Team Manager
    SELECT
    @StaffId = 
    s.Id
    FROM dbo.Staff s
    WHERE
    (
        s.FirstName = 'Marie'
        AND s.LastName = 'Pritchard'
        AND s.JobTitle = 'Team Manager'
    );
 
    INSERT INTO dbo.SalaryPayment
    (
        StaffId
        , PaymentDate
        , Amount
    )
    VALUES
    (
        @StaffId
        , @DecPayDate
        , 2500.75
    )
    ,
    (
     
        @StaffId
        , @JanPayDate
        , 2425.15
    )
    ,
    (
     
        @StaffId
        , @FebPayDate
        , 2425.15
    );
 
    -- Judy Dench | Team Manager
    SELECT
    @StaffId = 
    s.Id
    FROM dbo.Staff s
    WHERE
    (
        s.FirstName = 'Judy'
        AND s.LastName = 'Dench'
        AND s.JobTitle = 'Team Manager'
    );
 
    INSERT INTO dbo.SalaryPayment
    (
        StaffId
        , PaymentDate
        , Amount
    )
    VALUES
    (
        @StaffId
        , @DecPayDate
        , 2495.75
    )
    ,
    (
     
        @StaffId
        , @JanPayDate
        , 2400.15
    )
    ,
    (
     
        @StaffId
        , @FebPayDate
        , 2400.15
    );
 
    -- Dave Dodger | Chief Work Dodger
    SELECT
    @StaffId = 
    s.Id
    FROM dbo.Staff s
    WHERE
    (
        s.FirstName = 'Dave'
        AND s.LastName = 'Dodger'
        AND s.JobTitle = 'Chief Work Dodger'
    );
 
    INSERT INTO dbo.SalaryPayment
    (
        StaffId
        , PaymentDate
        , Amount
    )
    VALUES
    (
        @StaffId
        , @DecPayDate
        , 2122.90
    )
    ,
    (
     
        @StaffId
        , @JanPayDate
        , 2105.20
    )
    ,
    (
     
        @StaffId
        , @FebPayDate
        , 2105.20
    );
 
    -- Dave Dodger | Hard Worker
    SELECT
    @StaffId = 
    s.Id
    FROM dbo.Staff s
    WHERE
    (
        s.FirstName = 'Dave'
        AND s.LastName = 'Dodger'
        AND s.JobTitle = 'Hard Worker'
    );
 
    INSERT INTO dbo.SalaryPayment
    (
        StaffId
        , PaymentDate
        , Amount
    )
    VALUES
    (
        @StaffId
        , @DecPayDate
        , 2115.50
    )
    ,
    (
     
        @StaffId
        , @JanPayDate
        , 2100.50
    )
    ,
    (
     
        @StaffId
        , @FebPayDate
        , 2100.50
    );
 
    -- Bob Boots | Handle Cranker
    SELECT
    @StaffId = 
    s.Id
    FROM dbo.Staff s
    WHERE
    (
        s.FirstName = 'Bob'
        AND s.LastName = 'Boots'
        AND s.JobTitle = 'Handle Cranker'
    );
 
    INSERT INTO dbo.SalaryPayment
    (
        StaffId
        , PaymentDate
        , Amount
    )
    VALUES
    (
        @StaffId
        , @DecPayDate
        , 2100.00
    )
    ,
    (
     
        @StaffId
        , @JanPayDate
        , 2039.50
    )
    ,
    (
     
        @StaffId
        , @FebPayDate
        , 2039.50
    );
 
    -- Janet Timms | Handle Cranker
    SELECT
    @StaffId = 
    s.Id
    FROM dbo.Staff s
    WHERE
    (
        s.FirstName = 'Janet'
        AND s.LastName = 'Timms'
        AND s.JobTitle = 'Handle Cranker'
    );
 
    INSERT INTO dbo.SalaryPayment
    (
        StaffId
        , PaymentDate
        , Amount
    )
    VALUES
    (
        @StaffId
        , @DecPayDate
        , 2100.00
    )
    ,
    (
     
        @StaffId
        , @JanPayDate
        , 2039.50
    )
    ,
    (
     
        @StaffId
        , @FebPayDate
        , 2039.50
    );
     
END;
 
-- HolidayRequest table test data
IF NOT EXISTS
(
    SELECT *
    FROM dbo.HolidayRequest hr
)
BEGIN
 
    -- For ease, everyone has three holiday requests
     
    -- Steve Stevenson | Head Honcho
    SELECT
    @StaffId = 
    s.Id
    FROM dbo.Staff s
    WHERE
    (
        s.FirstName = 'Steve'
        AND s.LastName = 'Stevenson'
        AND s.JobTitle = 'Head Honcho'
    );
 
    INSERT INTO dbo.HolidayRequest
    (
        StaffId
        , DateFrom
        , DateTo
    )
    VALUES
    (
        @StaffId
        , '2017-01-30'
        , '2017-02-03'
    )
    ,
    (
        @StaffId
        , '2017-05-22'
        , '2017-05-26'
    )
    ,
    (
        @StaffId
        , '2017-07-24'
        , '2017-07-28'
    );
 
    -- Marie Pritchard | Team Manager
    SELECT
    @StaffId = 
    s.Id
    FROM dbo.Staff s
    WHERE
    (
        s.FirstName = 'Marie'
        AND s.LastName = 'Pritchard'
        AND s.JobTitle = 'Team Manager'
    );
 
    INSERT INTO dbo.HolidayRequest
    (
        StaffId
        , DateFrom
        , DateTo
    )
    VALUES
    (
        @StaffId
        , '2017-03-23'
        , '2017-03-24'
    )
    ,
    (
        @StaffId
        , '2017-06-12'
        , '2017-06-16'
    )
    ,
    (
        @StaffId
        , '2017-08-14'
        , '2017-08-14'
    );
 
    -- Judy Dench | Team Manager
    SELECT
    @StaffId = 
    s.Id
    FROM dbo.Staff s
    WHERE
    (
        s.FirstName = 'Judy'
        AND s.LastName = 'Dench'
        AND s.JobTitle = 'Team Manager'
    );
 
    INSERT INTO dbo.HolidayRequest
    (
        StaffId
        , DateFrom
        , DateTo
    )
    VALUES
    (
        @StaffId
        , '2017-03-24'
        , '2017-03-25'
    )
    ,
    (
        @StaffId
        , '2017-06-12'
        , '2017-06-15'
    )
    ,
    (
        @StaffId
        , '2017-08-15'
        , '2017-08-16'
    );
 
    -- Dave Dodger | Chief Work Dodger
    SELECT
    @StaffId = 
    s.Id
    FROM dbo.Staff s
    WHERE
    (
        s.FirstName = 'Dave'
        AND s.LastName = 'Dodger'
        AND s.JobTitle = 'Chief Work Dodger'
    );
 
    INSERT INTO dbo.HolidayRequest
    (
        StaffId
        , DateFrom
        , DateTo
    )
    VALUES
    (
        @StaffId
        , '2017-03-20'
        , '2017-03-24'
    )
    ,
    (
        @StaffId
        , '2017-03-27'
        , '2017-03-31'
    )
    ,
    (
        @StaffId
        , '2017-04-03'
        , '2017-04-07'
    );
 
    -- Dave Dodger | Hard Worker
    SELECT
    @StaffId = 
    s.Id
    FROM dbo.Staff s
    WHERE
    (
        s.FirstName = 'Dave'
        AND s.LastName = 'Dodger'
        AND s.JobTitle = 'Hard Worker'
    );
 
    INSERT INTO dbo.HolidayRequest
    (
        StaffId
        , DateFrom
        , DateTo
    )
    VALUES
    (
        @StaffId
        , '2017-03-31'
        , '2017-03-31'
    )
    ,
    (
        @StaffId
        , '2017-07-31'
        , '2017-07-31'
    )
    ,
    (
        @StaffId
        , '2017-09-25'
        , '2017-09-25'
    );
 
    -- Bob Boots | Handle Cranker
    SELECT
    @StaffId = 
    s.Id
    FROM dbo.Staff s
    WHERE
    (
        s.FirstName = 'Bob'
        AND s.LastName = 'Boots'
        AND s.JobTitle = 'Handle Cranker'
    );
 
    INSERT INTO dbo.HolidayRequest
    (
        StaffId
        , DateFrom
        , DateTo
    )
    VALUES
    (
        @StaffId
        , '2017-05-15'
        , '2017-05-19'
    )
    ,
    (
        @StaffId
        , '2017-07-10'
        , '2017-07-12'
    )
    ,
    (
        @StaffId
        , '2017-10-11'
        , '2017-10-13'
    );
 
    -- Janet Timms | Handle Cranker
    SELECT
    @StaffId = 
    s.Id
    FROM dbo.Staff s
    WHERE
    (
        s.FirstName = 'Janet'
        AND s.LastName = 'Timms'
        AND s.JobTitle = 'Handle Cranker'
    );
 
    INSERT INTO dbo.HolidayRequest
    (
        StaffId
        , DateFrom
        , DateTo
    )
    VALUES
    (
        @StaffId
        , '2017-04-10'
        , '2017-04-11'
    )
    ,
    (
        @StaffId
        , '2017-06-21'
        , '2017-06-23'
    )
    ,
    (
        @StaffId
        , '2017-11-13'
        , '2017-11-17'
    );
 
END;
GO
 
-- Create utility stored procedures, just because :op
 
-- Basic example using the HolidayRequest table
IF (OBJECT_ID('dbo.GetHolidayRequestDaysSumInfo', 'P') IS NOT NULL)
BEGIN
 
    DROP PROCEDURE dbo.GetHolidayRequestDaysSumInfo;
 
END;
GO
 
CREATE PROCEDURE dbo.GetHolidayRequestDaysSumInfo
AS
BEGIN
 
    -- Holiday request days (summed), grouped by staff name (two daves are cobbled together here)
    SELECT
    s.FirstName + ' ' + s.LastName AS [StaffName]
    , SUM(hr.NumberOfDaysRequested) AS [TotalDaysRequested]
    FROM dbo.Staff s
        INNER JOIN dbo.HolidayRequest hr ON s.Id = hr.StaffId
    GROUP BY s.FirstName + ' ' + s.LastName;
 
    -- Holiday request days (summed), grouped by department
    SELECT
    s.Department
    , SUM(hr.NumberOfDaysRequested) AS [TotalDaysRequested]
    FROM dbo.Staff s
        INNER JOIN dbo.HolidayRequest hr ON s.Id = hr.StaffId
    GROUP BY Department;
 
    -- Holiday request days (summed), grouped by department and job title
    SELECT
    s.Department
    , s.JobTitle
    , SUM(hr.NumberOfDaysRequested) AS [TotalDaysRequested]
    FROM dbo.Staff s
        INNER JOIN dbo.HolidayRequest hr ON s.Id = hr.StaffId
    GROUP BY s.Department, s.JobTitle;
 
END;
GO
 
IF (OBJECT_ID('dbo.GetHolidayRequestDaysSumInfoUsingGroupingSets', 'P') IS NOT NULL)
BEGIN
 
    DROP PROCEDURE dbo.GetHolidayRequestDaysSumInfoUsingGroupingSets;
 
END;
GO
 
CREATE PROCEDURE dbo.GetHolidayRequestDaysSumInfoUsingGroupingSets
AS
BEGIN
 
    -- Holiday request days (summed), group sets included for staff name, department and department and job title
    SELECT
    s.FirstName + ' ' + s.LastName AS [StaffName]
    , s.Department
    , s.JobTitle
    , SUM(hr.NumberOfDaysRequested) AS [TotalDaysRequested]
    FROM dbo.Staff s
        INNER JOIN dbo.HolidayRequest hr ON s.Id = hr.StaffId
    GROUP BY GROUPING SETS
    (
        (s.FirstName + ' ' + s.LastName)
        , (s.Department)
        , (s.Department, s.JobTitle)
    );
 
END;
GO
 
-- A couple more stored prodecures, focusing on the SalaryPayment table this time
IF (OBJECT_ID('dbo.GetSalarySumInfo', 'P') IS NOT NULL)
BEGIN
 
    DROP PROCEDURE dbo.GetSalarySumInfo;
 
END;
GO
 
CREATE PROCEDURE dbo.GetSalarySumInfo
AS
BEGIN
 
    -- Salary sum info grouped by staff name (two daves are, of course, grouped by this)
    SELECT
    s.FirstName + ' ' + s.LastName AS [StaffName]
    , SUM(sp.Amount) AS [TotalPay]
    FROM dbo.Staff s
        INNER JOIN dbo.SalaryPayment sp ON s.Id = sp.StaffId
    GROUP BY s.FirstName + ' ' + s.LastName;
 
    -- Salary sum info grouped by job title
    SELECT
    s.JobTitle
    , SUM(sp.Amount) AS [TotalPay]
    FROM dbo.Staff s
        INNER JOIN dbo.SalaryPayment sp ON s.Id = sp.StaffId
    GROUP BY s.JobTitle;
 
    -- Salary sum info grouped by department
    SELECT
    s.Department
    , SUM(sp.Amount) AS [TotalPay]
    FROM dbo.Staff s
        INNER JOIN dbo.SalaryPayment sp ON s.Id = sp.StaffId
    GROUP BY s.Department
 
    -- Salary sum info grouped by perk name
    SELECT
    p.PerkName
    , SUM(sp.Amount) AS [TotalPay]
    FROM dbo.Staff s
        INNER JOIN dbo.Perk p ON s.Perk = p.Id
        INNER JOIN dbo.SalaryPayment sp ON s.Id = sp.StaffId
    GROUP BY p.PerkName;
 
    -- Salary sum info grouped by department and job title
    SELECT
    s.Department
    , s.JobTitle
    , SUM(sp.Amount) AS [TotalPay]
    FROM dbo.Staff s
        INNER JOIN dbo.SalaryPayment sp ON s.Id = sp.StaffId
    GROUP BY s.Department, s.JobTitle;
 
END;
GO
 
IF (OBJECT_ID('dbo.GetSalarySumInfoUsingGroupingSets', 'P') IS NOT NULL)
BEGIN
 
    DROP PROCEDURE dbo.GetSalarySumInfoUsingGroupingSets;
 
END;
GO
 
CREATE PROCEDURE dbo.GetSalarySumInfoUsingGroupingSets
AS
BEGIN
 
    -- Salary sum info grouped by staff name, department, job title, perk name and department/job title, in a single result set
    SELECT
    s.FirstName + ' ' + s.LastName AS [StaffName]
    , s.Department
    , s.JobTitle
    , p.PerkName
    , SUM(sp.Amount) AS [TotalPay]
    FROM dbo.Staff s
        INNER JOIN dbo.Perk p ON s.Perk = p.Id
        INNER JOIN dbo.SalaryPayment sp ON s.Id = sp.StaffId
    GROUP BY GROUPING SETS
    (
        (s.FirstName + ' ' + s.LastName)
        , (s.JobTitle)
        , (s.Department)
        , (p.PerkName)
        , (s.Department, s.JobTitle)
    );
 
END;
GO
 
-- Execute stored procedures to inspect the results
 
-- Holiday aggregation results (grouping sets stored procedure variant returns all data in a single result set)
EXEC dbo.GetHolidayRequestDaysSumInfo;
EXEC dbo.GetHolidayRequestDaysSumInfoUsingGroupingSets;
 
-- Salary aggregation results (grouping sets stored procedure variant returns all data in a single result set)
EXEC dbo.GetSalarySumInfo;
EXEC dbo.GetSalarySumInfoUsingGroupingSets;