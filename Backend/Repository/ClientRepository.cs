using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class ClientRepository : RepositoryBase<Client>, IClientRepository
    {
        public ClientRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {

        }

        public void CreateClient(Client client) => Create(client);

        public void UpdateClient(Client client) => Update(client);

        public void DeleteClient(Client client) => Delete(client);

        public async Task<Client> GetClientByIdAsync(int id) =>
            await FindByCondition(x => x.Id.Equals(id))
            .SingleOrDefaultAsync();

        public async Task<IEnumerable<Client>> GetClientsAsync() =>
            await FindAll()
            .OrderBy(x => x.FirstName).ThenBy(x => x.LastName)
            .ToListAsync();


        public async Task<Client> GetClientByPhonesync(string phone) =>
            await FindByCondition(x => x.Phone.Equals(phone))
            .SingleOrDefaultAsync();

        public async Task<Client> GetClientByEmailAsync(string email) =>
           await FindByCondition(x => x.Email.Equals(email))
           .SingleOrDefaultAsync();

        public async Task<Client> GetClientByUserNameAsync(string userName) =>
           await FindByCondition(x => x.Username.Equals(userName))
           .SingleOrDefaultAsync();

        //public async Task<IEnumerable<Client>> GetClientsBankAccountAsync(int clienId) =>
        //     await FindByCondition(x => x.Id.Equals(clienId)).Include(Client, clienId)
        //    .OrderBy(x => x.FirstName).ThenBy(x => x.LastName)
        //    .ToListAsync();
    }
}
