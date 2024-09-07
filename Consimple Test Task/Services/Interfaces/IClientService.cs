using Consimple_Test_Task.Models;
using Consimple_Test_Task.Models.ResponseModels;

namespace Consimple_Test_Task.Services.Interfaces
{
    public interface IClientService
    {
        Task<Client> CreateClientAsync(Client model);
        Task<Client?> UpdateClientAsync(Client model);
        Task<Client?> GetClientAsync(int id);
        Task<IEnumerable<Client>> GetAllClientsAsync();
        Task DeleteClientAsync(int id);
        Task<IEnumerable<ClientDataResponseModel>> FindClientsByDateOfBirthAsync(DateTime dateOfBirth);
    }

}
