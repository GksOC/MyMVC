using Microsoft.AspNetCore.Mvc;

namespace ScndMVC.Controllers
{
    public class SellersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
