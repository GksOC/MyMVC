using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ScndMVC.Models;

namespace ScndMVC.Controllers
{
    public class DepartmentsController : Controller
    {
        public IActionResult Index()
        {
            List<Department> list = new List<Department>();
            list.Add(new Department { ID = 1, Name = "Eletronics" });
            list.Add(new Department { ID = 2, Name = "Fashion" });

            return View(list);
        }
    }
}
