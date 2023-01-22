using AutoMapper;
using Contracts;
using Entities.Dto;
using Entities.Models;
using Entities.Paging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace AccountManagement.Controllers
{
    [Route("api/product")]
    [ApiController]
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IConfiguration _configuration;


        public ProductController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper, IWebHostEnvironment hostEnvironment, IConfiguration configuration)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _hostEnvironment = hostEnvironment;
            _configuration = configuration;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllProduct([FromQuery] PagingParameter pagingParameter)
        {
            var result = new ResultMessage<PaginationDTO<IEnumerable<ProductDetailsDTO>>>();
            string message = "";
            try
            {
                var data = await _repository.ProductRepository.GetAllProductAsync(pagingParameter);
                var dtoData = _mapper.Map<List<ProductDetailsDTO>>(data.Data);

                var paginationDTO = new PaginationDTO<IEnumerable<ProductDetailsDTO>>
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
        public async Task<IActionResult> GetProductById(int id)
        {
            var result = new ResultMessage<ProductDetailsDTO>();
            string message = "";
            try
            {
                if (id == 0)
                {
                    result.Status = false;
                    result.Message = "Id must be diffrent than 0";
                    return Ok(result);
                }

                var productResult = await _repository.ProductRepository.GetProductByIdAsync(id);

                if (productResult is null)
                {
                    result.Status = false;
                    result.Message = $"Product with id {id} not found!";
                    return Ok(result);
                }
              

                _logger.LogInfo("GetProductByid method is called");

                var productResultDtos = _mapper.Map<ProductDetailsDTO>(productResult);

                result.ResultData = productResultDtos;

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
        public async Task<IActionResult> AddNewProduct([FromForm] ProductDTO productDTO)
        {
            var result = new ResultMessage<ProductDTO>();
            string message = "";
            try
            {
                if (productDTO is null)
                {
                    result.Status = false;
                    result.Message = "Product is empty!";
                    return Ok(result);
                }
              

                var uniqueProduct = await _repository.ProductRepository.GetProductByNameAsync(productDTO.Name, productDTO.Price, productDTO.CategoryId);

                if (uniqueProduct != null)
                {
                    result.Status = false;
                    result.Message = "Product must be unique!";
                    return Ok(result);
                }
               

                Product product = _mapper.Map<Product>(productDTO);
               
                if (productDTO.ImageUpload == null)
                {
                    result.Status = false;
                    result.Message = "Product Image must not be empty!";
                    return Ok(result);
                }
                

                string Name = productDTO.Name + "-" + product.Price + "-" + productDTO.CategoryId;
                string path = Path.Combine(_hostEnvironment.ContentRootPath, "StoreImage/" + Name);

                //using (var stream = new FileStream(path, FileMode.Create))
                //    {
                //       await productDTO.ImageUrl.CopyToAsync(stream);
                // }

                using (var ms = new MemoryStream())
                {
                    productDTO.ImageUpload.CopyTo(ms);
                    product.Base64Image = Convert.ToBase64String(ms.ToArray());
                    //product.Base64Image = ms.ToArray();
                }

                _repository.ProductRepository.CreateProduct(product);
                await _repository.SaveChangesAsync();

                result.ResultData = productDTO;
                result.Status = message.Length == 0;
                result.Message = message;
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("{0}: {1}", nameof(AddNewProduct), ex.Message));

                return BadRequest("Something went wrong, please try again!");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromForm] ProductDTO productUpdateDto)
        {
            var result = new ResultMessage<ProductDTO>();
            string message = "";
            try
            {
                var productResult = await _repository.ProductRepository.GetProductByIdAsync(id);

                if (productResult is null)
                {
                    result.Status = false;
                    result.Message = $"Product with id {id} not found!";
                    return Ok(result);
                }
            

                var uniqueName = await _repository.ProductRepository.GetProductByNameAsync(productUpdateDto.Name, productUpdateDto.Price, productUpdateDto.CategoryId);

                if (uniqueName != null)
                {
                    result.Status = false;
                    result.Message = "Product must be unique!";
                    return Ok(result);
                }
             

                if (productUpdateDto.ImageUpload == null)
                {
                    result.Status = false;
                    result.Message = "Product Image must not be empty!";
                    return Ok(result);
                }
                

                string fName = productUpdateDto.Name;
                string path = Path.Combine(_hostEnvironment.ContentRootPath, "StoreImage/" + fName);

                //if (path != productResult.ImageUrl) {

                //using (var stream = new FileStream(path, FileMode.Create))
                //{

                //    await productUpdateDto.ImageUrl.CopyToAsync(stream);
                //}
                // }

                Product product = _mapper.Map<Product>(productUpdateDto);

                using (var ms = new MemoryStream())
                {
                    productUpdateDto.ImageUpload.CopyTo(ms);
                    product.Base64Image = Convert.ToBase64String(ms.ToArray());
                    //product.Base64Image = ms.ToArray();
                }

                product.Id = productResult.Id;
                product.CategoryId = productResult.CategoryId;
                //product.ImageUrl = path;

                _repository.ProductRepository.UpdateProduct(product);
                await _repository.SaveChangesAsync();

                result.ResultData = productUpdateDto;
                result.Status = message.Length == 0;
                result.Message = message;
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("{0}: {1}", nameof(UpdateProduct), ex.Message));

                return BadRequest("Something went wrong, please try again!");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = new ResultMessage<Product>();
            string message = "";
            try
            {
                var productResult = await _repository.ProductRepository.GetProductByIdAsync(id);

                if (productResult is null)
                {
                    result.Status = false;
                    result.Message = $"Product with id {id} not found!";
                    return Ok(result);
                }
              

                //string path = productResult.ImageUrl;

                //FileInfo file = new FileInfo(path);

                //if (file.Exists)
                //{
                //    file.Delete();
                  
                //}

                _repository.ProductRepository.DeleteProduct(productResult);

                await _repository.SaveChangesAsync();

                result.ResultData = productResult;
                result.Status = message.Length == 0;
                result.Message = message;
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("{0}: {1}", nameof(DeleteProduct), ex.Message));

                return BadRequest("Something went wrong, please try again!");
            }
        }
    }
}