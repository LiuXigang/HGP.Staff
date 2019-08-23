using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HGP.Staff.Context;
using HGP.Staff.DomainModel;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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

        public async Task<int> DeleteByTimeAsync(DateTime time)
        {
            var models = await _context.Departments.Where(n => n.Time == time).ToListAsync();
            _context.Departments.RemoveRange(models);
            return await _context.SaveChangesAsync();
        }
    }
}
