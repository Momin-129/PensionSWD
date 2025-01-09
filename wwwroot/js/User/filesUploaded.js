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

$(document).ready(async function () {
  const response = await fetch("/User/GetFileUploaded");
  const result = await response.json();
  console.log(result);
  $("#reportBody").DataTable({
    data: result.data,
    columns: result.columns,
    language: {
      emptyTable: "No records available",
    },
  });
});
