function updateRegisterButton() {
  // Check if any error messages exist
  const anyErrors =
    $(".errorMsg").length > 0 ||
    $("#captchaError").text() != "" ||
    $("#captchaInput").val() == "" ||
    $("#userType").val() == "";
  console.log(anyErrors);

  // Disable or enable the register button based on the presence of errors
  $("#register").prop("disabled", anyErrors);
}

function setErrorSpan(id, msg) {
  let errorList = msg.split(",");
  let errorMsg = ``;

  errorList.forEach((item) => {
    if (item.trim() !== "") {
      errorMsg += `<p class="errorMsg">${item.trim()}</p>`;
    }
  });

  const errorSpan = $("#" + id + "Msg");
  if (errorSpan.length) {
    errorSpan.html(errorMsg); // Use .html to replace the contents instead of appending
  } else {
    const newSpan = $("<p>")
      .attr({ id: id + "Msg" })
      .html(errorMsg); // Use .html to set the initial content

    $("#" + id)
      .parent()
      .append(newSpan);
  }

  // Remove the error span if there are no error messages
  if (errorMsg.length === 0 && errorSpan.length) {
    errorSpan.remove();
  }

  updateRegisterButton();
}

function ValidateInputs(id, value) {
  if (id == "usernameInput") {
    if (!/^[a-zA-Z0-9]{4,20}$/.test(value)) {
      setErrorSpan(
        id,
        "Username should only contain alphabets and digits. and least of 4 characters."
      );
    } else setErrorSpan(id, "");
  }

  if (id == "password") {
    let msg = "";
    // Minimum length check
    if (value.length < 8) {
      msg += "Password must be at least 8 characters long,";
    }

    // Uppercase letter check
    if (!/[A-Z]/.test(value)) {
      msg += "Password must contain at least one uppercase letter,";
    }

    // Lowercase letter check
    if (!/[a-z]/.test(value)) {
      msg += "Password must contain at least one lowercase letter,";
    }

    // Digit check
    if (!/\d/.test(value)) {
      msg += "Password must contain at least one digit,";
    }

    // Special character check
    if (!/[$@*^!]/.test(value)) {
      msg += "Password must contain at least one special character [$@*^!],";
    }

    setErrorSpan(id, msg);
  }

  if (id == "confirmPassword") {
    let msg = "";
    if (value != $("#password").val()) {
      msg = "Confirm password is not same as password.";
    } else msg = "";

    setErrorSpan(id, msg);
  }
}

$(document).ready(function () {
  $("input").on("blur", function () {
    const id = $(this).attr("id");
    const value = $(this).val();
    if (id == "captchaInput") updateRegisterButton();
    ValidateInputs(id, value);
  });
  $("#userType").change(function () {
    updateRegisterButton();
    const value = $(this).val();
    $(this).next("select").remove();
    if (value != "Director Finance" && value != "Sectary") {
      $(this).after(`
           <div class="mb-2 w-100">
             <label class="text-muted">Division</label>
           </div>
          <select id="divisionCode" name="divisionCode" class="form-select mt-2">
              <option value=1>JAMMU</option>
              <option value=2>KASHMIR</option>
          </select>
      `);
    }
  });
});
