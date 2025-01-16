using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.API.Controllers
{
    public class DepartmentsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
