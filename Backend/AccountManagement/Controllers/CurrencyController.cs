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
using System.Threading.Tasks;

namespace AccountManagement.Controllers
{
    [Route("api/currency")]
    [ApiController]
    [Authorize]
    public class CurrencyController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;

        public CurrencyController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllCurrency([FromQuery] PagingParameter pagingParameter)
        {
            var result = new ResultMessage<PaginationDTO<IEnumerable<Currency>>>();
            string message = "";
            try
            {
                var currencyResult = await _repository.CurrencyRepository.GetAllAsync(pagingParameter);
                result.ResultData = currencyResult;

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
        public async Task<IActionResult> GetCurrencyById(int id)
        {
            var result = new ResultMessage<CurrencyDetailDTO>();
            string message = "";
            try
            {
                if (id == 0)
                {
                    result.Status = false;
                    result.Message = "Id must be diffrent than 0";
                    return Ok(result);
                }
               

                var currencyResult = await _repository.CurrencyRepository.GetCurrencyByIdAsync(id);

                if (currencyResult is null)
                {
                    result.Status = false;
                    result.Message = $"Currency with id {id} not found!";
                    return Ok(result);
                }
               

                var currencyResultDtos = _mapper.Map<CurrencyDetailDTO>(currencyResult);
                result.ResultData = currencyResultDtos;

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
        public async Task<IActionResult> AddNewCurrency([FromBody] CurrencyDTO currencyDTO)
        {
            var result = new ResultMessage<CurrencyDTO>();
            string message = "";
            try
            {
                if (currencyDTO is null)
                {
                    result.Status = false;
                    result.Message = "Currency object is empty!";
                    return Ok(result);
                }
              

                Currency currency = _mapper.Map<Currency>(currencyDTO);
                currency.Code = currencyDTO.Code.ToUpper();
                currency.DateCreated = DateTime.Now;

                var uniqueCode = await _repository.CurrencyRepository.GetCurrencyByCodeAsync(currencyDTO.Code);

                if (uniqueCode != null)
                {
                    result.Status = false;
                    result.Message = "Currency code must be unique!";
                    return Ok(result);
                }
          

                _repository.CurrencyRepository.CreateCurrency(currency);
                await _repository.SaveChangesAsync();

                result.ResultData = currencyDTO;
                result.Status = message.Length == 0;
                result.Message = message;
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("{0}: {1}", nameof(AddNewCurrency), ex.Message));

                return BadRequest("Something went wrong, please try again!");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCurrency(int id, [FromBody] CurrencyDTO currencyDTO)
        {
            var result = new ResultMessage<CurrencyDTO>();
            string message = "";
            try
            {
                var currencyResult = await _repository.CurrencyRepository.GetCurrencyByIdAsync(id);

                if (currencyResult == null)
                {
                    result.Status = false;
                    result.Message = $"Currency with id {id} not found!";
                    return Ok(result);
                }
              

                var uniqueCode = await _repository.CurrencyRepository.GetCurrencyByCodeAsync(currencyDTO.Code);

                if (uniqueCode != null)
                {
                    result.Status = false;
                    result.Message = "Currency code must be unique!";
                    return Ok(result);
                }
                

                currencyResult.ExchangeRate = currencyDTO.ExchangeRate;
                currencyResult.Description = currencyDTO.Description;

                if (currencyDTO.Code == null)
                {
                    result.Status = false;
                    result.Message = "Currency code must not be empty!";
                    return Ok(result);
                }
             

                currencyResult.Code = currencyDTO.Code.ToUpper();
                currencyResult.DateModified = DateTime.Now;

                _repository.CurrencyRepository.UpdateCurrency(currencyResult);
                await _repository.SaveChangesAsync();

                result.ResultData = currencyDTO;
                result.Status = message.Length == 0;
                result.Message = message;
                return Ok(result);

            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("{0}: {1}", nameof(UpdateCurrency), ex.Message));

                return BadRequest("Something went wrong, please try again!");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCurrency(int id)
        {
            var result = new ResultMessage<Currency>();
            string message = "";
            try
            {
                var currencyResult = await _repository.CurrencyRepository.GetCurrencyByIdAsync(id);

                if (currencyResult == null)
                {
                    result.Status = false;
                    result.Message = $"Currency with id {id} not found!";
                    return Ok(result);
                }
                


                _repository.CurrencyRepository.DeleteCurrency(currencyResult);
                await _repository.SaveChangesAsync();

                result.ResultData = currencyResult;
                result.Status = message.Length == 0;
                result.Message = message;
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("{0}: {1}", nameof(DeleteCurrency), ex.Message));

                return BadRequest("Something went wrong, please try again!");
            }
        }

    }
}
