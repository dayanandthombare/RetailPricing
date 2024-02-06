using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailPricing.Models
{
    public class SearchCriteria
    {
        public string? StoreId { get; set; }
        public string? SKU { get; set; }
        public string? ProductName { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
