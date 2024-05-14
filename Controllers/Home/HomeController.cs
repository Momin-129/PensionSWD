using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PensionTemporary.Models;
using PensionTemporary.Models.Entities;

namespace PensionTemporary.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    protected readonly PensionContext dbContext;
    public HomeController(ILogger<HomeController> logger, PensionContext dbContext, HelperFunction helper)
    {
        _logger = logger;
        this.dbContext = dbContext;
    }

    public IActionResult Index(string? errorMessage)
    {
        ViewData["ErrorMessage"] = errorMessage ?? "";
        return View();
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromForm] IFormCollection form)
    {

        var username = new SqlParameter("@Username", form["username"].ToString());
        var userType = new SqlParameter("@UserType", form["userType"].ToString());
        var email = new SqlParameter("@Email", "testemail@gmail.com");
        var password = new SqlParameter("@Password", form["password"].ToString());


        int.TryParse(form["divisionCode"], out int divisionCode);

        var divisionCodeParam = new SqlParameter("@divisionCode", divisionCode);


        var result = await dbContext.Users.FromSqlRaw("EXEC UserRegistration @Username,@Email,@UserType,@divisionCode,@Password", username, email, userType, divisionCodeParam, password).ToListAsync();

        if (result != null && result.Count > 0)
            return RedirectToAction("Index");
        else
            return View();
    }

    [HttpGet]
    public IActionResult GetListForBankPayamentFile()
    {
        var data = dbContext.JkSwdeliveredMay30s
      .FromSqlRaw("EXEC GetListforBankPaymentFile")
      .ToList();

        var duplicates = new List<JkSwdeliveredMay30>();


        for (int i = 0; i < data.Count; i++)
        {
            for (int j = i + 1; j < data.Count; j++)
            {
                if (data[i].AccountNo == data[j].AccountNo)
                {
                    duplicates.Add(data[i]);
                    duplicates.Add(data[j]);
                }
            }
        }

        return Json(new { duplicates });
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromForm] IFormCollection form)
    {

        string? username = form["username"].ToString();
        string? password = form["password"].ToString();

        var usernameParam = new SqlParameter("@Username", username);
        var passParam = new SqlParameter("@Password", password);

        var User = dbContext.Users.FromSqlRaw("EXEC UserLogin @Username, @Password ", usernameParam, passParam).ToList();


        if (User.Count != 0)
        {

            int divisionCode = User[0].DivisionCode ?? 0;
            int UserId = User[0].Uuid;

            HttpContext.Session.SetInt32("divisionCode", divisionCode);
            HttpContext.Session.SetInt32("userId", UserId);
            HttpContext.Session.SetString("username", username);


            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, username),
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            _logger.LogInformation($"USER : {User[0].Username}");
            return RedirectToAction("Index", "User");
        }

        return RedirectToAction("Index", new { errorMessage = "Invalid Username or Password." });

    }


    public async Task<IActionResult> Logout()
    {
        // Sign out the current user
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return RedirectToAction("Index", "Home");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
