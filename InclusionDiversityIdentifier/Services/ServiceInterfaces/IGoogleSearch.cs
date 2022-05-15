using InclusionDiversityIdentifier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.CustomSearchAPI.v1.Data;

namespace InclusionDiversityIdentifier.Services.ServiceInterfaces
{
    public interface IGoogleSearch
    {
        Search PerformGoogleSearch(string querySearche);
    }
}
