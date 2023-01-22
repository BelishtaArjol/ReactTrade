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

namespace AccountManagement.Controllers
{
    [Route("api/category")]
    [ApiController]
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;

        public CategoryController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllCategory([FromQuery] PagingParameter pagingParameter)
        {
            var result = new ResultMessage<PaginationDTO<IEnumerable<CategoryDetailsDTO>>>();
            string message = "";
            try
            {
                var data = await _repository.CategoryRepository.GetAllCategoryAsync(pagingParameter);
                var dtoData = _mapper.Map<List<CategoryDetailsDTO>>(data.Data);

                var paginationDTO = new PaginationDTO<IEnumerable<CategoryDetailsDTO>>
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
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var result = new ResultMessage<CategoryDetailsDTO>();
            string message = "";
            try
            {
                if (id == 0)
                {
                    result.Status = false;
                    result.Message = "Id must be diffrent than 0";
                    return Ok(result);
                }

                var categoryResult = await _repository.CategoryRepository.GetCategoryByIdAsync(id);

                if (categoryResult is null)
                {
                    result.Status = false;
                    result.Message = $"Category with id {id} not found!";
                    return Ok(result);
                }
                
                _logger.LogInfo("GetCategoryByid method is called");

                var categoryResultDtos = _mapper.Map<CategoryDetailsDTO>(categoryResult);

                result.ResultData = categoryResultDtos;
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
        public async Task<IActionResult> AddNewCategory([FromBody] CategoryDTO categoryDTO)
        {
            var result = new ResultMessage<CategoryDTO>();
            string message = "";
            try
            {
                if (categoryDTO is null)
                {
                    result.Status = false;
                    result.Message = "Category is empty!";
                    return Ok(result);
                }
              

                var uniqueCode = await _repository.CategoryRepository.GetCategoryByCodeAsync(categoryDTO.Code);

                if (uniqueCode != null)
                {
                    result.Status = false;
                    result.Message = "Category code must be unique!";
                    return Ok(result);
                }
              


                Category category = _mapper.Map<Category>(categoryDTO);
                category.DateCreated = DateTime.Now;

                _repository.CategoryRepository.CreateCategory(category);
                await _repository.SaveChangesAsync();

                result.ResultData = categoryDTO;
                result.Status = message.Length == 0;
                result.Message = message;
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("{0}: {1}", nameof(AddNewCategory), ex.Message));

                return BadRequest("Something went wrong, please try again!");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryDTO categoryDTO)
        {
            var result = new ResultMessage<CategoryDTO>();
            string message = "";
            try
            {
                var categoryResult = await _repository.CategoryRepository.GetCategoryByIdAsync(id);

                if (categoryResult is null)
                {
                    result.Status = false;
                    result.Message = $"Category with id {id} not found!";
                    return Ok(result);
                }
               

                var codeUnique = await _repository.CategoryRepository.GetCategoryByCodeAsync(categoryDTO.Code);

                if (codeUnique != null)
                {
                    result.Status = false;
                    result.Message = "Category code must be unique!";
                    return Ok(result);
                }
               


                Category category = _mapper.Map<Category>(categoryDTO);
                category.Id = categoryResult.Id;
                category.DateModified = DateTime.Now;
                category.DateCreated = categoryResult.DateCreated;

                _repository.CategoryRepository.UpdateCategory(category);
                await _repository.SaveChangesAsync();

                result.ResultData = categoryDTO;
                result.Status = message.Length == 0;
                result.Message = message;
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("{0}: {1}", nameof(UpdateCategory), ex.Message));

                return BadRequest("Something went wrong, please try again!");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var result = new ResultMessage<Category>();
            string message = "";
            try
            {
                var categoryResult = await _repository.CategoryRepository.GetCategoryByIdAsync(id);

                if (categoryResult is null)
                {
                    result.Status = false;
                    result.Message = $"Category with id {id} not found!";
                    return Ok(result);
                }
               

                var productResult = await _repository.ProductRepository.FindCategoryforProductAsync(id);

                if (productResult != null)
                {
                    result.Status = false;
                    result.Message = $"Category with id {id} is joined with other products!";
                    return Ok(result);
                }
                

                _repository.CategoryRepository.DeleteCategory(categoryResult);

                await _repository.SaveChangesAsync();

                result.ResultData = categoryResult;
                result.Status = message.Length == 0;
                result.Message = message;
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("{0}: {1}", nameof(DeleteCategory), ex.Message));

                return BadRequest("Something went wrong, please try again!");
            }
        }
    }
}
