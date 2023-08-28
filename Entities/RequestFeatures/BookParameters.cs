namespace Entities.RequestFeatures
{
    public class BookParameters : RequestParameters 
    {
        public uint MinPrice { get; set; }
        public uint MaxPrice { get; set; } = uint.MaxValue;
        public bool IsValidPriceRange => MaxPrice > MinPrice;
    }

}
