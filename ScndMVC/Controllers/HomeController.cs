using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ScndMVC.Models.ViewModels;

namespace ScndMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Ridiculus viverra curae consequat ligula viverra conubia suspendisse aliquam. Mauris lacus erat aenean; efficitur ligula vel. Amet sodales eleifend maximus sagittis eu scelerisque. Praesent placerat cubilia consectetur mus volutpat ut elementum. Porta blandit dignissim malesuada dapibus odio rhoncus. Pharetra sollicitudin suspendisse eget magna eget tincidunt ullamcorper. Bibendum aliquet hendrerit viverra; turpis pharetra varius libero.\r\n\r\nUltrices ligula aliquam sodales torquent phasellus donec mi leo. Dui luctus egestas per venenatis aliquam ex. Purus dignissim mollis viverra imperdiet ac interdum. In tristique torquent; nascetur orci placerat quisque nec purus. Elit sapien amet praesent bibendum tempor primis. Hendrerit vulputate inceptos venenatis orci scelerisque scelerisque fusce elementum class. Blandit mauris aptent, est dui felis dis.";
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
