using System;

namespace HGP.Staff.DomainModel
{
    public class Department
    {
        public int Id { get; set; }
        public int EmployeeNumber { get; set; }
        public DateTime Time { get; set; }
        public Organization Organization { get; set; }
    }
}
