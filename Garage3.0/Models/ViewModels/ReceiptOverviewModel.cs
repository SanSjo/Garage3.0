using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Garage3.Models.ViewModels
{
    public class ReceiptOverviewModel
    {
        public String Member { get; set; }
        public string Vehicle { get; set; }

        public string TimeParked { get; set; }

        public decimal Price { get; set; }

        public decimal Cost { get; set; }

        public decimal Savings { get; set; }
    }
}
