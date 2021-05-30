using System;
using System.Collections.Generic;

#nullable disable

namespace DbKursovaya.Models
{
    public partial class Organization
    {
        public Organization()
        {
            Employees = new HashSet<Employee>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
