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
    public class ClientService : IClientService
    {
        public IUnitOfWork _unitOfWork;

        public ClientService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Client>> GetAllClientsAsync()
        {
            var clients = await _unitOfWork.Clients.GetAllAsync();
            return clients;
        }

        public async Task<Client> CreateClientAsync(string name)
        {
            var client = new Client { Name = name };
            await _unitOfWork.Clients.AddAsync(client);
            await _unitOfWork.SaveAsync();
            return client;
        }

        public async Task DeleteClientAsync(int clientId)
        {
            var client = await _unitOfWork.Clients.GetByIdAsync(clientId);
            if (client == null) throw new ArgumentException("Client not found");

            await _unitOfWork.Clients.DeleteAsync(client);
            await _unitOfWork.SaveAsync();
        }


    }
}
