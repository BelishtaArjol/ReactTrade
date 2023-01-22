namespace Entities.Dto
{
    public class ProductDetailsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public int CategoryId { get; set; }
        public decimal Price { get; set; }
        public string Base64Image { get; set; }
    }
}
