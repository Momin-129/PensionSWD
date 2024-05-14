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
  $("#insertNewCyleForm").submit(function (e) {
    e.preventDefault();
    $(".fileupload-btn").append(`
    <div id="insertSpinner" class="spinner-border spinner-border-sm text-primary" role="status">
       <span class="visually-hidden">Loading...</span>
    </div>
  `);
    const formData = new FormData(this);
    fetch("/User/InsertNewCycle", { method: "post", body: formData })
      .then((res) => res.json())
      .then((data) => {
        $(".fileupload-btn .insertSpinner").remove();
        $(".fileupload-btn").next("p").remove();
        if (data.status) {
          $("#excel").replaceWith($("#excel").val("").clone(true));
          $("#response").parent().find("thead").remove();
          $("#response").empty();
          $("#response").parent().find("button").remove();
          const response = data.response;
          for (let i = 0; i < response.length; i++) {
            const item = response[i];
            const thead = $("<thead/>").addClass("bg-dark text-white");
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
          });
          $("#response")
            .parent()
            .parent()
            .after(
              `<button class="btn btn-success" onclick='downloadFile("${applicationRoot}/uploads/${data.filename}");'>Export As EXCEL</button>`
            );
        } else {
          $(".fileupload-btn").after(
            `<p class="fs-6 text-danger text-center">${data.response}</p>`
          );
        }
        $(document).find(".vh-100").removeClass("vh-100").addClass("h-auto");
        $(".frame").remove();
      });
  });
});
