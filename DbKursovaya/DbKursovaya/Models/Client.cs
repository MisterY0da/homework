using System;
using System.Collections.Generic;

#nullable disable

namespace DbKursovaya.Models
{
    public partial class Client
    {
        public Client()
        {
            EmployeeClients = new HashSet<EmployeeClient>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<EmployeeClient> EmployeeClients { get; set; }
    }
}
