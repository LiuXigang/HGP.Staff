﻿using HGP.Staff.DomainModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HGP.Staff.Services
{
    public interface IDepartmentService
    {
        Task<List<Department>> GetllAllAsync();
        Task<int> AddRangeAsync(List<Department> model);
        Task<int> DeleteByTimeAsync(DateTime time);
    }
}
