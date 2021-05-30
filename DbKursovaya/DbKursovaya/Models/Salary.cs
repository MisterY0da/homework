using System;
using System.Collections.Generic;

#nullable disable

namespace DbKursovaya.Models
{
    public partial class Salary
    {
        public int Id { get; set; }
        public double Tax { get; set; }
        public decimal Sum { get; set; }
        public int EmployeeId { get; set; }
        public DateTime PaymentDate { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
