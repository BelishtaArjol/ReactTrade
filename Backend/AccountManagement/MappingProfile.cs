using AutoMapper;
using Entities.Dto;
using Entities.DTO;
using Entities.Models;

namespace AccountManagement
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //client mapping
            CreateMap<CreateUserDTO, Client>();
            CreateMap<Client, ClientDetailDTO>();

            //currency mapping
            CreateMap<Currency, CurrencyDTO>().ReverseMap();
            CreateMap<Currency, CurrencyDetailDTO>().ReverseMap();

            //bankAccount mapping
            CreateMap<BankAccount, BankAccountDTO>().ReverseMap();
            CreateMap<BankAccount, BankAccountDetailsDTO>().ReverseMap();
            CreateMap<BankAccount, BankAccountClientDTO>().ReverseMap();

            //bankTransaction mapping
            CreateMap<BankTransaction, BankTransactionDTO>().ReverseMap();
            CreateMap<BankTransaction, BankTransactionDetailsDTO>().ReverseMap();
            CreateMap<BankTransaction, BankTransactionAccountDTO>().ReverseMap();


            //pagination mapping
            CreateMap<BankAccountDTO, PaginationDTO<BankAccountDTO>>().ReverseMap();
            CreateMap<BankAccount, PaginationDTO<BankAccount>>().ReverseMap();


            //category mapping
            CreateMap<CategoryDTO, Category>().ReverseMap();
            CreateMap<Category, CategoryDetailsDTO>().ReverseMap();

            //product mapping
            //CreateMap<Product, ProductDTO>().ForMember(dest =>dest.Image,opt => opt.MapFrom(src => src.ImageUrl)).ReverseMap();
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<ProductDetailsDTO, Product>().ReverseMap();

            //custom mapping
            CreateMap<ClientCustomModel, ClientCustomDTO>().ReverseMap();


        }
    }
}
