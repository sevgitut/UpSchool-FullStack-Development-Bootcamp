using Domain.Enums;

namespace Domain.Dtos
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        
        public int RequestedAmount { get; set; }
        public int TotalFoundAmount { get; set; }
        public ProductCrawlType ProductCrawlType { get; set; }

        public OrderDto(Guid id, int requestedAmount, int totalFoundAmount, ProductCrawlType productCrawlType)
        {
            Id = id;
            RequestedAmount = requestedAmount;
            TotalFoundAmount = totalFoundAmount;
            ProductCrawlType = productCrawlType;
        }
    }
}
