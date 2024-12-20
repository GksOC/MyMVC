using System.Collections.Generic;
using System.Linq;

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
            obj.Department = _context.Department.First(); //atribuindo um valor para DepartmentID obrigatório para evitar chegar nulo
            _context.Add(obj);
            _context.SaveChanges();
        }

        public Seller FindByID(int id)
        {
            return _context.Seller.FirstOrDefault(obj => obj.ID == id);
        }

        public void Remove(int id) 
        {
            var obj = _context.Seller.Find(id);
            _context.Seller.Remove(obj);
            _context.SaveChanges();
        }
    }
}
