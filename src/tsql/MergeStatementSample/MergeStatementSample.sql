/* 
    Very basic MERGE example
    -----------------------------
 
    Imagine these tables exist across databases, etc. and require synchronisation (or, as per my recent usage, I built up a derived table using CTE's to provide a source data table to compare against the target table to drive modifications)
    NOTE: Tables created for illustration only; not representative of how to structure this in practice of course
*/
 
-- TARGET TABLE
IF OBJECT_ID('dbo.TARGET_PERSON_TABLE') IS NULL
BEGIN
 
    CREATE TABLE dbo.TARGET_PERSON_TABLE
    (
        TARGET_PERSON_ID                INT     PRIMARY KEY
        , TARGET_FORENAME               NVARCHAR(255)
        , TARGET_SURNAME                NVARCHAR(255)
        , TARGET_AGE                    INT         
    );
 
END;
 
-- SOURCE TABLE
IF OBJECT_ID('dbo.SOURCE_PERSON_TABLE') IS NULL
BEGIN
 
    CREATE TABLE dbo.SOURCE_PERSON_TABLE
    (
        SOURCE_PERSON_ID                INT     PRIMARY KEY
        , SOURCE_FORENAME               NVARCHAR(255)
        , SOURCE_SURNAME                NVARCHAR(255)
        , SOURCE_AGE                    INT         
    );
 
END;
 
--Arrange some test data into each table (target and source, for illustration)
INSERT INTO dbo.TARGET_PERSON_TABLE
(
    TARGET_PERSON_ID
    , TARGET_FORENAME
    , TARGET_SURNAME
    , TARGET_AGE
)
VALUES
(
    1
    , 'Dave'
    , 'Jones'
    , 32
)
,
(
    2
    , 'Moira'
    , 'Stevens'
    , 27
)
,
(
    3
    , 'Larry'
    , 'Bodsworth'
    , 48
);
 
INSERT INTO dbo.SOURCE_PERSON_TABLE
(
    SOURCE_PERSON_ID
    , SOURCE_FORENAME
    , SOURCE_SURNAME
    , SOURCE_AGE
)
VALUES
(
    2
    , 'Mandy'
    , 'Stevens'
    , 32
)
,
(
    3
    , 'Larry'
    , 'Rodsworth'
    , 50
)
,
(
    4
    , 'Sandy'
    , 'Ennis'
    , 29
)
,
(
    5
    , 'Wendy'
    , 'Wainwright'
    , 40
);
 
-- Inspect the target/source table data prior to the MERGE operation
SELECT 
tpt.TARGET_PERSON_ID
, tpt.TARGET_FORENAME
, tpt.TARGET_SURNAME
, tpt.TARGET_AGE
FROM dbo.TARGET_PERSON_TABLE tpt;
 
SELECT
spt.SOURCE_PERSON_ID
, spt.SOURCE_FORENAME
, spt.SOURCE_SURNAME
, spt.SOURCE_AGE
FROM dbo.SOURCE_PERSON_TABLE spt;
 
-- Synchronise the target table with the source table, performing matching and INSERT, UPDATE and DELETE operations as required
MERGE dbo.TARGET_PERSON_TABLE tpt
USING SOURCE_PERSON_TABLE spt ON tpt.TARGET_PERSON_ID = spt.SOURCE_PERSON_ID 
    -- If a row is 'matched' (based on the above 'ON' statement) then simply update the target Person
    WHEN MATCHED 
    THEN UPDATE 
        SET 
        tpt.TARGET_FORENAME = spt.SOURCE_FORENAME
        , tpt.TARGET_SURNAME = spt.SOURCE_SURNAME
        , tpt.TARGET_AGE = spt.SOURCE_AGE
    -- If a row is 'not matched' (based on the above 'ON' statement) then do an insert of a new Person into the target table, based on the source table
    WHEN NOT MATCHED
        THEN INSERT
        (
            TARGET_PERSON_ID
            , TARGET_FORENAME
            , TARGET_SURNAME
            , TARGET_AGE
        )
        VALUES
        (
            SOURCE_PERSON_ID
            , SOURCE_FORENAME
            , SOURCE_SURNAME
            , SOURCE_AGE
        )
    -- Lastly, if a the target table contains a row not matched by the source table then remove the target table row entirely
    WHEN NOT MATCHED BY SOURCE 
        THEN DELETE;
 
/*
    Inspect the target/source table data post MERGE operation (values should be synchronised between correctly between the two tables)
 
    Expected Results
    ----------------------
    1) Person ID 1, Dave Jones, should be removed from the target table (DELETE)
    2) Person ID 2, Forename of 'Moira' should be updated to 'Mandy' and Age should be updated from 27 to 32 (UPDATE)
    3) Person ID 3, Surname of 'Bodsworth' should be updated to 'Rodsworth' and Age should be updated from 48 to 50 (UPDATE)
    4) Person ID 4, Sandy Ennis, should be added to the target table (INSERT)
    5) Person ID 5, Wendy Wainwright, should be added to the target table (INSERT)
 
*/
 
SELECT
tpt.TARGET_PERSON_ID
, tpt.TARGET_FORENAME
, tpt.TARGET_SURNAME
, tpt.TARGET_AGE
FROM dbo.TARGET_PERSON_TABLE tpt;
 
SELECT
spt.SOURCE_PERSON_ID
, spt.SOURCE_FORENAME
, spt.SOURCE_SURNAME
, spt.SOURCE_AGE
FROM dbo.SOURCE_PERSON_TABLE spt;
 
-- Clear down the tables post operation, for ease of re-running and re-testing
TRUNCATE TABLE dbo.TARGET_PERSON_TABLE;
TRUNCATE TABLE dbo.SOURCE_PERSON_TABLE;