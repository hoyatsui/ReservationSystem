using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotnetCoding.Core.Models;

namespace DotnetCoding.Services.Interfaces
{
    public interface IProviderService
    {
        Task<IEnumerable<Provider>> GetAllProviders();
        Task<Provider> CreateProviderAsync(string name);
        Task DeleteProviderAsync(int providerId);
    }
}
