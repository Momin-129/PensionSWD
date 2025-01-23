function setSearchCount(number, userCount) {
  const numberString = number.toString();
  $("#searchCount").empty();
  $("#searchCount").append(`
        &copy; 2024 - SWD Pension
        &nbsp; &nbsp; Total Number Of Searches
    `);
  for (let i = 0; i < numberString.length; i++) {
    const inputBox = $(
      `<p class="border border-dark d-flex justify-content-center align-items-center" style="height:30px;width:30px;margin:0"/>`
    );
    inputBox.append(numberString[i]);

    $("#searchCount").append(inputBox);
  }
  $("#searchCount").append(`
        &nbsp; &nbsp; Your Total Searches
    `);
  const userCountString = userCount.toString();
  for (let i = 0; i < userCountString.length; i++) {
    const inputBox = $(
      `<p class="border border-dark d-flex justify-content-center align-items-center" style="height:30px;width:30px;margin:0"/>`
    );
    inputBox.append(userCountString[i]);

    $("#searchCount").append(inputBox);
  }
}

$(document).ready(async function () {
  const searchCount = await fetch("/User/GetSearchCount");
  const result = await searchCount.json();
  let count = result.totalCount;
  let usercount = result.userTotal;
  let username = result.username;
  let userType = result.userType;

  if (userType == "Sectary") {
    $("ul.userSection").append(`
    <li class="nav-item">
       <a class="nav-link" href="/User/InsertNewCycle">Insert New Cycle</a>
    </li>
    <li class="nav-item dropdown">
        <a class="nav-link dropdown-toggle" href="#" role="button" data-bs-toggle="dropdown"
            aria-expanded="false">
            Generate
        </a>
        <ul class="dropdown-menu">
            <li>
                <a class="nav-link" href="/User/GenerateBankFile">Bank Payment File</a>
            </li>
            <li>
                <hr class="dropdown-divider">
            </li>
            <li>
                <a class="nav-link" href="/User/GenerateFileForDepartment">Verification File For Department</a>
            </li>
        </ul>
    </li>
  `);
    $("#updateRecordsUl").append(`
     <li>
        <hr class="dropdown-divider">
     </li>
    <li>
        <a class="dropdown-item" href="/User/DepartmentVerified">
            Department Verified Records
        </a>
    </li>
  `);
  }

  $(document).find(".navend").removeClass("w-100");
  $(".userSection").show();
  $("#registerButton").hide();
  $("#username").text(username.toUpperCase());
  setSearchCount(count, usercount);
});
