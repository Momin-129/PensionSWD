function updateDataTable(selector, data, columns) {
  if ($.fn.DataTable.isDataTable(selector)) {
    $(selector).DataTable().clear().destroy();
  }

  if (data.length) {
    $(selector).DataTable({
      data,
      columns: columns.map((col, index) => ({
        title: col.title,
        data: index, // Map columns by index
      })),
      scrollX: true,
      responsive: true,
    });
  } else {
    $(selector).DataTable({
      data: [], // Empty data array
      columns: columns.map((col) => ({
        title: col.title,
        data: null, // No data available
      })),
      scrollX: true,
      responsive: true,
      language: {
        emptyTable: "No records available",
      },
    });
  }
}

$(document).ready(function () {
  $("#historyForm").on("submit", async function (e) {
    e.preventDefault();
    const formdata = new FormData(this);
    const response = await fetch("/User/GetHistory", {
      method: "POST",
      body: formdata,
    });
    const result = await response.json();
    console.log(result);
    updateDataTable("#historyTable", result.data, result.columns);
  });
});
