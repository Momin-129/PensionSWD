function downloadFile(filePath, callback) {
  const link = document.createElement("a");
  link.href = filePath;
  link.download = filePath.substring(filePath.lastIndexOf("/") + 1);
  document.body.appendChild(link);
  link.click();
  document.body.removeChild(link);

  setTimeout(function () {
    
    if (typeof callback === "function") {
      callback();
    }
  }, 1000);
}
$(document).ready(function () {
  $("form").on("submit", function (e) {
    e.preventDefault();
    const formdata = new FormData(this);
    fetch("/User/DepartmentVerified", { method: "post", body: formdata })
      .then((res) => res.json())
      .then((data) => {
        if (!data.status) {
          $(".container").append(`
                <p class="text-center text-center text-danger">${data.response}</p>
            `);
        } else {
          const fileUrl = `${applicationRoot}/uploads/${data.filename}`.replace(
            /\\/g,
            "/"
          );
          downloadFile(fileUrl, function () {
            const formData = new FormData();
            formData.append("reportFile", data.filePath);
            fetch("/User/DeleteReportFile", {
              method: "post",
              body: formData,
            })
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
  });
});
