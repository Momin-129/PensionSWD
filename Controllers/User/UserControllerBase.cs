using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PensionTemporary.Models.Entities;

namespace PensionTemporary.Controllers.Users
{
    [Authorize]
    public partial class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        protected readonly PensionContext dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly HelperFunction _helper;
        private readonly ValidationFunctions validation;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserController(ILogger<UserController> logger, PensionContext dbContext, HelperFunction helper, ValidationFunctions _validation, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            this.dbContext = dbContext;
            _helper = helper;
            _webHostEnvironment = webHostEnvironment;
            validation = _validation;
            _httpContextAccessor = httpContextAccessor;
        }
        [HttpGet]
        public IActionResult Index()
        {

            return View();
        }

        public IActionResult Record()
        {
            return View();
        }

        public IActionResult Reports()
        {
            return View();
        }

        public IActionResult UpdateIndividual()
        {
            return View();
        }

        public IActionResult UpdateMultiple()
        {
            return View();
        }

        public IActionResult FilesUploaded()
        {
            return View();
        }
    }
}