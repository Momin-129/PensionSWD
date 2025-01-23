$(document).ready(function () {
  $("#loginForm").on("submit", async function (event) {
    event.preventDefault();
    const formdata = new FormData(this);
    const response = await fetch("/Home/Login", {
      method: "POST",
      body: formdata,
    });
    const result = await response.json();
    if (result.status) {
      window.location.href = result.url;
    } else {
      $("#error_message").text(result.message);
    }
  });
});
