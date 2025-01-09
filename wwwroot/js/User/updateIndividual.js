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
function formDataToObject(formData) {
  var object = {};
  formData.forEach(function (value, key) {
    object[key] = value;
  });
  return object;
}

function setErrorSpan(id, msg) {
  if ($("#" + id + "Msg").length) {
    $("#" + id + "Msg").text(msg);
  } else {
    const newSpan = $("<span>")
      .attr({ id: id + "Msg", class: "fs-6 text-danger" })
      .text(msg);
    $("#" + id).after(newSpan);
  }
  if (msg.length == 0) $("#" + id + "Msg").remove();
}

$(document).ready(function () {
  $("#updateForm").submit(function (e) {
    console.log("HERE");
    e.preventDefault();
    const formData = new FormData(this);
    var dataObject = formDataToObject(formData);
    let error = false;

    for (let item in dataObject) {
      if (item.includes("New")) {
        const oldKey = "Old" + item.substring("New".length);
        console.log(
          `Comparing New (${item}): ${dataObject[item]} with Old (${oldKey}): ${dataObject[oldKey]}`
        );
        if (dataObject[item] == dataObject[oldKey]) {
          setErrorSpan(item, "Old and New Value should be different.");
          error = true;
        } else {
          setErrorSpan(item, "");
        }
      } else if (item.includes("eligibleForPension")) {
        if (
          dataObject[item].toLowerCase() !== "yes" &&
          dataObject[item].toLowerCase() !== "no"
        ) {
          setErrorSpan(item, "Eligible For Pension value should be YES OR NO.");
          error = true;
        } else {
          setErrorSpan(item, "");
        }
      } else if (item.includes("accountNo")) {
        if (dataObject[item].length !== 16) {
          setErrorSpan(item, "Account Number should be of 16 digits.");
          error = true;
        } else {
          setErrorSpan(item, "");
        }
      } else if (item.includes("ifscCode")) {
        if (dataObject[item].length !== 11) {
          setErrorSpan(item, "Ifsc Code should be of 11 digits.");
          error = true;
        } else {
          setErrorSpan(item, "");
        }
      }
    }

    if (!error) {
      fetch("/User/UpdateIndividual", {
        method: "POST",
        body: formData,
      })
        .then((res) => res.json())
        .then((data) => {
          console.log(data);
          $("#RefNo").val("");
          $("#ColumnToEdit").nextAll().remove();
          $("#ColumnToEdit").after(
            `<p class="fs-6 fw-bold text-success">${data.response}</p>`
          );
        });
    }
  });

  $("#ColumnToEdit").change(function () {
    const value = $(this).val();
    $(this).nextAll().remove();

    if (value == "eligibleForPension" || value == "applicantName") {
      $(this).after(`
            <input placeholder="Account Number" type="text" name="AccountNo" id="AccountNo" class="form-control w-50 rounded-pill" required />
            <input placeholder="Ifsc Code" type="text" name="IfscCode" id="IfscCode" class="form-control w-50 rounded-pill" required />
        `);
      if (value == "applicantName") {
        $(this).after(`
            <input placeholder="Eligible For Pension" type="text" name="EligibleForPension" id="EligibleForPension" class="form-control w-50 rounded-pill" required />
        `);
      }
    } else if (value == "accountNo") {
      $(this).after(`
            <input placeholder="Ifsc Code" type="text" name="ifscCode" id="ifscCode" class="form-control w-50 rounded-pill" required />
            <input placeholder="Eligible For Pension" type="text" name="EligibleForPension" id="EligibleForPension" class="form-control w-50 rounded-pill" required />
        `);
    } else if (value == "ifscCode") {
      $(this).after(`
            <input placeholder="Account Number" type="text" name="accountNo" id="accountNo" class="form-control w-50 rounded-pill" required />
            <input placeholder="Eligible For Pension" type="text" name="EligibleForPension" id="EligibleForPension" class="form-control w-50 rounded-pill" required />
        `);
    }

    if (value != "") {
      $(this).after(`
        <input type="text" placeholder="Old ${formatKey(
          value
        )}" class="form-control w-50 rounded-pill" name="${"OldValue"}" id="${"OldValue"}" required/>
       <input type="text" placeholder="New ${formatKey(
         value
       )}" class="form-control w-50 rounded-pill" name="${"NewValue"}" id="${"NewValue"}" required/>
      `);
    }

    $(this).after(
      `<input type="text" class="form-control w-50 rounded-pill" name="Reason" id="Reason" placeholder="Reason of edit" required />`
    );

    $(this)
      .parent()
      .append(
        `<button type="submit" class="btn btn-dark rounded-5 mx-auto w-25">Update</button>`
      );
  });
});
