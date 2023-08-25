using AppCrudNET7.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


using AppCrudNET7.Repositories.Contracts;
using System.Reflection.Metadata;

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


        [HttpGet]
        public async Task<IActionResult> listDepartments()
        {
            List<Department> _list = await _departmentRepository.List();

            return StatusCode(StatusCodes.Status200OK, _list);
        }


        // This method handles GET requests to retrieve a list of departments.
        // It fetches the department data from the repository and returns it as a response.

        [HttpGet]
        public async Task<IActionResult> listEmployees()
        {
            List<Employee> _list = await _employeeRepository.List();

            return StatusCode(StatusCodes.Status200OK, _list);
        }


        //This method handles POST requests to save information of a new employee.
        // It receives the employee data from the request body and saves it to the database.
        // Returns a success message if the save operation is successful, or an error message otherwise.

        [HttpPost]
        public async Task<IActionResult> saveEmployee([FromBody] Employee model)
        {
            bool _result = await _employeeRepository.Save(model);

            if (_result)
                return StatusCode(StatusCodes.Status200OK, new { value = _result, msg = "ok" });
            else
                return StatusCode(StatusCodes.Status500InternalServerError, new { value = _result, msg = "error" });

           
        }

        // This method handles PUT requests to update employee information.
        // It receives the updated employee data from the request body and updates it in the database.
        // Returns a success message if the update operation is successful, or an error message otherwise.

        [HttpPut]
        public async Task<IActionResult> editEmployee([FromBody] Employee model)
        {
            bool _result = await _employeeRepository.Edit(model);

            if (_result)
                return StatusCode(StatusCodes.Status200OK, new { value = _result, msg = "ok" });
            else
                return StatusCode(StatusCodes.Status500InternalServerError, new { value = _result, msg = "error" });
        }


        // This method handles DELETE requests to remove an employee's information.
        // It receives the ID of the employee to delete and performs the corresponding operation in the database.
        // Returns a success message if the deletion is successful, or an error message otherwise.
        [HttpDelete]
        public async Task<IActionResult> deleteEmployee(int idEmployee)
        {
            bool _result = await _employeeRepository.Delete(idEmployee);

            if (_result)
                return StatusCode(StatusCodes.Status200OK, new { value = _result, msg = "ok" });
            else
                return StatusCode(StatusCodes.Status500InternalServerError, new { value = _result, msg = "error" });
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