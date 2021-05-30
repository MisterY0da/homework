using System;
using System.Collections.Generic;

#nullable disable

namespace DbKursovaya.Models
{
    public partial class EmployeeClient
    {
        public int EmployeeId { get; set; }
        public int ClientId { get; set; }
        public DateTime? AttractingDate { get; set; }
        public decimal? DealSum { get; set; }
        public double? DealPercentage { get; set; }
        public DateTime? DealDate { get; set; }

        public virtual Client Client { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
