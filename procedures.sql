CREATE PROCEDURE [dbo].[GetFilesUploaded]
    @UpdatedBy VARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        ParsedJson.ColumnName AS UpdatedColumn,
        ParsedJson.UpdatedBy,
        ParsedJson.BulkUpdate,
        ParsedJson.FileUsed,
        ParsedJson.UpdatedAt
    FROM 
        UpdateHistory
    CROSS APPLY 
        OPENJSON(UpdationDetails)
        WITH (
            ColumnName NVARCHAR(255) '$.ColumnName',
            UpdatedBy NVARCHAR(255) '$.UpdatedBy',
            BulkUpdate BIT '$.BulkUpdate',
            FileUsed NVARCHAR(MAX) '$.FileUsed',
            UpdatedAt NVARCHAR(50) '$.UpdatedAt'
        ) AS ParsedJson
    WHERE 
        ParsedJson.UpdatedBy = @UpdatedBy
        AND ParsedJson.FileUsed IS NOT NULL
        AND ParsedJson.FileUsed <> '';
END;

CREATE PROCEDURE [dbo].[UpdateAccountNo]
    @RefNo VARCHAR(50),
    @OldAccountNo VARCHAR(16),
    @NewAccountNo VARCHAR(16),
    @IfscCode VARCHAR(11),
    @Reason VARCHAR(255)
AS
BEGIN
    -- Update the account number and log the reason for the change
    UPDATE jkSWdeliveredMay30
    SET 
        accountNo = @NewAccountNo,
        remarksForCorrectionInNameIFSCaccountNo = @Reason
    WHERE 
        refNo = @RefNo 
        AND accountNo = @OldAccountNo 
        AND ifscCode = @IfscCode 
        AND duplicateBankAccountNo IS NULL; -- Only update if the record is NOT a duplicate

    -- Return the number of rows affected
    RETURN @@ROWCOUNT;
END;

CREATE PROCEDURE [dbo].[UpdateApplicantName]
    @Ref VARCHAR(50),
    @AccountNo VARCHAR(16),
    @IfscCode VARCHAR(11),
    @OldName VARCHAR(255),
    @NewName VARCHAR(255),
    @Reason VARCHAR(255)
AS
BEGIN
    UPDATE jkSWdeliveredMay30 SET [name] = @NewName, remarksForCorrectionInNameIFSCaccountNo = @Reason WHERE [name] = @OldName AND refNo = @Ref AND accountNo = @AccountNo AND ifscCode = @IfscCode AND duplicateBankAccountNo IS NULL;
END;

CREATE PROCEDURE [dbo].[UpdateEligibility]
    @RefNo VARCHAR(50),
    @AccountNo VARCHAR(16),
    @IfscCode VARCHAR(11),
    @NewEligibleForPension VARCHAR(3), -- Assuming YES/NO values
    @Reason VARCHAR(255)
AS
BEGIN
    -- Update the eligibility and log the reason for the change
    UPDATE jkSWdeliveredMay30
    SET 
        eligibleForPension = @NewEligibleForPension,
        remarksForCorrectionInNameIFSCaccountNo = @Reason
    WHERE 
        refNo = @RefNo 
        AND accountNo = @AccountNo 
        AND ifscCode = @IfscCode 
        AND duplicateBankAccountNo IS NULL; -- Only update if the record is NOT a duplicate

    -- Return the number of rows affected
    RETURN @@ROWCOUNT;
END;

CREATE PROCEDURE [dbo].[UpdateIfscCode]
    @RefNo VARCHAR(50),
    @AccountNo VARCHAR(16),
    @OldIfsc VARCHAR(11),
    @NewIfsc VARCHAR(11),
    @Reason VARCHAR(255)
AS
BEGIN
    -- Update the IFSC code and log the reason for the change
    UPDATE jkSWdeliveredMay30
    SET 
        ifscCode = @NewIfsc,
        remarksForCorrectionInNameIFSCaccountNo = @Reason
    WHERE 
        refNo = @RefNo 
        AND accountNo = @AccountNo 
        AND ifscCode = @OldIfsc 
        AND duplicateBankAccountNo IS NULL; -- Only update if the record is NOT a duplicate

    -- Return the number of rows affected
    RETURN @@ROWCOUNT;
END;

CREATE PROCEDURE UserLogin
    @Username NVARCHAR(50),
    @Password NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    -- Hash the incoming password for comparison
    DECLARE @HashedPassword NVARCHAR(255);
    SET @HashedPassword = CONVERT(NVARCHAR(255), HASHBYTES('SHA2_256', @Password), 2);

    -- Select the user if the username and password match
    SELECT  *
    FROM Users
    WHERE Username = @Username AND [Password] = @HashedPassword;
END;

CREATE PROCEDURE UserRegistration
    @Username NVARCHAR(50),
    @Email NVARCHAR(100),
    @UserType NVARCHAR(20),
    @DivisionCode INT,
    @Password NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    -- Hash the password (assuming the use of HASHBYTES function for simplicity)
    DECLARE @HashedPassword NVARCHAR(255);
    SET @HashedPassword = CONVERT(NVARCHAR(255), HASHBYTES('SHA2_256', @Password), 2);

    -- Insert the user into the Users table
    INSERT INTO Users (Username, Email, UserType, DivisionCode, [Password])
    VALUES (@Username, @Email, @UserType, @DivisionCode, @HashedPassword);
END;
