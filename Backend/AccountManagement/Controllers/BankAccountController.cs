using AutoMapper;
using Contracts;
using Entities.Dto;
using Entities.DTO;
using Entities.Models;
using Entities.Paging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountManagement.Controllers
{
    [Route("api/bankaccount")]
    [ApiController]
    [Authorize]
    public class BankAccountController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;

        public BankAccountController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllBankAccount([FromQuery] PagingParameter pagingParameter)
        {
            var result = new ResultMessage<PaginationDTO<IEnumerable<BankAccountDetailsDTO>>>();
            string message = "";
            try
            {
                var data = await _repository.BankAccountRepository.GetAllBankAccountsAsync(pagingParameter);
                var dtoData = _mapper.Map<List<BankAccountDetailsDTO>>(data.Data);

                var paginationDTO = new PaginationDTO<IEnumerable<BankAccountDetailsDTO>>
                {
                    Data = dtoData,
                    CurrentPage = data.CurrentPage,
                    PageSize = data.PageSize,
                    TotalCount = data.TotalCount,
                    TotalPages = data.TotalPages
                };

                result.ResultData = paginationDTO;
            }
            catch(Exception ex)
            {
                message = ex.Message;
            }
            result.Status = message.Length == 0;
            result.Message = message;
            return Ok(result);
        }

        [HttpGet("{id}/transactions")]
        public async Task<IActionResult> GetAllBankAccountTransaction(int id, [FromQuery] PagingParameter pagingParameter)
        {
            var result = new ResultMessage<PaginationDTO<IEnumerable<BankTransactionAccountDTO>>>();
            string message = "";
            try { 
            if (HttpContext.User.Claims.First(c => c.Type == "UserId").Value == null)
                {   result.Status = false;
                    result.Message = "Please Log In to procced the action.";
                    return Ok(result);
                }
                

            int clientId = Convert.ToInt32(HttpContext.User.Claims.First(c => c.Type == "UserId").Value);

            var bankAccountResult = await _repository.BankTransactionRepository.GetAllBankAccountsTransactionAsync(pagingParameter, id, clientId);

            if (bankAccountResult.Data.Count() == 0)
                {
                    result.Status = false;
                    result.Message = "No bank Accounts found for this client.";
                    return Ok(result);
                }
            
            var bankAccountDtos = _mapper.Map<List<BankTransactionAccountDTO>>(bankAccountResult.Data);

            var paginationDTO = new PaginationDTO<IEnumerable<BankTransactionAccountDTO>>
            {
                Data = bankAccountDtos,
                CurrentPage = bankAccountResult.CurrentPage,
                PageSize = bankAccountResult.PageSize,
                TotalCount = bankAccountResult.TotalCount,
                TotalPages = bankAccountResult.TotalPages
            };
            result.ResultData = paginationDTO;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                
            }
            result.Status = message.Length == 0;
            result.Message = message;

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBankAccountById(int id)
        {
            var result = new ResultMessage<BankAccountDetailsDTO>();
            string message = "";
            try { 
            if (id == 0)
                {
                    result.Status = false;
                    result.Message = "Id must be diffrent than 0";
                    return Ok(result);
                }
              

                var bankAccountResult = await _repository.BankAccountRepository.GetBankAccountByIdAsync(id);

            if (bankAccountResult is null)
                {
                    result.Status = false;
                    result.Message = $"BankAccount with id {id} not found!";
                    return Ok(result);
                }
               

             _logger.LogInfo("GetBankAccountByid method is called");

            var bankAccountResultDtos = _mapper.Map<BankAccountDetailsDTO>(bankAccountResult);
            result.ResultData = bankAccountResultDtos;
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            result.Status = message.Length == 0;
            result.Message = message;
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewBankAccount([FromBody] BankAccountDTO bankAccountDTO)
        {
            var result = new ResultMessage<BankAccountDTO>();
            string message = "";
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                int clientId = Convert.ToInt32(HttpContext.User.Claims.First(c => c.Type == "UserId").Value);
                var uniqueCode = await _repository.BankAccountRepository.GetBankAccountByCodeAsync(bankAccountDTO.Code, clientId);

                if (uniqueCode != null)
                {
                    result.Status = false;
                    result.Message = "BankAccount code must be unique!";
                    return Ok(result);
                }

                if (bankAccountDTO.Balance < 0)
                {
                    result.Status = false;
                    result.Message = "Bank Account must be initialized!";
                    return Ok(result);
                }

                var clientBankAccount = await _repository.ClientRepository.GetClientByIdAsync(clientId);

                if (clientBankAccount is null)
                    return NotFound($"Client with id {bankAccountDTO.ClientId} not found!");

                var currencyBankAccount = await _repository.CurrencyRepository.GetCurrencyByIdAsync(bankAccountDTO.CurrencyId);

                if (currencyBankAccount == null)
                {
                    result.Status = false;
                    result.Message = $"Currency with id {bankAccountDTO.CurrencyId} not found!";
                    return Ok(result);
                }

                BankAccount bankAccount = _mapper.Map<BankAccount>(bankAccountDTO);
                bankAccount.DateCreated = DateTime.Now;
                bankAccount.ClientId = clientId;
                bankAccount.IsActive = true;

                result.ResultData = bankAccountDTO;

                _repository.BankAccountRepository.CreateBankAccount(bankAccount);
                await _repository.SaveChangesAsync();

                result.Status = message.Length == 0;
                result.Message = message;

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("{0}: {1}", nameof(AddNewBankAccount), ex.Message));

                return BadRequest("Something went wrong, please try again!");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBankAccount(int id, [FromBody] BankAccountDTO bankAccountDTO)
        {
            var result = new ResultMessage<BankAccountDTO>();
            string message = "";
            try
            {
                var bankAccountResult = await _repository.BankAccountRepository.GetBankAccountByIdAsync(id);

                if (bankAccountResult is null)
                {
                    result.Status = false;
                    result.Message = $"BankAccount with id {id} not found!";
                    return Ok(result);
                }

                if (bankAccountDTO.Balance < 0 || bankAccountDTO.Balance ==0)
                {
                    result.Status = false;
                    result.Message = "BankAccount must be initialized!";
                    return Ok(result);
                }

                int clientId = Convert.ToInt32(HttpContext.User.Claims.First(c => c.Type == "UserId").Value);

                var clientBankAccount = await _repository.ClientRepository.GetClientByIdAsync(clientId);

                if (clientBankAccount == null)
                {
                    result.Status = false;
                    result.Message = "This Client id does not exists!";
                    return Ok(result);
                }

                var currencyBankAccount = await _repository.CurrencyRepository.GetCurrencyByIdAsync(bankAccountDTO.CurrencyId);

                if (currencyBankAccount == null)
                {
                    result.Status = false;
                    result.Message = "This Currency id does not exists!";
                    return Ok(result);
                }

                BankAccount bank = _mapper.Map<BankAccount>(bankAccountDTO);
                bank.ClientId = clientId;
                bank.CurrencyId = currencyBankAccount.Id;
                bank.Id = bankAccountResult.Id;
                bank.DateModified = DateTime.Now;
                bank.DateCreated = bankAccountResult.DateCreated;

                result.ResultData = bankAccountDTO;
                _repository.BankAccountRepository.UpdateBankAccount(bank);
                await _repository.SaveChangesAsync();

                result.Status = message.Length == 0;
                result.Message = message;

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("{0}: {1}", nameof(UpdateBankAccount), ex.Message));

                return BadRequest("Something went wrong, please try again!");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBankAccount(int id)
        {
            var result = new ResultMessage<BankAccount>();
            string message = "";
            try
            {
                var bankAccount = await _repository.BankAccountRepository.GetBankAccountByIdAsync(id);

                if (bankAccount is null)
                {
                    result.Status = false;
                    result.Message = $"BankAccount with id {id} not found!";
                    return Ok(result);
                }

                if (bankAccount.Balance != 0)
                {
                    result.Status = false;
                    result.Message = "Make sure your bank account balance is 0";
                    return Ok(result);
                }

                bankAccount.DateModified = DateTime.Now;
                bankAccount.IsActive = false;
                result.ResultData = bankAccount;
                _repository.BankAccountRepository.UpdateBankAccount(bankAccount);

                await _repository.SaveChangesAsync();

                result.Status = message.Length == 0;
                result.Message = message;

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("{0}: {1}", nameof(DeleteBankAccount), ex.Message));

                return BadRequest("Something went wrong, please try again!");
            }
        }
    }
}
