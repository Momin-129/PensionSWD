/****** Kashmir Script for SelectTopNRows command from SSMS  ******/
SELECT 
       (refNo) as Column1,
	  ([districtNameForBank]+'MAR'+'2402'+[districtUidForBank]) as Column2, 
	  'Social Welfare Department' as column3,
	  '0367010200000520' as column4,
	  '1000' as column5,
	  FORMAT (getdate(), 'ddd MMMM dd HH:mm:ss IST yyyy') as column6,
	  'THE JAMMU AND KASHMIR BANK LTD.' as column7,
	      'JAKA0TANKEE'  as column8,
		    iif(nameCorrectionByDirectorate is NULL,name,nameCorrectionByDirectorate) as column9,
		  iif(ifscCorrectionByDirectorate is NULL,ifscCode,ifscCorrectionByDirectorate) as column10,
		   iif(accountNoCorrectionByDirectorate is NULL,accountNo,accountNoCorrectionByDirectorate) as column11,
		   pensionTypeShort as column12 
--into [PensionPayment].[dbo].[shopianPaymentOfMarch2024_9244]
	 /* 	  into [PensionPayment].[dbo].[kishtwarForJune2023Payment8275] */
  FROM [jkSWDPension].[dbo].[jkSWdeliveredMay30]
  WHERE (districtLGDCode = 625) AND (eligibleForPension = 'YES') 
 AND (JK_ISSS='YES') and remarks2 is null
AND
(monthActionOnDate = 'October' AND yearActionOnDate = '2022' OR
monthActionOnDate = 'November' AND yearActionOnDate = '2022' OR
monthActionOnDate = 'December' AND yearActionOnDate='2022' OR
monthActionOnDate = 'January' AND yearActionOnDate = '2023' OR
monthActionOnDate = 'February' AND yearActionOnDate = '2023' OR
monthActionOnDate = 'March' AND yearActionOnDate = '2023' OR
monthActionOnDate = 'April'AND yearActionOnDate = '2023' OR
monthActionOnDate = 'May' AND yearActionOnDate = '2023' OR
monthActionOnDate = 'June' AND yearActionOnDate = '2023' OR
monthActionOnDate = 'July' AND yearActionOnDate = '2023' OR
monthActionOnDate = 'August' AND yearActionOnDate = '2023' OR
monthActionOnDate = 'September' AND yearActionOnDate = '2023' OR
monthActionOnDate = 'October' AND yearActionOnDate = '2023' OR
monthActionOnDate = 'November' AND yearActionOnDate = '2023' OR 
monthActionOnDate = 'December' AND yearActionOnDate = '2023' OR
monthActionOnDate = 'January' AND yearActionOnDate = '2024' OR
monthActionOnDate = 'February' AND yearActionOnDate = '2024' OR 
monthActionOnDate = 'March' AND yearActionOnDate = '2024') 
and nsapchk is null and GOI_NSAP is null

ORDER BY yearActionOnDate, monthActionOnDate

/* **************************************/
Update [jkswdPension].[dbo].[jkSWdeliveredMay30]
SET [paymentOfYearFeb2024]='2024', [paymentOfMonthFeb2024]='YES'
FROM [jkSWDPension].[dbo].[jkSWdeliveredMay30]
  WHERE (districtLGDCode =625) AND (eligibleForPension = 'YES') 
 AND (JK_ISSS='YES') 
AND 
(monthActionOnDate = 'October' AND yearActionOnDate = '2022' OR
monthActionOnDate = 'November' AND yearActionOnDate = '2022' OR
monthActionOnDate = 'December' AND yearActionOnDate='2022' OR
monthActionOnDate = 'January' AND yearActionOnDate = '2023' OR
monthActionOnDate = 'February' AND yearActionOnDate = '2023' OR
monthActionOnDate = 'March' AND yearActionOnDate = '2023' OR
monthActionOnDate = 'April'AND yearActionOnDate = '2023' OR
monthActionOnDate = 'May' AND yearActionOnDate = '2023' OR
monthActionOnDate = 'June' AND yearActionOnDate = '2023' OR
monthActionOnDate = 'July' AND yearActionOnDate = '2023' OR
monthActionOnDate = 'August' AND yearActionOnDate = '2023' OR
monthActionOnDate = 'September' AND yearActionOnDate = '2023' OR
monthActionOnDate = 'October' AND yearActionOnDate = '2023' OR
monthActionOnDate = 'November' AND yearActionOnDate = '2023' OR 
monthActionOnDate = 'December' AND yearActionOnDate = '2023' OR
monthActionOnDate = 'January' AND yearActionOnDate = '2024' OR
monthActionOnDate = 'February' AND yearActionOnDate = '2024' OR 
monthActionOnDate = 'March' AND yearActionOnDate = '2024')
and nsapchk is null and GOI_NSAP is null

/* ********************************check Duplicate Ref Number 1******   */
SELECT [column1], COUNT(*) AS duplicateRefNo
FROM   [PensionPayment].[dbo].[shopianPaymentOfMarch2024_9244]
GROUP BY [column1]
HAVING (COUNT(*) > 1) 

/* ********************************check Duplicate Account Number 2******   */
SELECT [column11], COUNT(*) AS BankDuplicate
FROM   [PensionPayment].[dbo].[shopianPaymentOfMarch2024_9244]
GROUP BY [column11]
HAVING (COUNT(*) > 1) 

/* ********************************check Payment file with NSAP Data  3******   */
SELECT matched_accounts.column11, COUNT(*) AS NSAP
FROM
    (
    SELECT *
    FROM [PensionPayment].[dbo].[shopianPaymentOfMarch2024_9244] AS isss, 
         [nsapMord].[dbo].[nsapData] AS nsap
    WHERE isss.column11 = nsap.bankaccount_no 
    ) AS matched_accounts
GROUP BY matched_accounts.column11
HAVING (COUNT(*) > 0)

/* ********************************stop payment like death cases ******   */
SELECT matched_accounts.column1, COUNT(*) AS NSAP
FROM
    (
    SELECT *
    FROM [PensionPayment].[dbo].[shopianPaymentOfMarch2024_9244] AS isss, 
         [jkSWDPension].[dbo].[casesStopped] AS nsap
    WHERE isss.column1 = nsap.refNo
    ) AS matched_accounts
GROUP BY matched_accounts.column1
HAVING (COUNT(*) > 0)  
*/