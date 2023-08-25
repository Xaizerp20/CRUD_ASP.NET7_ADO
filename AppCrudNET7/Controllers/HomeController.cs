using AppCrudNET7.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


using AppCrudNET7.Repositories.Contracts;

namespace AppCrudNET7.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGenericRepository<Department> _departmentRepository;
        private readonly IGenericRepository<Employee> _employeeRepository;


        // HomeController handles incoming requests and coordinates
        // application logic. It receives instances of logger and
        // generic repositories for departments and employees through
        // dependency injection.

        public HomeController(ILogger<HomeController> logger, IGenericRepository<Department> departmentRepository, IGenericRepository<Employee> employeeRepository)
        {
            _logger = logger;
            _departmentRepository = departmentRepository;
            _employeeRepository = employeeRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}