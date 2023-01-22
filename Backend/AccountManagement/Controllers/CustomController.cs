using AccountManagement.CustomQuery;
using AutoMapper;
using Contracts;
using Entities.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountManagement.Controllers
{
    //[Route("api/custom")]
    //[ApiController]
    //public class CustomController : Controller
    //{
    //    private readonly IRepositoryManager _repository;
    //    private readonly IMapper _mapper;
    //    private readonly ILoggerManager _logger;
    //    private readonly IConfiguration _configuration;

    //    public CustomController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper, IConfiguration configuration)
    //    {
    //        _repository = repository;
    //        _mapper = mapper;
    //        _logger = logger;
    //        _configuration = configuration;

    //    }

    //    [HttpGet("/clientAccunt")]
    //    public async Task<IActionResult> GetClientAccount()
    //    {

    //        var resultClientAccount = await ClientBalanceCustom.GetClientBalance();

    //        if (resultClientAccount is null)
    //            return NotFound("No clients found");

    //        var clientDtos = _mapper.Map<List<ClientCustomDTO>>(resultClientAccount);


    //        return Ok(clientDtos);
    //    }

    //    [HttpGet("transactionByAccountId")]
    //    public async Task<IActionResult> GetAllTranscationByAccountId(int id)
    //    {
    //        var resultAccount = await ClientBalanceCustom.GetTransactionsByAccountId(id);

    //        if (resultAccount is null)
    //            return NotFound("No clients found");

    //        //var clientDtos = _mapper.Map<List<ClientCustomDTO>>(resultClientAccount);

    //        return Ok(resultAccount);
    //    }

    //    [HttpGet("accountByClientId")]
    //    public async Task<IActionResult> GetAccountByClientId(int id)
    //    {
    //        var resultAccount = await ClientBalanceCustom.GetAccountByClientId(id);

    //        if (resultAccount is null)
    //            return NotFound("No clients found");

    //        //var clientDtos = _mapper.Map<List<ClientCustomDTO>>(resultClientAccount);

    //        return Ok(resultAccount);
    //    }

    //}
}
