using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryManager
    {
        public IClientRepository ClientRepository { get; }
        public ICurrencyRepository CurrencyRepository { get; }
        public IBankAccountRepository BankAccountRepository { get; }
        public IBankTransactionRepository BankTransactionRepository { get; }
        public ICategoryRepository CategoryRepository { get; }
        public IProductRepository ProductRepository { get; }
        Task SaveChangesAsync();
    }
}