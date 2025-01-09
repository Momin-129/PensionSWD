using System.Text.RegularExpressions;
using PensionTemporary.Models.Entities;

public class ValidationFunctions
{
    private readonly PensionContext dbContext;
    private readonly ILogger<ValidationFunctions> _logger;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public ValidationFunctions(PensionContext dbContext, ILogger<ValidationFunctions> logger, IWebHostEnvironment webHostEnvironment)
    {
        this.dbContext = dbContext;
        _logger = logger;
        _webHostEnvironment = webHostEnvironment;
    }

    // Utility functions
    private bool IsDigitOnly(string value) => !string.IsNullOrEmpty(value) && value.All(char.IsDigit);

    private bool IsValidRegex(string value, string pattern) => Regex.IsMatch(value, pattern);

    private void ValidateLength(string fieldName, string value, int expectedLength, List<string> errors)
    {
        if (value.Length != expectedLength)
        {
            errors.Add($"{fieldName} should be {expectedLength} characters long.");
        }
    }

    private void ValidateDigitsOnly(string fieldName, string value, List<string> errors)
    {
        if (!IsDigitOnly(value))
        {
            errors.Add($"{fieldName} should contain only digits.");
        }
    }

    private void ValidateRegex(string fieldName, string value, string pattern, string errorMessage, List<string> errors)
    {
        if (!IsValidRegex(value, pattern))
        {
            errors.Add($"{fieldName} {errorMessage}");
        }
    }

    // Centralized validation logic for common fields
    private void ValidateCommonFields(string accountNo, string ifscCode, string reason, List<string> errors)
    {
        ValidateLength("Account Number", accountNo, 16, errors);
        ValidateLength("IFSC Code", ifscCode, 11, errors);
        ValidateRegex("Reason", reason, @"^[a-zA-Z0-9\s]*$", "should only contain alphabets, digits, and spaces.", errors);
    }

    // Specific validation methods
    public List<string> ValidateNewCycleExcelModel(NewCycleExcelModel model)
    {
        var errors = new List<string>();

        ValidateLength("Account Number", model.AccountNo!, 16, errors);
        ValidateLength("IFSC Code", model.IFSCCode!, 11, errors);

        // Validate fields containing only digits
        ValidateDigitsOnly("District Uid For Bank", model.DistrictUidForBank!, errors);
        ValidateDigitsOnly("LGD State Code", model.LgdStateCode!, errors);
        ValidateDigitsOnly("Division Code", model.DivisionCode!, errors);
        ValidateDigitsOnly("District LGD Code", model.DistrictLGDcode!, errors);
        ValidateDigitsOnly("TSWO Tehsil Code", model.TSWOTehsilCode!, errors);
        ValidateDigitsOnly("Account Number", model.AccountNo!, errors);

        if (model.PreviousPension == "YES")
        {
            ValidateDigitsOnly("Previous Pension Account Number", model.PreviousPensionAccountNo!, errors);
        }

        return errors;
    }

    public List<string> ValidateUpdateEligibility(EligibleExcelModel eligible)
    {
        var errors = new List<string>();

        ValidateCommonFields(eligible.AccountNo, eligible.IfscCode, eligible.Reason, errors);

        if (eligible.OldEligibleForPension == eligible.NewEligibleForPension)
        {
            errors.Add("New Value should not be the same as the old value.");
        }

        return errors;
    }

    public List<string> ValidateWeedoutCase(WeedoutExcelModel eligible)
    {
        var errors = new List<string>();

        ValidateCommonFields(eligible.AccountNo, eligible.IfscCode, eligible.Reason, errors);

        return errors;
    }

    public List<string> ValidateUpdateAccountNo(AccountNoExcelModel account)
    {
        var errors = new List<string>();

        ValidateLength("Old Account Number", account.OldAccountNo, 16, errors);
        ValidateLength("New Account Number", account.NewAccountNo, 16, errors);

        if (account.OldAccountNo == account.NewAccountNo)
        {
            errors.Add("New Account Number can't be the same as the Old Account Number.");
        }

        if (!new[] { "yes", "no" }.Contains(account.EligibleForPension.ToLower()))
        {
            errors.Add("Eligible For Pension should be either YES or NO.");
        }

        ValidateCommonFields(account.NewAccountNo, account.IfscCode, account.Reason, errors);

        return errors;
    }

    public List<string> ValidateUpdateIfscCode(IfscExcelModel ifsc)
    {
        var errors = new List<string>();

        ValidateCommonFields(ifsc.AccountNo, ifsc.NewIfscCode, ifsc.Reason, errors);

        ValidateLength("Old IFSC Code", ifsc.OldIfscCode, 11, errors);
        ValidateLength("New IFSC Code", ifsc.NewIfscCode, 11, errors);

        if (ifsc.OldIfscCode == ifsc.NewIfscCode)
        {
            errors.Add("New IFSC Code can't be the same as the Old IFSC Code.");
        }

        if (!new[] { "yes", "no" }.Contains(ifsc.EligibleForPension.ToLower()))
        {
            errors.Add("Eligible For Pension should be either YES or NO.");
        }

        return errors;
    }

    public List<string> ValidateUpdateApplicantName(NameExcelModel applicant)
    {
        var errors = new List<string>();

        ValidateCommonFields(applicant.AccountNo, applicant.IfscCode, applicant.Reason, errors);

        ValidateRegex("Old Name", applicant.OldName, @"^[a-zA-Z\s]*$", "can only contain alphabets and spaces.", errors);
        ValidateRegex("New Name", applicant.NewName, @"^[a-zA-Z\s]*$", "can only contain alphabets and spaces.", errors);

        if (applicant.OldName == applicant.NewName)
        {
            errors.Add("New Applicant Name can't be the same as the Old Applicant Name.");
        }

        return errors;
    }
}
