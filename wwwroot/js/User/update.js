function downloadFile(filePath) {
  const link = document.createElement("a");
  link.href = filePath;
  link.download = filePath.substring(filePath.lastIndexOf("/") + 1);
  document.body.appendChild(link);
  link.click();
  document.body.removeChild(link);
}

function formatKey(key) {
  // Insert a space before all caps and capitalize the first character
  return (
    key
      // Break the string into parts before each uppercase letter (except the first if it is uppercase)
      .replace(/([A-Z])/g, " $1")
      // Trim any extra spaces that might have been added at the beginning
      .trim()
      // Capitalize the first letter of the resulting string
      .replace(/^./, function (str) {
        return str.toUpperCase();
      })
  );
}

$(document).ready(function () {
  $("#excelColumn").change(function () {
    const value = $(this).val();
    let fileName = "";
    if (value == "eligibleForPension") {
      fileName = "EligibleTemplate.xlsx";
    } else if (value == "accountNo") {
      fileName = "AccountNoTemplate.xlsx";
    } else if (value == "ifscCode") {
      fileName = "IfscTemplate.xlsx";
    } else if (value == "applicantName") {
      fileName = "NameTemplate.xlsx";
    } else if (value == "weedout") {
      fileName = "WeedoutTemplate.xlsx";
    }

    $(this).nextAll().remove();
    $(this).after(
      `<p class="fs-6 text-primary text-decoration-underline" style="cursor:pointer" onclick='downloadFile("${applicationRoot}/resources/${fileName}")'>Download Sample Template</p>
       <label for="excelfile" class="fs-6 fw-bold">Upload Excel File</label>
      <input type="file" class="form-control w-50 rounded-pill" name="excel" id="excel" required />

      <input id="submitButton" type="submit" class="btn btn-dark w-25 rounded-5 mx-auto" asp-controller="Home"
          asp-action="UpdateEligibility" value="Update Record" />
      `
    );
  });

  $("#updateForm").submit(function (e) {
    e.preventDefault();
    const formData = new FormData(this);
    console.log(formData);
    fetch("/User/UpdateMultiple", {
      method: "POST",
      body: formData,
    })
      .then((res) => res.json())
      .then((data) => {
        if (data.status) {
          $("#excel").replaceWith($("#excel").val("").clone(true));
          $("#response").parent().find("thead").remove();
          $("#response").empty();
          $("#response").parent().parent().find("button").remove();
          const response = data.response;
          for (let i = 0; i < response.length; i++) {
            const item = response[i];
            const thead = $("<thead/>");
            const theadTr = $("<tr/>");
            for (let key in item) {
              theadTr.append(`<th>${formatKey(key)}</th>`);
            }
            thead.append(theadTr);
            $("#response").parent().append(thead);
            break;
          }
          response.map((item) => {
            const tr = $(`<tr/>`);
            for (let key in item) {
              const str = item[key].split(",");
              const list = str.map((err) => `<p>${err}</p>`).join("");
              tr.append(`<td>${list}</td>`);
            }
            $("#response").append(tr);
            console.log(item);
            console.log($("#response").html());
          });
          $("#response")
            .parent()
            .parent()
            .parent()
            .append(
              `<button class="btn btn-success" onclick='downloadFile("${applicationRoot}/uploads/${data.fileName}");'>Export As EXCEL</button>`
            );
        } else {
          $(this).after(`<p class="fs-4 text-danger">${data.response}</p>`);
        }
      });
  });
});
