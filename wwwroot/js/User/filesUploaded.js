function downloadFile(filePath, callback) {
  const link = document.createElement("a");
  link.href = filePath;
  link.download = filePath.substring(filePath.lastIndexOf("/") + 1);
  document.body.appendChild(link);
  link.click();
  document.body.removeChild(link);

  // Call the callback function if provided
  if (typeof callback === "function") {
    callback();
  }
}

function GetReportFile(filePath) {
  const formData = new FormData();
  formData.append("excel", filePath);
  fetch("/User/DownloadExcelFile", { method: "post", body: formData })
    .then((res) => res.json())
    .then((data) => {
      if (data.status) {
        const fileName = data.filePath.substring(
          data.filePath.indexOf("/reports/") + "/reports/".length
        );
        downloadFile(`${applicationRoot}/reports/${fileName}`, function () {
          // After the file is downloaded, delete it
          const formData = new FormData();
          formData.append("reportFile", data.filePath);
          fetch("/User/DeleteReportFile", { method: "post", body: formData })
            .then((res) => res.json())
            .then((result) => {
              if (result.status) {
                console.log("File deleted successfully.");
              } else {
                console.error("Failed to delete the file.");
              }
            });
        });
      }
    });
}

$(document).ready(function () {
  const tbody = $("#reportBody");
  let fileType = "";

  filesUploaded.map((item) => {
    const file =
      item.fileUsed.length === 0
        ? "NIL"
        : item.fileUsed.substring(
            item.fileUsed.indexOf("/uploads/") + "/uploads/".length
          );
    if (item.updatedColumn == "eligibleForPension") {
      fileType = "Eligibility Updation File";
    } else if (item.updatedColumn == "accountNo") {
      fileType = "Account Updation File";
    } else if (item.updatedColumn == "ifscCode") {
      fileType = "Ifsc Code Updation File";
    } else if (item.updatedColumn == "applicantName") {
      fileType = "Name Updation File";
    }
    const tr = $("<tr/>");
    tr.append(`
       <td>${item.updatedAt}</td>
       <td>${item.updatedBy}</td>
       <td style="cursor:pointer" onclick='GetReportFile("${
         item.fileUsed
       }")'>${file}</td>
        <td>${item.fileUsed.length == 0 ? "NIL" : fileType}</td>
       
    `);
    tbody.append(tr);
  });
});
