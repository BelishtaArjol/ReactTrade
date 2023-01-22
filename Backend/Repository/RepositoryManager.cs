using Contracts;
using Entities;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private RepositoryContext _repositoryContext;
        private IClientRepository _clientRepository;
        private ICurrencyRepository _currencyRepository;
        private IBankAccountRepository _bankAccountRepository;
        private IBankTransactionRepository _bankTransactionRepository;
        private ICategoryRepository _categoryRepository;
        private IProductRepository _productRepository;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public IClientRepository ClientRepository
        {
            get
            {
                if (_clientRepository == null)
                    _clientRepository = new ClientRepository(_repositoryContext);

                return _clientRepository;
            }
        }

        public ICurrencyRepository CurrencyRepository
        {
            get
            {
                if (_currencyRepository == null)
                    _currencyRepository = new CurrencyRepository(_repositoryContext);

                return _currencyRepository;
            }
        }

        public IBankAccountRepository BankAccountRepository
        {
            get
            {
                if (_bankAccountRepository == null)
                    _bankAccountRepository = new BankAccountRepository(_repositoryContext);

                return _bankAccountRepository;
            }
        }

        public IBankTransactionRepository BankTransactionRepository
        {
            get
            {
                if (_bankTransactionRepository == null)
                    _bankTransactionRepository = new BankTransactionRepository(_repositoryContext);

                return _bankTransactionRepository;
            }
        }

        public ICategoryRepository CategoryRepository
        {
            get
            {
                if (_categoryRepository == null)
                    _categoryRepository = new CategoryRepository(_repositoryContext);

                return _categoryRepository;
            }
        }

        public IProductRepository ProductRepository
        {
            get
            {
                if (_productRepository == null)
                    _productRepository = new ProductRepository(_repositoryContext);

                return _productRepository;
            }
        }

        public async Task SaveChangesAsync() => await _repositoryContext.SaveChangesAsync();
    }
}