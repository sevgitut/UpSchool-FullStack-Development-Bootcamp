using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Orders.Queries.GetAll
{
    public class OrderGetAllDto
    {
        public Guid Id { get; set; }
        public int? RequestedAmount { get; set; }
        public int? TotalFoundAmount { get; set; }
        public ProductCrawlType ProductCrawlType { get; set; }

        public DateTimeOffset CreatedOn { get; set; }

        public string? CreatedByUserId { get; set; }
    }
}