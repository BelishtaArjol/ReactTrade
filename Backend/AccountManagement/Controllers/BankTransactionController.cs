using AutoMapper;
using Contracts;
using Entities.Dto;
using Entities.Models;
using Entities.Paging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Action = Entities.Models.Action;

namespace AccountManagement.Controllers
{
    [Route("api/banktransaction")]
    [ApiController]
    [Authorize]
    public class BankTransactionController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;

        public BankTransactionController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllBankTransaction([FromQuery] PagingParameter pagingParameter)
        {
            var result = new ResultMessage<PaginationDTO<IEnumerable<BankTransactionDetailsDTO>>>();
            string message = "";
            try
            { 
            var data = await _repository.BankTransactionRepository.GetAllBankTransactionsAsync(pagingParameter);

            var dtoData = _mapper.Map<List<BankTransactionDetailsDTO>>(data.Data);

            var paginationDTO = new PaginationDTO<IEnumerable<BankTransactionDetailsDTO>>
            {
                Data = dtoData,
                CurrentPage = data.CurrentPage,
                PageSize = data.PageSize,
                TotalCount = data.TotalCount,
                TotalPages = data.TotalPages
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
        public async Task<IActionResult> GetBankTransactionById(int id)
        {
            var result = new ResultMessage<BankTransactionDetailsDTO>();
            string message = "";
            try { 
            if (id == 0)
                {
                    result.Status = false;
                    result.Message = "Id must be diffrent than 0";
                    return Ok(result);
                }
               

            var bankTransactionResult = await _repository.BankTransactionRepository.GetBankTransactionByIdAsync(id);

            if (bankTransactionResult is null)
                {
                    result.Status = false;
                    result.Message = $"BankTransaction with id {id} not found!";
                    return Ok(result);
                }
               

            var bankTransactionResultDtos = _mapper.Map<BankTransactionDetailsDTO>(bankTransactionResult);
            result.ResultData = bankTransactionResultDtos;

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
        public async Task<IActionResult> AddNewBankTransaction([FromBody] BankTransactionDTO bankTransactionDTO)
        {
            var result = new ResultMessage<BankTransactionDTO>();
            string message = "";
            try
            {
                if (bankTransactionDTO is null)
                    return BadRequest("BankAccount is empty!");

                if (bankTransactionDTO.Amount < 0)
                {
                    result.Status = false;
                    result.Message = "BankTransaction must be initialized!";
                    return Ok(result);
                }
              

                var accountBankTransaction = await _repository.BankAccountRepository.GetBankAccountByIdAsync(bankTransactionDTO.BankAccountId);

                if (accountBankTransaction == null)
                {
                    result.Status = false;
                    result.Message = "This Client id does not exists!";
                    return Ok(result);
                }
                

                if (bankTransactionDTO.Action == Action.Deposit)
                {
                    accountBankTransaction.Balance = accountBankTransaction.Balance + bankTransactionDTO.Amount;
                    _repository.BankAccountRepository.UpdateBankAccount(accountBankTransaction);
                    await _repository.SaveChangesAsync();
                }
                else if (bankTransactionDTO.Action == Action.Withdraw)
                {
                    if (accountBankTransaction.Balance == 0)
                        return BadRequest("Your balace is 0. Action failed!");

                    if(bankTransactionDTO.Amount > accountBankTransaction.Balance)
                        return BadRequest("Your balace is lower than amount. Action failed!");

                    accountBankTransaction.Balance = accountBankTransaction.Balance - bankTransactionDTO.Amount;
                    _repository.BankAccountRepository.UpdateBankAccount(accountBankTransaction);
                    await _repository.SaveChangesAsync();
                }
                else
                {
                    {
                        result.Status = false;
                        result.Message = "Action value is not correct!";
                        return Ok(result);
                    }
                   
                }

                BankTransaction bankTransaction = _mapper.Map<BankTransaction>(bankTransactionDTO);
                bankTransaction.Action.Equals(bankTransactionDTO.Action);
                bankTransaction.IsActive = true;
                bankTransaction.DateCreated = DateTime.Now;

                _repository.BankTransactionRepository.CreateBankTransaction(bankTransaction);
                await _repository.SaveChangesAsync();

                result.ResultData = bankTransactionDTO;
                result.Status = message.Length == 0;
                result.Message = message;

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("{0}: {1}", nameof(AddNewBankTransaction), ex.Message));

                return BadRequest("Something went wrong, please try again!");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBankTransaction(int id)
        {
            var result = new ResultMessage<BankTransaction>();
            string message = "";
            try
            {
                var bankTransaction = await _repository.BankTransactionRepository.GetBankTransactionByIdAsync(id);

                if (bankTransaction == null)
                     {
                        result.Status = false;
                        result.Message = $"BankTransaction with id {id} not found!";
                        return Ok(result);
                    }
                  

                var accountBankTransaction = await _repository.BankAccountRepository.GetBankAccountByIdAsync(bankTransaction.BankAccountId);

                if (accountBankTransaction is null)
                {
                    result.Status = false;
                    result.Message = "BankAccount is null. Action Failed!";
                    return Ok(result);
                }
              

                if (bankTransaction.Action == Action.Deposit && accountBankTransaction.Balance == 0)
                {
                    result.Status = false;
                    result.Message = "Not enough Balance. Action Failed!";
                    return Ok(result);
                }
                

                if (bankTransaction.IsActive == false)
                {
                    result.Status = false;
                    result.Message = "This transaction is deleted!";
                    return Ok(result);
                }
                

                if (bankTransaction.Action == Action.Deposit)
                {
                    if (bankTransaction.Amount > accountBankTransaction.Balance)
                    {
                        result.Status = false;
                        result.Message = "Your balace is lower than amount. Action failed!";
                        return Ok(result);
                    }
                   

                    accountBankTransaction.Balance = accountBankTransaction.Balance - bankTransaction.Amount;
                }
                else
                {
                    accountBankTransaction.Balance = accountBankTransaction.Balance + bankTransaction.Amount;
                }

                _repository.BankAccountRepository.UpdateBankAccount(accountBankTransaction);
                await _repository.SaveChangesAsync();


                bankTransaction.DateModified = DateTime.Now;
                bankTransaction.IsActive = false;
                _repository.BankTransactionRepository.UpdateBankTransaction(bankTransaction);
                await _repository.SaveChangesAsync();

                result.ResultData = bankTransaction;
                result.Status = message.Length == 0;
                result.Message = message;

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("{0}: {1}", nameof(DeleteBankTransaction), ex.Message));

                return BadRequest("Something went wrong, please try again!");
            }
        }
    }
}
