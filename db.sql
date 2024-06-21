SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[UpdateAccountNo]
    @refNo VARCHAR(255),
    @OldAccountNo VARCHAR(255),
    @NewAccountNo VARCHAR(255),
    @ifscCode VARCHAR(255),
    @Reason VARCHAR(MAX),
    @divisionCode VARCHAR(255)
AS
BEGIN
    UPDATE jkSWdeliveredMay30 SET accountNo = @NewAccountNo, remarks2=@Reason WHERE refNo= @refNo AND (COALESCE(accountNoCorrectionByDirectorate, accountNo) = @OldAccountNo) AND (COALESCE(ifscCorrectionByDirectorate, ifscCode) = @ifscCode) AND (@divisionCode = 0 OR divisionCode = @divisionCode)
END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[UpdateIfscCode]
    @refNo VARCHAR(255),
    @AccountNo VARCHAR(255),
    @OldIfsc VARCHAR(255),
    @NewIfsc VARCHAR(255),
    @Reason VARCHAR(MAX),
    @divisionCode VARCHAR(255)
AS
BEGIN
    UPDATE jkSWdeliveredMay30 SET ifscCode = @NewIfsc,remarks2 = @Reason WHERE refNo= @refNo AND (COALESCE(accountNoCorrectionByDirectorate, accountNo) = @AccountNo) AND (COALESCE(ifscCorrectionByDirectorate, ifscCode) = @OldIfsc) AND (@divisionCode = 0 OR divisionCode = @divisionCode)
END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[UpdateEligibility]
    @refNo VARCHAR(255),
    @accountNo VARCHAR(50),
    @ifscCode VARCHAR(50),
    @NewEligibleForPension VARCHAR(20),
    @Reason NVARCHAR(MAX),
    @divisionCode INT
AS
BEGIN
    -- Update statement
    UPDATE jkSWdeliveredMay30
    SET eligibleForPension = @NewEligibleForPension , remarks2 = @Reason
    WHERE refNo = @refNo AND (@divisionCode = 0 OR divisionCode = @divisionCode)
      AND (COALESCE(accountNoCorrectionByDirectorate, accountNo) = @accountNo)
      AND (COALESCE(ifscCorrectionByDirectorate, ifscCode) = @ifscCode)
END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[UpdateApplicantName]
    @refNo VARCHAR(255),
    @AccountNo VARCHAR(255),
    @IfscCode VARCHAR(255),
    @OldName VARCHAR(255),
    @NewName VARCHAR(255),
    @Reason VARCHAR(MAX),
    @divisionCode VARCHAR(255)
AS
BEGIN
    UPDATE jkSWdeliveredMay30 SET name = @NewName,remarks2 = @Reason WHERE refNo= @refNo AND (COALESCE(accountNoCorrectionByDirectorate, accountNo) = @AccountNo) AND (COALESCE(ifscCorrectionByDirectorate, ifscCode) = @IfscCode) AND (@divisionCode = 0 OR divisionCode = @divisionCode)
END
GO
