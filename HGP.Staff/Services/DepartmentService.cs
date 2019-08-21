using System.Collections.Generic;
using System.Threading.Tasks;
using HGP.Staff.Context;
using HGP.Staff.DomainModel;
using Microsoft.EntityFrameworkCore;

namespace HGP.Staff.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly MyContext _context;
        public DepartmentService(MyContext context) => _context = context;

        public Task<int> AddRangeAsync(List<Department> model)
        {
            _context.Departments.AddRangeAsync(model);
            return _context.SaveChangesAsync();
        }

        public Task<List<Department>> GetllAllAsync()
        {
           return _context.Departments.ToListAsync();
        }
    }
}
