-- DROP SCHEMA dbo;

CREATE SCHEMA dbo;
-- Pension.dbo.BankFiles definition

-- Drop table

-- DROP TABLE Pension.dbo.BankFiles;

CREATE TABLE BankFiles (
	Column1 nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	Column2 nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	Column3 nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	Column4 nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	Column5 nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	Column6 nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	Column7 nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	Column8 nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	Column9 nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	Column10 nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	Column11 nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	Column12 nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	Column13 nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
);


-- Pension.dbo.DepartmentFileModels definition

-- Drop table

-- DROP TABLE Pension.dbo.DepartmentFileModels;

CREATE TABLE DepartmentFileModels (
	RefNo nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	AppliedDistrict nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	AppliedTehsil nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	Name nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	Parentage nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	DOB nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	Gender nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	MobileNo nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	Email nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	PensionType nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	TypeOfDisabilityAsPerUDID nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	PresentAddress nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	PresentDistrict nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	PresentTehsil nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	Present_GP_Muncipality nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	PresentVillage nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	BankName nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	BranchName nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	IfscCode nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	AccountNo nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	PreviousPension nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	PreviousPensionBank nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	PreviousPensionBankBranch nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	PreviousPensionBankIFSCcode nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	PreviousPensionAccountNo nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	TswoPreviousPensionYesNo nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	TswoPreviousPensionScheme nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	ActionOnDate nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	YearActionOnDate nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	MonthActionOnDate nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CurrentStatus nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	SanctionedByTask nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	JK_ISSS nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	JanSugamDownloadCycle nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
);


-- Pension.dbo.SearchCount definition

-- Drop table

-- DROP TABLE Pension.dbo.SearchCount;

CREATE TABLE SearchCount (
	CountId int NOT NULL,
	Username varchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS DEFAULT '0' NOT NULL,
	Count int NOT NULL
);


-- Pension.dbo.UpdateHistory definition

-- Drop table

-- DROP TABLE Pension.dbo.UpdateHistory;

CREATE TABLE UpdateHistory (
	UUID int IDENTITY(1,1) NOT NULL,
	RefNo nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	UpdationDetails nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	CONSTRAINT PK_UpdateHistory PRIMARY KEY (UUID)
);


-- Pension.dbo.Users definition

-- Drop table

-- DROP TABLE Pension.dbo.Users;

CREATE TABLE Users (
	UUID int IDENTITY(1,1) NOT NULL,
	Username nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	Email nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	UserType nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	divisionCode int NULL,
	Password nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	CONSTRAINT PK_Users PRIMARY KEY (UUID)
);


-- Pension.dbo.[__EFMigrationsHistory] definition

-- Drop table

-- DROP TABLE Pension.dbo.[__EFMigrationsHistory];

CREATE TABLE [__EFMigrationsHistory] (
	MigrationId nvarchar(150) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	ProductVersion nvarchar(32) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	CONSTRAINT PK___EFMigrationsHistory PRIMARY KEY (MigrationId)
);


-- Pension.dbo.casesStopped definition

-- Drop table

-- DROP TABLE Pension.dbo.casesStopped;

CREATE TABLE casesStopped (
	uid int NULL,
	refNo nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	districtLGDcode nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	districtName nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	name nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	parentage nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	infRecvdINnicOn nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	reasonForStoppingPension nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	letterFrom nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	letterNo nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	letterDate nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	reason1 nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	reason2 nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	reason3 nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
);


-- Pension.dbo.jkSWdeliveredMay30 definition

-- Drop table

-- DROP TABLE Pension.dbo.jkSWdeliveredMay30;

CREATE TABLE jkSWdeliveredMay30 (
	uid int NULL,
	sno float NULL,
	districtUidForBank varchar(6) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	districtNameForBank varchar(3) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	refNo nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	lgdStateCode int NULL,
	divisionCode int NULL,
	divisionName nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	districtLGDCode int NULL,
	appliedDistrict nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	TSWOtehsilCode int NULL,
	appliedTehsil nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	name nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	nameCorrectionByDirectorate nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	nameCorrectionDirLetter nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	nameCorrectionDate datetime NULL,
	parentage nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	dob nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	gender nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	mobileNo nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	email nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	pensionType nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	pensionTypeShort varchar(3) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	typeOfDisabilityAsPerUDID nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	presentAddress nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	presentDistrict nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	presentTehsil nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	present_GP_Muncipality nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	presentVillage nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	bankName nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	bankNameCorrectionByDirectorate nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	bankNameCorrectionDirLetter nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	bankNameCorrectionDate nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	branchName nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	branchNameCorrectionByDirectorate nchar(10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	ifscCode nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	ifscCorrectionByDirectorate nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	ifscCorrectionDirLetter nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	ifscCorrectionDate datetime NULL,
	bankFileGeneratedBeforeDirCorrection nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	accountNo nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	accountNoCorrectionByDirectorate nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	accountNoCorrectionDirLetter nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	accountNoCorrectionDate datetime NULL,
	previousPension nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	previousPensionBank nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	previousPensionBankBranch nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	previousPensionBankIFSCcode nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	previousPensionAccountNo nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	tswoPreviousPensionYesNo nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	tswoPreviousPensionScheme nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	actionOnDate nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	yearActionOnDate nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	monthActionOnDate nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	actionOnDate_verified1 nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	yearActionOnDate_verified1 nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	monthActionOnDate_verified1 nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	reSanctionDate nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	currentStatus nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	sanctionedByTask nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	schemeSanctionedByDirKashmir nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	schemeSanctionedByDirJammu nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	schemeSanctionedByDSWO nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	JK_ISSS nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	GOI_NSAP nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	SCHEME_CLARIFICATION nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	leftOutCasesDueToSchemeClarification varchar(3) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	duplicateBankAccountNo nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	sharedWithDeptForVerification nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	eligibleForPension nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	deptVerified nchar(10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	deptVerifiedDate datetime NULL,
	aprilChk nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	aprilChk2 nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	aprilChk3 nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	chkIfscCodeUpdated nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	chkAccountNoUpdated nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	Reason nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	Remarks nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	remarks2 nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	remarks3 nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	remarksForCorrectionInNameIFSCaccountNo nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	nsapChk nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	janSugamDownloadCycle nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	oldCDACbenificary nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	arrear_total_months int NULL,
	arrear_total_months_amt int NULL,
	arrear_bank_file_generated varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	arrear_bank_file_generated_date varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	arear_bank_file_bankPayment_ok varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	arear_bank_file_bankPayment_ok_date varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	arrear_March23_Minus_Feb23 int NULL,
	arrear_April23_Minus_March23 int NULL,
	arrear_May23_Minus_April23 int NULL,
	arrear_June23_Minus_May23 int NULL,
	arrear_July23_Minus_June23 int NULL,
	arrear_Aug23_Minus_July23 int NULL,
	arrear_Sep23_Minus_Aug23 int NULL,
	arrear_Oct23_Minus_Sep23 int NULL,
	arrear_Nov23_Minus_Oct23 int NULL,
	arrear_Dec23_Minus_Nov23 int NULL,
	arrear_Jan24_Minus_Dec23 int NULL,
	arrear_Feb24_Minus_Jan24 int NULL,
	arrear_Mar24_Minus_Feb24 int NULL,
	paymentOfYearFeb2023 nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	paymentOfMonthFeb2023 nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	paymentOfYearFeb2023BankRes nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	paymentOfMonthFeb2023BankRes nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	paymentOfYearMarch2023 nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	paymentOfMonthMarch2023 nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	paymentOfYearMarch2023BankRes nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	paymentOfMonthMarch2023BankRes nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	paymentOfYearApril2023 nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	paymentOfMonthApril2023 nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	paymentOfYearApril2023BankRes nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	paymentOfMonthApril2023BankRes nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	paymentOfYearMay2023 nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	paymentOfMonthMay2023 nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	paymentOfYearMay2023BankRes nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	paymentOfMonthMay2023BankRes nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	paymentOfYearJune2023 nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	paymentOfMonthJune2023 nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	paymentOfYearJune2023BankRes nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	paymentOfMonthJune2023BankRes nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	paymentOfYearJuly2023 nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	paymentOfMonthJuly2023 nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	paymentOfYearJuly2023BankRes nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	paymentOfMonthJuly2023BankRes nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	paymentOfYearAug2023 nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	paymentOfMonthAug2023 nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	paymentOfYearAug2023BankRes nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	paymentOfMonthAug2023BankRes nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	paymentOfYearSep2023 nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	paymentOfMonthSep2023 nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	paymentOfYearSep2023BankRes nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	paymentOfMonthSep2023BankRes nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	paymentOfYearOct2023 nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	paymentOfMonthOct2023 nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	paymentOfYearOct2023BankRes nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	paymentOfMonthOct2023BankRes nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	paymentOfYearNov2023 nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	paymentOfMonthNov2023 nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	paymentOfYearNov2023BankRes nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	paymentOfMonthNov2023BankRes nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	paymentOfYearDec2023 nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	paymentOfMonthDec2023 nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	paymentOfYearDec2023BankRes nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	paymentOfMonthDec2023BankRes nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	paymentOfYearJan2024 nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	paymentOfMonthJan2024 nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	paymentOfYearJan2024BankRes nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	paymentOfMonthJan2024BankRes nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	paymentOfYearFeb2024 nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	paymentOfMonthFeb2024 nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	paymentOfYearFeb2024BankRes nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	paymentOfMonthFeb2024BankRes nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	junePaymentMonth2ndBnkFile2023 nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	octPaymentMonth2ndBnkFile2023 nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	octPaymentMonth2ndBnkFile2023_2 nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	dummyCol nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
);


-- Pension.dbo.msDistrict definition

-- Drop table

-- DROP TABLE Pension.dbo.msDistrict;

CREATE TABLE msDistrict (
	divisionCode int NULL,
	districtLgdCode int NULL,
	districtName varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	districtNameForBank varchar(3) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
);


-- Pension.dbo.msDivision_top500 definition

-- Drop table

-- DROP TABLE Pension.dbo.msDivision_top500;

CREATE TABLE msDivision_top500 (
	stateCode int NULL,
	divisionCode nchar(10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	divisionName varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
);


-- Pension.dbo.msTSWOtehsil_top500 definition

-- Drop table

-- DROP TABLE Pension.dbo.msTSWOtehsil_top500;

CREATE TABLE msTSWOtehsil_top500 (
	divisionCode int NULL,
	districtLGDCode int NULL,
	TSWOtehsilCode int NULL,
	TSWOtehsilName varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	tswoOfficeName varchar(200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
);


-- Pension.dbo.nsapData definition

-- Drop table

-- DROP TABLE Pension.dbo.nsapData;

CREATE TABLE nsapData (
	sanction_order_no nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	state_code nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	state_name nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	district_code nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	district_name nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	sub_district_municipal_area_code nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	sub_district_municipal_area_name nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	gram_panchayat_ward_code nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	gram_panchayat_ward_name nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	village_code nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	village_name nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	beneficiary_name nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	parentage_name nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	bankaccount_no nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	ifsc_code nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	scheme_name nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	duplicateAccountNo nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
);
