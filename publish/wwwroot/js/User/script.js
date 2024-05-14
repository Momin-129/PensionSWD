function generateTable(array, index) {
  $("#citizenDetails").empty();
  $("#paymentDetails").empty();
  $("#paymentDetails").parent().parent().show();
  const citizenDetails = array[index];
  for (let key in citizenDetails) {
    if (key !== "Payment Details") {
      $("#citizenDetails").append(`
        <tbody>
        <tr>
          <th>${key}</th>
          <td>${
            citizenDetails[key] == null || citizenDetails[key] == ""
              ? "NO"
              : citizenDetails[key]
          }</td>
        </tr>
        </tbody>
    `);
    }
  }
  citizenDetails["Payment Details"].map((item) => {
    let month, year;
    for (let key in item) {
      if (key != "Reference Id") {
        month = key.split(" ")[1];
        year = key.split(" ")[2];
        break;
      }
    }
    $("#paymentDetails").append(`
        <tr>
          <td>${year}</td>
          <td>${month}</td>
          <td>${
            item["BankFile " + month + " " + year] == null ? "NO" : "YES"
          }</td>
          <td>${
            item["PaymentFile " + month + " " + year] == null ? "NO" : "YES"
          }</td>
        </tr>
    `);
  });
}

$(document).ready(function () {
  let filteredRecords = [];

  $("#searchType").on("change", function () {
    const value = $(this).val();
    $("#inputNumber").val("");
    if (value == "refId") {
      $("#inputNumber").attr("placeholder", "Reference Number");
    } else $("#inputNumber").attr("placeholder", "Bank Account Number");
  });

  $("#getRecord").submit(function (e) {
    e.preventDefault();
    const searchType = $("#searchType").val();
    const inputNumber = $("#inputNumber").val();
    if (searchType == "" || inputNumber == "") {
      searchType == "" &&
        $("#searchType").after(
          `<p class="fs-6 text-center text-danger">Please Select a Search Type</p>`
        );
      inputNumber == "" &&
        $("#inputNumber").after(
          `<p class="fs-6 text-center text-danger">Please provide a value</p>`
        );
    } else {
      $("#searchType").next("p").remove();
      $("#inputNumber").next("p").remove();
      filteredRecords = [];
      const formData = new FormData(this);
      count++;
      usercount++;
      formData.append("total", count);
      formData.append(username, usercount);
      fetch("/User/GetRecord", { method: "POST", body: formData })
        .then((res) => res.json())
        .then((data) => {
          const records = data.list;
          setSearchCount(data.total, data.usercount);
          if (records.length > 0) {
            $("#noRecord").hide();
            $("#paymentDetails").parent().parent().show();
            $("#duplicateRecords").parent().parent().show();
            $("#citizenDetails").parent().show();
            records.map((item) => {
              const obj = Object.assign(
                {},
                {
                  "Refrence Id": item.refNo,
                  District: item.divisionName,
                  "Applied In Tehsil": item.appliedTehsil,
                  Name: item.name,
                  Parentage: item.parentage,
                  "Date Of Birth": item.dob,
                  "Pension Category": item.pensionType,
                  "Present Address":
                    item.presentAddress +
                    " " +
                    item.presentTehsil +
                    " " +
                    item.presentDistrict,
                  "Bank Name": item.bankName,
                  "Branch Name": item.branchName,
                  "Ifsc Code": item.ifscCode,
                  "Account Number": item.accountNo,
                  "Current Status": item.currentStatus,
                  "JK-ISSS": item.jkIsss,
                  "GOI-NSAP": item.goiNsap,
                  "Duplicate Account Number": item.duplicateBankAccountNo,
                  "Shared With Department For Verification":
                    item.sharedWithDeptForVerification,
                  "Eligible For Pension": item.eligibleForPension,
                  "Verified By Department": item.deptVerified,
                  "Department Verified Date": item.deptVerifiedDate,
                  Remarks: item.remarks2,
                  Nsapchk: item.nsapChk,
                  "Junsugam Download Cyclye": item.janSugamDownloadCycle,
                  "CDAC Benificary": item.oldCdacbenificary,
                  "Arrear Total Amount": item.arrearTotalMonthsAmt,
                  "Arrear Bank File Generated": item.arrearBankFileGenerated,
                  "Arrear Bank Payment Dispursed":
                    item.arearBankFileBankPaymentOk,
                  "Payment Details": [],
                }
              );
              for (let key in item) {
                if (key.includes("paymentOfYear") && !key.includes("BankRes")) {
                  const remainingString = key.substring(
                    key.indexOf("paymentOfYear") + "paymentOfYear".length
                  );
                  const matches = remainingString.match(/([a-zA-Z]+)(\d+)/);
                  const paymentobj = {
                    "Reference Id": item["refNo"],
                    ["BankFile " + matches[1] + " " + matches[2]]:
                      item["paymentOfMonth" + remainingString],
                    ["PaymentFile " + matches[1] + " " + matches[2]]:
                      item["paymentOfMonth" + remainingString + "BankRes"],
                  };
                  obj["Payment Details"].push(paymentobj);
                }
              }
              filteredRecords.push(obj);
            });

            if (filteredRecords.length == 1) {
              $(".duplicate").hide();
              generateTable(filteredRecords, 0);
            } else {
              console.log("Why are you here?");
              $(".duplicate").show();
              $("#duplicateRecords").empty();
              $("#citizenDetails").empty();
              $("#paymentDetails").empty();
              $("#paymentDetails").parent().parent().hide();
              records.map((item, index) => {
                $("#duplicateRecords").append(`
                <tr>
                  <td><button class="btn btn-primary" id="${index}">Select</button></td>
                  <th>${item["refNo"]}</th>
                  <td>${item["name"]}</td>
                  <td>${item["parentage"]}</td>
                  <td>${item["divisionName"]}</td>
                </tr>
            `);
              });
            }
            $(document)
              .find(".vh-100")
              .removeClass("vh-100")
              .addClass("h-auto");
          } else {
            $("#paymentDetails").parent().parent().hide();
            $("#duplicateRecords").parent().parent().hide();
            $("#citizenDetails").parent().hide();
            $("#noRecord").show();
            $(document)
              .find(".h-auto")
              .removeClass("h-auto")
              .addClass("vh-100");
          }
        });
    }
  });

  $("#duplicateRecords").on("click", "button", function () {
    const index = parseInt($(this).attr("id"));
    generateTable(filteredRecords, index);
    console.log("Generate Talbe");
  });

  $("#getRecord").on("click", ":submit", function (e) {
    // Prevent form submission if clicked button is not a submit button
    if (!$(this).is('[type="submit"]')) {
      e.preventDefault();
    }
  });
});
