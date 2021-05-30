using System;
using System.Collections.Generic;

#nullable disable

namespace DbKursovaya.Models
{
    public partial class Employee
    {
        public Employee()
        {
            EmployeeClients = new HashSet<EmployeeClient>();
            Salaries = new HashSet<Salary>();
        }

        public int Id { get; set; }
        public decimal SalaryFixed { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public int OrganizationId { get; set; }

        public virtual Organization Organization { get; set; }
        public virtual ICollection<EmployeeClient> EmployeeClients { get; set; }
        public virtual ICollection<Salary> Salaries { get; set; }
    }
}
