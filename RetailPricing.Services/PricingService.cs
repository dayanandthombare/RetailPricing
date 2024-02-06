using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Http;
using RetailPricing.Models;
using RetailPricing.Repositories.Interfaces;
using RetailPricing.Services.Interfaces;

namespace RetailPricing.Services
{
    public class PricingService : IPricingService
    {
        private readonly IPricingRepository _pricingRepository;

        public PricingService(IPricingRepository pricingRepository)
        {
            _pricingRepository = pricingRepository;
        }

        

        public async Task<bool> ProcessFileAsync(IFormFile file)
        {
            try
            {
                using (var reader = new StreamReader(file.OpenReadStream()))
                using (var csvReader = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HeaderValidated = null,
                    MissingFieldFound = null
                }))
                {
                    var records = csvReader.GetRecords<PricingRecord>().ToList();
                    await _pricingRepository.AddPricingRecordsAsync(records);
                    return true;
                }
            }
            catch (Exception ex)
            {
               
                return false;
            }
        }

        public async Task<IEnumerable<PricingRecord>> SearchAsync(SearchCriteria criteria)
        {
            try
            {
                return await _pricingRepository.SearchRecordsAsync(criteria);
            }
            catch (Exception ex)
            {
               
                throw;
            }
        }

        public async Task<bool> UpdatePricingRecordAsync(int id, PricingRecordUpdateDto updateDto)
        {
            try
            {
                var recordToUpdate = await _pricingRepository.GetPricingRecordByIdAsync(id);
                if (recordToUpdate == null) return false;

               
                recordToUpdate.ProductName = updateDto.ProductName;
                recordToUpdate.Price = updateDto.Price;
              

                await _pricingRepository.UpdatePricingRecordAsync(recordToUpdate);
                return true;
            }
            catch (Exception ex)
            {
               
                return false;
            }
        }

        public async Task<IEnumerable<PricingRecord>> GetRecordByIdAsync(int id)
        {
            try
            {
                return await _pricingRepository.GetRecordByIdAsync(id);
            }
            catch (Exception ex)
            {
               
                throw;
            }
        }
    }
}
