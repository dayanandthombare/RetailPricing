using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using RetailPricing.Models;
namespace RetailPricing.Services.Interfaces
{
    public interface IPricingService
    {
        public Task<bool> ProcessFileAsync(IFormFile file);
        Task<IEnumerable<PricingRecord>> SearchAsync(SearchCriteria criteria);
        Task<bool> UpdatePricingRecordAsync(int id, PricingRecordUpdateDto updateDto);
        Task<IEnumerable<PricingRecord>> GetRecordByIdAsync(int Id);
    }
}
