using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ScndMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ScndMVC.Models.Services
{
    public class SellerService
    {
        private readonly MainContext _context; 

        public SellerService(MainContext context)
        {
            _context = context;
        }

        public List<Seller> FindAll()
        {
            return _context.Seller.ToList();
        }

        public void Insert(Seller obj)
        {
            _context.Add(obj);
            _context.SaveChanges();
        }

    }
}
