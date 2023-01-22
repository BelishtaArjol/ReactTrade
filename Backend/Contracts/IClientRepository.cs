using Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IClientRepository
    {
        Task<IEnumerable<Client>> GetClientsAsync();
        //Task<IEnumerable<Client>> GetClientsBankAccountAsync(int clienId);
        Task<Client> GetClientByIdAsync(int id);
        Task<Client> GetClientByPhonesync(string phone);
        Task<Client> GetClientByEmailAsync(string email);
        Task<Client> GetClientByUserNameAsync(string userName);
        void CreateClient(Client client);
        void UpdateClient(Client client);
        void DeleteClient(Client client);
    }
}
