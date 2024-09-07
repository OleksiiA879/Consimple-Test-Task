using Consimple_Test_Task.Models;
using Consimple_Test_Task.Models.ResponseModels;
using Consimple_Test_Task.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Consimple_Test_Task.Services
{
    public class ClientService : IClientService
    {
        private readonly ApplicationDbContext _dbContext;

        public ClientService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }



        public async Task<Client> CreateClientAsync(Client model)
        {
            var existingClient = await _dbContext.Clients.FindAsync(model.Id);
            if (existingClient != null)
                return existingClient;

            var newClient = new Client
            {
                FullName = model.FullName,
                DateOfBirth = model.DateOfBirth.Date,
                DateOfRegistration = DateTime.UtcNow.Date
            };
            await _dbContext.Clients.AddAsync(newClient);
            await _dbContext.SaveChangesAsync();
            return newClient;
        }

        public async Task<Client?> UpdateClientAsync(Client model)
        {
            if (model == null)
                return null;

            var existingClient = await _dbContext.Clients.FirstOrDefaultAsync(c => c.Id == model.Id);
            if (existingClient == null)
                return null;

            existingClient.FullName = model.FullName;

            await _dbContext.SaveChangesAsync();
            return existingClient;
        }

        public async Task<Client?> GetClientAsync(int id)
        {
            return await _dbContext.Clients.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Client>> GetAllClientsAsync()
        {
            return await _dbContext.Clients.ToListAsync();
        }

        public async Task DeleteClientAsync(int id)
        {
            var client = await GetClientAsync(id);
            if (client == null) return;

            _dbContext.Clients.Remove(client);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<ClientDataResponseModel>> FindClientsByDateOfBirthAsync(DateTime dateOfBirth)
        {
            return await _dbContext.Clients
                .Where(c => c.DateOfBirth.Date == dateOfBirth.Date)
                .Select(c => new ClientDataResponseModel
                {
                    Id = c.Id,
                    FullName = c.FullName
                })
                .ToListAsync();
        }
    }
}
