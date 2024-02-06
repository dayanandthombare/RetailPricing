using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RetailPricing.Data;
using RetailPricing.Models;
using RetailPricing.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailPricing.Repositories
{
    public class PricingRepository : IPricingRepository
    {
        private readonly AppDbContext _context;

        public PricingRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddPricingRecordsAsync(IEnumerable<PricingRecord> records)
        {
            try
            {
                await _context.PricingRecords.AddRangeAsync(records);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<PricingRecord>> SearchRecordsAsync(SearchCriteria criteria)
        {
            
            var query = _context.PricingRecords.AsQueryable();

            if (!string.IsNullOrEmpty(criteria.StoreId))
            {
                query = query.Where(r => r.StoreId == criteria.StoreId);
            }

            if (!string.IsNullOrEmpty(criteria.SKU))
            {
                query = query.Where(r => r.SKU == criteria.SKU);
            }

            if (!string.IsNullOrEmpty(criteria.ProductName))
            {
                query = query.Where(r => r.ProductName == criteria.ProductName);
            }

          

            return await query.ToListAsync();
        }

        public async Task<PricingRecord> GetPricingRecordByIdAsync(int id)
        {
            return await _context.PricingRecords.FindAsync(id);
        }

        public async Task<bool> UpdatePricingRecordAsync(PricingRecord record)
        {
            var existingRecord = await _context.PricingRecords.FindAsync(record.Id);
            if (existingRecord == null)
            {
                return false;
            }

          
            existingRecord.ProductName = record.ProductName;
            existingRecord.Price = record.Price;
           

            _context.PricingRecords.Update(existingRecord);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<PricingRecord>> GetRecordByIdAsync(int Id)
        {
           
            var query = _context.PricingRecords.AsQueryable();

            if (Id > 0)
            {
                query = query.Where(r => r.Id == Id);
            }


            return await query.ToListAsync();
        }
    }
}
