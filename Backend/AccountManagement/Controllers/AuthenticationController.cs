using AccountManagement.Utility;
using AutoMapper;
using Contracts;
using Entities.DTO;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace AccountManagement.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;
        private readonly IConfiguration _configuration;

        public AuthenticationController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper, IConfiguration configuration)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserDTO createUserDTO)
        {
            var result = new ResultMessage<CreateUserDTO>();
            string message = "";
            try
            {
                if (createUserDTO is null)
                    return BadRequest("User to insert can't be empty");



                AuthenticationManager.CreatePasswordHash(createUserDTO.Password, out byte[] passwordHash, out byte[] passwordSalt);

                Client client = _mapper.Map<Client>(createUserDTO);

                client.DateCreated = DateTime.Now;
                client.PasswordHash = passwordHash;
                client.PasswordSalt = passwordSalt;

                _repository.ClientRepository.CreateClient(client);
                await _repository.SaveChangesAsync();



                result.ResultData = createUserDTO;
                result.Status = message.Length == 0;
                result.Message = message;

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("{0}: {1}", nameof(Register), ex.Message));

                return BadRequest("Something went wrong, please try again!");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO loginUserDTO)
        {
            var result = new ResultMessage<LoginUserDTO>();


            var userResult = await _repository.ClientRepository.GetClientByUserNameAsync(loginUserDTO.UserName);


            if (userResult.Username == null)
            {
                result.Status = false;
                result.Message = $"User with username {loginUserDTO.UserName} not found!";
                return Ok(result);
            }

            if (!AuthenticationManager.VerifyPasswordHash(loginUserDTO.Password, userResult.PasswordHash, userResult.PasswordSalt))
            {
                _logger.LogWarn(string.Format("{0}: {1}", nameof(Login), "Authentication failed. Wrong user name or password."));
                result.Status = false;
                result.Message = $"Authentication failed. Wrong username or password!";
                return Ok(result);
            }

            var user = _mapper.Map<ClientDetailDTO>(userResult);

            string configureToken = _configuration.GetSection("AppSettings:Token").Value;

            string token = AuthenticationManager.CreateToken(user.Id, user.Username, user.Email, configureToken);


            return Ok(new { Token = token, user });
        }

        [HttpGet("validate-token")]
        public async Task<IActionResult> ValidateToken([FromHeader] string token)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(token))
                    return BadRequest("Invalid token!");

                int userId = AuthenticationManager.ValidateToken(token);
                if (userId == -1)
                    return BadRequest("Invalid token!");

                var user = await _repository.ClientRepository.GetClientByIdAsync(userId);

                if (user == null)
                    return NotFound($"Not found user with id: {userId}");

                var clientDto = _mapper.Map<ClientDetailDTO>(user);

                return Ok(clientDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("{0}: {1}", nameof(ValidateToken), ex.Message));

                return BadRequest("Something went wrong, please try again!");
            }
        }
    }
}
