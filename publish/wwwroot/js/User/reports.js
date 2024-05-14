function fetchHistory() {
  const refNo = $("#refNo").val();
  const formData = new FormData();
  formData.append("refNo", refNo);
  fetch("/User/GetHistory", { method: "POST", body: formData })
    .then((res) => res.json())
    .then((data) => {
      if (data.status) {
        $("#submit").next("p").remove();
        GenerateReport(JSON.parse(data.history));
      } else {
        $("#submit").after(
          `<p class="fs-1 fw-bold text-center ">${data.response}</p>`
        );
      }
    });
}

function GenerateReport(history) {
  $("#historyNicjk").hide();
  $("#history").hide();

  const tbodyNicjk = $("#reportBodyNicjk");
  const tbody = $("#reportBody");
  const refNo = $("#refNo").val();

  tbodyNicjk.empty();
  tbody.empty();

  if (username == "nicjk") {
    history.map((item) => {
      const tr = $("<tr/>");
      tr.append(`
        <td>${refNo}</td>
        <td>${item.TableName}</td>
        <td>${item.UpdatedColumn}</td>
        <td>${
          item.ColumnDescription == null
            ? "NO Discription available"
            : item.ColumnDescription
        }</td>
        <td>${item.OldValue}</td>
        <td>${item.NewValue}</td>
        <td>${item.BulkUpdate ? "YES" : "NO"}</td>
        <td>${
          item.FileUsed.length == 0
            ? "NIL"
            : item.FileUsed.substring(item.FileUsed.indexOf("/uploads"))
        }</td>
        <td>${item.UpdatedBy}</td>
        <td>${item.IpAddress}</td>
        <td>${item.Reason}</td>
        <td>${item.UpdatedAt}</td>
    `);
      tbodyNicjk.append(tr);
    });
    $("#historyNicjk").show();
  } else {
    let fileType = "";

    history.map((item) => {
      const file =
        item.FileUsed.length == 0
          ? refNo
          : item.FileUsed.substring(
              item.FileUsed.indexOf("/uploads/") + "/uploads/".length
            );
      if ((item.UpdatedColumn = "eligibleForPension")) {
        fileType = "Eligibility Updation File";
      } else if (item.UpdatedColumn == "accountNo") {
        fileType = "Account Updation File";
      } else if (item.UpdatedColumn == "ifscCode") {
        fileType = "Ifsc Code Updation File";
      } else if (item.UpdatedColumn == "applicantName") {
        fileType = "Name Updation File";
      }

      const tr = $("<tr/>");
      tr.append(`
       <td>${item.UpdatedAt}</td>
       <td>${item.UpdatedBy}</td>
       <td>${file}</td>
        <td>${item.FileUsed.length == 0 ? "Individual Record" : fileType}</td>
       
    `);
      tbody.append(tr);
    });
    $("#history").show();
  }
}

$(document).ready(function () {
  $("#historyNicjk").hide();
  $("#history").hide();
  $("#historyForm").submit(function (e) {
    e.preventDefault();
    fetchHistory();
  });
});
