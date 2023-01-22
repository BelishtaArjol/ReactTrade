using AutoMapper;
using Contracts;
using Entities.Dto;
using Entities.DTO;
using Entities.Models;
using Entities.Paging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AccountManagement.Controllers
{
    [Route("api/client")]
    [ApiController]
    [Authorize]
    public class ClientController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;
        private readonly IConfiguration _configuration;

        public ClientController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper, IConfiguration configuration)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = new ResultMessage<List<ClientDetailDTO>>();
            string message = "";
            try
            {
                var clients = await _repository.ClientRepository.GetClientsAsync();

                if (clients.Count() == 0)
                {
                    result.Status = false;
                    result.Message = "No clients found";
                    return Ok(result);
                }
              

                var clientDtos = _mapper.Map<List<ClientDetailDTO>>(clients);
                result.ResultData = clientDtos;
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            result.Status = message.Length == 0;
            result.Message = message;
            return Ok(result);
        }

        [HttpGet("{id}/bankAccount")]
        public async Task<IActionResult> GetAllClientBankAccount([FromQuery] PagingParameter pagingParameter)
        {
            var result = new ResultMessage<PaginationDTO<IEnumerable<BankAccountClientDTO>>>();
            string message = "";
            try { 

            if (HttpContext.User.Claims.First(c => c.Type == "UserId").Value == null)
                {
                    result.Status = false;
                    result.Message = "Please Log In to procced the action.";
                    return Ok(result);
                }
               

            int clientId = Convert.ToInt32(HttpContext.User.Claims.First(c => c.Type == "UserId").Value);

            var clientResult = await _repository.BankAccountRepository.GetAllBankAccountsClientsAsync(pagingParameter, clientId);

            if (clientResult is null)
                {
                    result.Status = false;
                    result.Message = "No bank Accounts found for this client.";
                    return Ok(result);
                }
                
            var bankAccountDto = _mapper.Map<List<BankAccountClientDTO>>(clientResult.Data);

            var paginationDTO = new PaginationDTO<IEnumerable<BankAccountClientDTO>>
            {
                Data = bankAccountDto,
                CurrentPage = clientResult.CurrentPage,
                PageSize = clientResult.PageSize,
                TotalCount = clientResult.TotalCount,
                TotalPages = clientResult.TotalPages
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
        public async Task<IActionResult> GetUserById(int id)
        {
            var result = new ResultMessage<ClientDetailDTO>();
            string message = "";
            try
            {
                var client = await _repository.ClientRepository.GetClientByIdAsync(id);

                if (client is null)
                {
                    result.Status = false;
                    result.Message = $"User with id {id} not found!";
                    return Ok(result);
                }
              

                var clientDto = _mapper.Map<ClientDetailDTO>(client);

                result.ResultData = clientDto;
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            result.Status = message.Length == 0;
            result.Message = message;
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateClientDTO updateClientDTO)
        {
            var result = new ResultMessage<UpdateClientDTO>();
            string message = "";
            try
            {
                if (updateClientDTO is null)
                {
                    result.Status = false;
                    result.Message = "Make sure you have entered all the fields.";
                    return Ok(result);
                }

                var existingClient = await _repository.ClientRepository.GetClientByIdAsync(id);
                if (existingClient is null)
                {
                    result.Status = false;
                    result.Message = $"User with email {updateClientDTO.Email} not found!";
                    return Ok(result);
                }

                var email = await _repository.ClientRepository.GetClientByEmailAsync(updateClientDTO.Email);
                if (email != null && email.Email != updateClientDTO.Email)
                {
                    result.Status = false;
                    result.Message = "This Email exists. Please try another Email Address!";
                    return Ok(result);
                }

                var phone = await _repository.ClientRepository.GetClientByPhonesync(updateClientDTO.Phone);
                if (phone != null && phone.Phone != updateClientDTO.Phone)
                {
                    result.Status = false;
                    result.Message = "This Phone number exists. Please try another Phone Number!";
                    return Ok(result);
                }
             

                existingClient.FirstName = updateClientDTO.FirstName;
                existingClient.LastName = updateClientDTO.LastName;
                if (new EmailAddressAttribute().IsValid(updateClientDTO.Email))
                {
                    existingClient.Email = updateClientDTO.Email;
                }
                else {
                    result.Status = false;
                    result.Message = "Email address is invalid!";
                    return Ok(result);
                    }


                existingClient.Birthdate = updateClientDTO.Birthdate;
                existingClient.Phone = updateClientDTO.Phone;
                existingClient.DateModified = DateTime.Now;

                _repository.ClientRepository.UpdateClient(existingClient);
                await _repository.SaveChangesAsync();

                result.ResultData = updateClientDTO;
                result.Status = message.Length == 0;
                result.Message = message;
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("{0}: {1}", nameof(UpdateUser), ex.Message));

                return BadRequest("Something went wrong, please try again!");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = new ResultMessage<Client>();
            string message = "";
            try
            {
                var client = await _repository.ClientRepository.GetClientByIdAsync(id);

                if (client is null)
                {
                    result.Status = false;
                    result.Message = $"User with id {id} not found!";
                    return Ok(result);
                }


           
                _repository.ClientRepository.DeleteClient(client);
                await _repository.SaveChangesAsync();

                result.ResultData = client;
                result.Status = message.Length == 0;
                result.Message = message;
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("{0}: {1}", nameof(DeleteUser), ex.Message));

                return BadRequest("Something went wrong, please try again!");
            }
        }
    }
}