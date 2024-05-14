$(document).ready(function () {
  $("form").on("submit", function (event) {
    if (!(window.captchaText == $("#captchaInput").val())) {
      event.preventDefault();
    }
  });
});
