using System.Collections.Generic;
using System.Linq;

namespace ScndMVC.Models.Services
{
    public class DepartmentService
    {
        private readonly MainContext _context;

        public DepartmentService(MainContext context)
        {
            _context = context;
        }

        public List<Department> FindAll()
        {
            return _context.Department.OrderBy(x => x.Name).ToList();
        }
    }
}
