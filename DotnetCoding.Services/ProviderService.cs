using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotnetCoding.Core.Interfaces;
using DotnetCoding.Core.Models;
using DotnetCoding.Services.Interfaces;

namespace DotnetCoding.Services
{
    public class ProviderService : IProviderService
    {
        public IUnitOfWork _unitOfWork;

        public ProviderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Provider>> GetAllProviders()
        {
            var providers = await _unitOfWork.Providers.GetAllAsync();
            return providers;
        }

        public async Task<Provider> CreateProviderAsync(string name)
        {
            var provider = new Provider { Name = name };
            await _unitOfWork.Providers.AddAsync(provider);
            await _unitOfWork.SaveAsync();
            return provider;
        }

        public async Task DeleteProviderAsync(int providerId)
        {
            var provider = await _unitOfWork.Providers.GetByIdAsync(providerId);
            if (provider == null) throw new ArgumentException("Provider not found");

            await _unitOfWork.Providers.DeleteAsync(provider);
            await _unitOfWork.SaveAsync();
        }
    }
}
