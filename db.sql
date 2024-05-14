CREATE PROCEDURE [dbo].[UpdateWithDepartmentFile]
    @refNo VARCHAR(100),
    @accountNo VARCHAR(20),
    @ifscCode VARCHAR(15)
AS
BEGIN
    UPDATE jkSWdeliveredMay30 SET eligibleForPension = 'YES' WHERE 
    refNo = @refNo 
    AND accountNo = @accountNo
    AND ifscCode = @ifscCode
    AND nsapChk IS NULL
    AND duplicateBankAccountNo IS NULL
    AND GOI_NSAP IS NULL
    AND remarks2 IS NULL
    AND remarks3 IS NULL
    AND eligibleForPension='NO' 
    AND sharedWithDeptForVerification = 'YES';
END

CREATE PROCEDURE [dbo].[GetListforBankPaymentFile]
    @districtLGDCode INT,
    @month VARCHAR(10),
    @year VARCHAR(10),
    @payingAccountNo VARCHAR(50),
    @payingIfscCode VARCHAR(20),
    @payingBankName VARCHAR(50)
AS
BEGIN
    SELECT 
        (refNo) as Column1,
        ([districtNameForBank]+@month+@year+[districtUidForBank]) as Column2,
        'Social Welfare Department' as column3,
        @payingAccountNo as column4,
        CASE WHEN refNo='JK-SW-PNS/2023/641967' THEN '2000' ELSE '1000' END as column5,
        FORMAT (getdate(), 'ddd MMMM dd HH:mm:ss IST yyyy') as column6,
        @payingBankName as column7,
        @payingIfscCode  as column8,
        iif(nameCorrectionByDirectorate is NULL,name,nameCorrectionByDirectorate) as column9,
        iif(ifscCorrectionByDirectorate is NULL,ifscCode,ifscCorrectionByDirectorate) as column10,
        iif(accountNoCorrectionByDirectorate is NULL,accountNo,accountNoCorrectionByDirectorate) as column11,
        pensionTypeShort as column12,
        CASE 
            WHEN [paymentOfYearFeb2023BankRes] IS NULL AND
                 [paymentOfMonthFeb2023BankRes] IS NULL AND
                 [paymentOfYearMarch2023BankRes] IS NULL AND
                 [paymentOfMonthMarch2023BankRes] IS NULL AND
                 [paymentOfYearApril2023BankRes] IS NULL AND
                 [paymentOfMonthApril2023BankRes] IS NULL AND
                 [paymentOfYearMay2023BankRes] IS NULL AND
                 [paymentOfMonthMay2023BankRes] IS NULL AND
                 [paymentOfYearJune2023BankRes] IS NULL AND
                 [paymentOfMonthJune2023BankRes] IS NULL AND
                 [paymentOfYearJuly2023BankRes] IS NULL AND
                 [paymentOfMonthJuly2023BankRes] IS NULL AND
                 [paymentOfYearAug2023BankRes] IS NULL AND
                 [paymentOfMonthAug2023BankRes] IS NULL AND
                 [paymentOfYearSep2023BankRes] IS NULL AND
                 [paymentOfMonthSep2023BankRes] IS NULL AND
                 [paymentOfYearOct2023BankRes] IS NULL AND
                 [paymentOfMonthOct2023BankRes] IS NULL AND
                 [paymentOfYearNov2023BankRes] IS NULL AND
                 [paymentOfMonthNov2023BankRes] IS NULL AND
                 [paymentOfYearDec2023BankRes] IS NULL AND
                 [paymentOfMonthDec2023BankRes] IS NULL AND
                 [paymentOfYearJan2024BankRes] IS NULL AND
                 [paymentOfMonthJan2024BankRes] IS NULL AND
                 [paymentOfYearFeb2024BankRes] IS NULL AND
                 [paymentOfMonthFeb2024BankRes] IS NULL THEN 'FALSE'
            ELSE 'TRUE'
        END as column13
    FROM 
        jkSWdeliveredMay30
    WHERE 
        districtLGDCode = @districtLGDCode
        AND eligibleForPension = 'YES'
        AND remarks2 IS NULL
        AND JK_ISSS = 'YES'
        AND duplicateBankAccountNo IS NULL
        AND (
            (yearActionOnDate = '2022' AND monthActionOnDate IN ('October', 'November', 'December')) OR
            (yearActionOnDate = '2023' AND monthActionOnDate IN ('January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December')) OR
            (yearActionOnDate = '2024' AND monthActionOnDate IN ('January', 'February', 'March','April','May'))
        )
        AND nsapchk IS NULL 
        AND GOI_NSAP IS NULL
    ORDER BY 
        yearActionOnDate, 
        monthActionOnDate;
END


CREATE PROCEDURE [dbo].[SharedWithDepartment]
    @refNo VARCHAR(50)
AS
BEGIN
    UPDATE jkSWdeliveredMay30 SET sharedWithDeptForVerification = 'YES' WHERE refNo = @refNo;
END


CREATE PROCEDURE [dbo].[UpdateEligibilityBankFile]
    @refNo VARCHAR(50),
    @accountNo VARCHAR(50),
    @ifscCode VARCHAR(50)
AS
BEGIN
    UPDATE jkSWdeliveredMay30 SET eligibleForPension = 'NO' WHERE refNo =@refNo AND accountNo = @accountNo AND ifscCode = @ifscCode; 
END