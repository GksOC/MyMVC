using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ScndMVC.Models.Services;

namespace ScndMVC.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _selllerService;

        public SellersController(SellerService selllerService)
        {
            _selllerService = selllerService;
        }

        public IActionResult Index()
        {
            var list = _selllerService.FindAll();
            return View(list);
        }
    }
}
