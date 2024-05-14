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

    private bool IsDigitOnly(string value)
    {
        if (!string.IsNullOrEmpty(value) && !value.All(char.IsDigit))
        {
            return false;
        }
        return true;
    }
    public List<string> ValidateNewCycleExcelModel(NewCycleExcelModel model)
    {
        var errorList = new List<string>();

        if (model.AccountNo!.Length != 16)
        {
            errorList.Add("Account Number should be of 16 characters.");
        }
        if (model.IFSCCode!.Length != 11)
        {
            errorList.Add("IFSC Code should be of 11 characters.");
        }
        // Validate fields containing only digits
        if (!IsDigitOnly(model.DistrictUidForBank!))
        {
            errorList.Add("District Uid For Bank should contain only digits.");
        }
        if (!IsDigitOnly(model.LgdStateCode!))
        {
            errorList.Add("LG State Code should contain only digits.");
        }
        if (!IsDigitOnly(model.DivisionCode!))
        {
            errorList.Add("Division Code should contain only digits.");
        }
        if (!IsDigitOnly(model.DistrictLGDcode!))
        {
            errorList.Add("District LGD Code should contain only digits.");
        }
        if (!IsDigitOnly(model.TSWOTehsilCode!))
        {
            errorList.Add("TSWO Tehsil Code should contain only digits.");
        }

        if (!IsDigitOnly(model.AccountNo))
        {
            errorList.Add("Account Number should contain only digits.");
        }
        if (model.PreviousPension == "YES" && !IsDigitOnly(model.PreviousPensionAccountNo!))
        {
            errorList.Add("Previous Pension Account Number should contain only digits.");
        }



        return errorList;
    }


    public List<string> ValidateUpdateEligibility(EligibleExcelModel eligible)
    {
        var errorList = new List<string>();

        if (eligible.AccountNo.Length != 16)
        {
            errorList.Add("Account Number Should be of 16 characters");
        }
        if (eligible.OldEligibleForPension == eligible.NewEligibleForPension)
        {
            errorList.Add("New Value should not be same as old value.");
        }
        if (eligible.IfscCode.Length != 11)
        {
            errorList.Add("Ifsc Code Length should be of 11 characters.");
        }
        if (!Regex.IsMatch(eligible.Reason, @"^[a-zA-Z0-9\s]*$"))
        {
            errorList.Add("Reason should only contain alphabets digits and space.");
        }


        return errorList;
    }

    public List<string> ValidateWeedoutCase(WeedoutExcelModel eligible)
    {
        var errorList = new List<string>();

        if (eligible.AccountNo.Length != 16)
        {
            errorList.Add("Account Number Should be of 16 characters");
        }
        if (eligible.IfscCode.Length != 11)
        {
            errorList.Add("Ifsc Code Length should be of 11 characters.");
        }
        if (!Regex.IsMatch(eligible.Reason, @"^[a-zA-Z0-9\s]*$"))
        {
            errorList.Add("Reason should only contain alphabets digits and space.");
        }

        return errorList;
    }

    public List<string> ValidateUpdateAccountNo(AccountNoExcelModel account)
    {
        var errorList = new List<string>();

        if (account.OldAccountNo.Length != 16)
        {
            errorList.Add("Old Account Number Should be of 16 characters");
        }
        if (account.NewAccountNo.Length != 16)
        {
            errorList.Add("New Account Number Should be of 16 characters");
        }
        if (account.OldAccountNo == account.NewAccountNo)
        {
            errorList.Add("New Account Number can't be same as Old Account Number.");
        }

        if (account.EligibleForPension.ToLower() != "yes" && account.EligibleForPension.ToLower() != "no")
        {
            errorList.Add("Eligible For Pension Should be only YES OR NO.");
        }
        if (account.IfscCode.Length != 11)
        {
            errorList.Add("Ifsc Code Length should be of 11 characters.");
        }
        if (!Regex.IsMatch(account.Reason, @"^[a-zA-Z0-9\s]*$"))
        {
            errorList.Add("Reason should only contain alphabets digits and space.");
        }



        return errorList;
    }

    public List<string> ValidateUpdateIfscCode(IfscExcelModel ifsc)
    {
        var errorList = new List<string>();
        if (ifsc.AccountNo.Length != 16)
        {
            errorList.Add("Account Number Should be of 16 characters");
        }
        if (ifsc.OldIfscCode.Length != 11)
        {
            errorList.Add("Old Ifsc Code should be of lenght 11.");
        }
        if (ifsc.NewIfscCode.Length != 11)
        {
            errorList.Add("New Ifsc Code should be of lenght 11.");
        }
        if (ifsc.OldIfscCode == ifsc.NewIfscCode)
        {
            errorList.Add("New Ifsc Code can't be same as Old Ifsc Code.");
        }
        if (ifsc.EligibleForPension.ToLower() != "yes" && ifsc.EligibleForPension.ToLower() != "no")
        {
            errorList.Add("Ifsc Code Length should be of 11 characters.");
        }
        if (!Regex.IsMatch(ifsc.Reason, @"^[a-zA-Z0-9\s]*$"))
        {
            errorList.Add("Reason should only contain alphabets digits and space.");
        }
        return errorList;
    }

    public List<string> ValidateUpdateApplicantName(NameExcelModel applicant)
    {
        var errorList = new List<string>();
        if (applicant.AccountNo.Length != 16)
        {
            errorList.Add("Account Number Should be of 16 characters");
        }
        if (applicant.IfscCode.Length != 11)
        {
            errorList.Add("Ifsc Code Should be of 11 characters");

        }
        if (!Regex.IsMatch(applicant.OldName, @"^[a-zA-Z\s]*$"))
        {
            errorList.Add("Old Name can only container alphabets and space.");

        }
        if (!Regex.IsMatch(applicant.NewName, @"^[a-zA-Z\s]*$"))
        {
            errorList.Add("New Name can only container alphabets and space.");

        }
        if (applicant.OldName == applicant.NewName)
        {
            errorList.Add("New Applicant Name can't be same as Old Applicant Name.");
        }
        if (!Regex.IsMatch(applicant.Reason, @"^[a-zA-Z0-9\s]*$"))
        {
            errorList.Add("Reason should only contain alphabets digits and space.");
        }

        return errorList;
    }
}
