using InclusionDiversityIdentifier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InclusionDiversityIdentifier.Repository
{
    public interface IBusinessRepo
    {
        Task<List<Business>> GetAllBusiness();
        Task<List<string>> GetAllBusinessNames();
        Task<bool> UpdateBusinessInformation(BusinessDiverseInfo businessUpdate);
    }
}
