using RetailPricing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailPricing.Repositories.Interfaces
{
    public interface IPricingRepository
    {
        Task AddPricingRecordsAsync(IEnumerable<PricingRecord> records);
        Task<IEnumerable<PricingRecord>> SearchRecordsAsync(SearchCriteria criteria);
        Task<PricingRecord> GetPricingRecordByIdAsync(int id);
        Task<bool> UpdatePricingRecordAsync(PricingRecord record);
        Task<IEnumerable<PricingRecord>> GetRecordByIdAsync(int Id);
    }
}
