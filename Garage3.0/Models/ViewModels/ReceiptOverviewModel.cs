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

        public string Price { get; set; }

        public string Cost { get; set; }

        public string Savings { get; set; }
    }
}
