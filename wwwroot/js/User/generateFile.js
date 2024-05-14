function downloadFile(filePath, callback) {
  const link = document.createElement("a");
  link.href = filePath;
  link.download = filePath.substring(filePath.lastIndexOf("/") + 1);
  document.body.appendChild(link);
  link.click();
  document.body.removeChild(link);

  // Call the callback function if provided
  if (callback) {
    callback();
  }
}

function getCheckedValues() {
  var values = [];
  $('input[type="checkbox"][name="duplicates"]:checked').each(function () {
    values.push($(this).val());
  });
  return values;
}

function fetchAndUpdateEligibilityBankFile(values) {
  const updateData = new FormData();
  updateData.append("duplicates", JSON.stringify(values));

  return fetch("/User/UpdateEligibilityBankFile", {
    method: "post",
    body: updateData,
  }).then((res) => res.json());
}

function fetchAndGetListForBankPaymentFile() {
  return fetch("/User/GetListForBankPayamentFile", {
    method: "post",
    body: formdata,
  }).then((res) => res.json());
}

function deleteReportFile(filePath) {
  const formData = new FormData();
  formData.append("reportFile", filePath);
  return fetch("/User/DeleteReportFile", {
    method: "post",
    body: formData,
  }).then((res) => res.json());
}

function handleFormSubmit(e) {
  e.preventDefault();
  $(".generateFile").append(`
    <div id="insertSpinner" class="spinner-border spinner-border-sm text-primary" role="status">
       <span class="visually-hidden">Loading...</span>
    </div>
  `);
  formdata = new FormData(this);

  const url =
    $(this).attr("id") == "bankFile"
      ? "/User/GetListForBankPayamentFile"
      : "/User/GenerateFileForDepartment";
  fetch(url, {
    method: "post",
    body: formdata,
  })
    .then((res) => res.json())
    .then((data) => {
      if (data.status) {
        const fileUrl = `${applicationRoot}/reports/${data.filename}`.replace(
          /\\/g,
          "/"
        );
        downloadFile(fileUrl, function () {
          deleteReportFile(data.filepath).then((result) => {
            if (result.status) {
              console.log("File deleted successfully.");
            } else {
              console.error("Failed to delete the file.");
            }
          });
        });
        $(".generateFile").find("#insertSpinner").remove();
      } else {
        if (url == "/User/GetListForBankPayamentFile") {
          choiceGrid(data.response, data.duplicateType);
        } else {
          $("form")
            .find("button")
            .after(`<p class="text-danger text-center">${data.response}</p>`);
        }
        $(".generateFile").find("#insertSpinner").remove();
      }
    });
}

$(document).ready(function () {
  $("form").on("submit", handleFormSubmit);
});
