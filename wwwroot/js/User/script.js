$(document).ready(function () {
  let citizenRecords = [];
  let citizenColumns = [];
  let paymentRecords = [];
  let paymentColumns = [];

  // Update input placeholder based on search type
  $("#searchType").on("change", function () {
    const placeholder =
      $(this).val() === "refId" ? "Reference Number" : "Bank Account Number";
    $("#inputNumber").val("").attr("placeholder", placeholder);
  });

  // Handle form submission
  $("#getRecord").submit(async function (e) {
    e.preventDefault();

    const formData = new FormData(this);

    try {
      const response = await fetch("/User/GetRecord", {
        method: "POST",
        body: formData,
      });
      const result = await response.json();

      citizenRecords = result.citizenDetails || [];
      citizenColumns = result.columns || [];
      paymentRecords = result.paymentRecords || [];
      paymentColumns = result.paymentColumns || [];

      handleDuplicateRecords(result.duplicateRecords, result.duplicateColumns);

      updateDataTable(
        "#citizenDetails",
        citizenRecords.length ? [citizenRecords[0]] : [],
        citizenColumns
      );
      updateDataTable(
        "#paymentDetails",
        paymentRecords.length ? [paymentRecords[0]] : [],
        paymentColumns
      );
    } catch (error) {
      console.error("Error fetching record:", error);
    }
  });

  // Handle duplicate record selection
  $(document).on("click", "#selectDuplicate", function () {
    const index = parseInt($(this).attr("data-value"), 10);

    updateDataTable(
      "#citizenDetails",
      index >= 0 ? [citizenRecords[index]] : [],
      citizenColumns
    );
    updateDataTable(
      "#paymentDetails",
      index >= 0 ? [paymentRecords[index]] : [],
      paymentColumns
    );
  });

  // Function to handle duplicate records
  function handleDuplicateRecords(duplicateRecords, duplicateColumns) {
    if (duplicateRecords && duplicateRecords.length > 0) {
      $("#duplicateRecords").parent().show();

      updateDataTable("#duplicateRecords", duplicateRecords, duplicateColumns);
    } else {
      $("#duplicateRecords").parent().hide();
    }
  }

  // Function to initialize or update DataTable
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
});
