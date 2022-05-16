using InclusionDiversityIdentifier.Database;
using InclusionDiversityIdentifier.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InclusionDiversityIdentifier.Repository
{
    public class BusinessRepo : IBusinessRepo
    {
        private readonly ApplicationDBContext _context;

        public BusinessRepo(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<Business>> GetAllBusiness()
        {
            var businessTable = await _context.Businesses.ToListAsync();
            return businessTable;
        }

        public async Task<bool> UpdateBusinessInformation(BusinessDiverseInfo businessUpdate)
        {
            var businessRecord = await _context.Businesses
                                       .FirstOrDefaultAsync(m => m.dunsNumId == businessUpdate.dunsNumId);

            businessRecord.isWomanOwned = businessUpdate.isWomanOwned;
            businessRecord.minorityOwnedDesc = businessUpdate.minorityOwnedDesc;

            _context.Update(businessRecord);

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<string>> GetAllBusinessNames() 
        {
            var businessNames = await _context.Businesses.Select(u => u.dunsName).ToListAsync();
            return businessNames;
        }
    }
}
