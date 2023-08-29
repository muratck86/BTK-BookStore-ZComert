namespace Entities.RequestFeatures
{
    public class BookParameters : RequestParameters 
    {
        public uint MinPrice { get; set; } = 10;
        public uint MaxPrice { get; set; } = 10000;
        public bool IsValidPriceRange => MaxPrice > MinPrice && MaxPrice <= 10000 && MinPrice >= 10;
        public String? SearchTerm { get; set; }
        
        public BookParameters()
        {
            OrderBy = "Id";
        }
    }

}
