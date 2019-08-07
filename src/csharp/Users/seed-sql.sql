IF NOT EXISTS
(
	SELECT 1
	FROM dbo.AspNetUsers u
)
BEGIN

	-- Create two roles - 'SuperUser' & 'AdminUser'
	DECLARE
	@SuperUser		UNIQUEIDENTIFIER = NEWID()
	, @AdminUser	UNIQUEIDENTIFIER = NEWID();

	INSERT INTO dbo.AspNetRoles
	(
		Id
		, [Name]
	)
	VALUES
	(
		@SuperUser
		, 'SuperUser'
	),
	(
		@AdminUser
		, 'AdminUser'
	);

	-- Create 'dave' - he will be aprt of the SuperUser & AdminUser Roles
	DECLARE
	@UserId			UNIQUEIDENTIFIER = NEWID();

	INSERT INTO dbo.AspNetUsers
	(
		Id
		, UserName
		, Email
		, EmailConfirmed
		, PhoneNumberConfirmed
		, TwoFactorEnabled
		, LockoutEnabled
		, AccessFailedCount
	)
	VALUES
	(
		@UserId
		, 'dave'
		, 'dave@test.co.uk'
		, 1
		, 1
		, 0
		, 0
		, 0
	);

	INSERT INTO dbo.AspNetUserRoles
	(
		UserId
		, RoleId
	)
	SELECT
	@UserId
	, r.Id
	FROM dbo.AspNetRoles r;

	DECLARE
	@JustAdmin			UNIQUEIDENTIFIER = NEWID();

	INSERT INTO dbo.AspNetUsers
	(
		Id
		, UserName
		, Email
		, EmailConfirmed
		, PhoneNumberConfirmed
		, TwoFactorEnabled
		, LockoutEnabled
		, AccessFailedCount
	)
	VALUES
	(
		@JustAdmin
		, 'Jane'
		, 'jane@test.co.uk'
		, 1
		, 1
		, 0
		, 0
		, 0
	);

	INSERT INTO dbo.AspNetUserRoles
	(
		UserId
		, RoleId
	)
	SELECT
	@JustAdmin
	, r.Id
	FROM dbo.AspNetRoles r
	WHERE r.[Name] = 'AdminUser';

END;

SELECT *
FROM dbo.AspNetUsers u
	INNER JOIN dbo.AspNetUserRoles ur ON u.Id = ur.UserId
	INNER JOIN dbo.AspNetRoles r ON ur.RoleId = r.Id;