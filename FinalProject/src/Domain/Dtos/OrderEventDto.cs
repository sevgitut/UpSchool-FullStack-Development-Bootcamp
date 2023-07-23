using Domain.Entities;
using Domain.Enums;

namespace Domain.Dtos
{
    public class OrderEventDto
    {
        public Guid OrderId { get; set; }

        public Order Order { get; set; }

        public OrderStatus Status { get; set; }

        public OrderEventDto()
        {
            
        }
    }
}
