using Microsoft.AspNetCore.Http;

namespace Entities.Dto
{
    public class ProductDTO
    {
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public int CategoryId { get; set; }
        public decimal Price { get; set; }
        public IFormFile ImageUpload { get; set; }
   
    }
}