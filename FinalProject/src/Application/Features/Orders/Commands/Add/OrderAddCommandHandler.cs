using Application.Common.Interfaces;
using Domain.Common;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Orders.Commands.Add
{
    public class OrderAddCommandHandler : IRequestHandler<OrderAddCommand, Response<Guid>>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public OrderAddCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Response<Guid>> Handle(OrderAddCommand request, CancellationToken cancellationToken)
        {
            var order = new Order()
            {
                Id = request.Id,
                RequestedAmount = request.RequestedAmount,
                TotalFoundAmount = request.TotalFoundAmount,
                ProductCrawlType = request.ProductCrawlType,
                UserId = null,
                CreatedOn = DateTimeOffset.Now,
                
            };

            await _applicationDbContext.Orders.AddAsync(order, cancellationToken);

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return new Response<Guid>($"Get {order.ProductCrawlType} from databese", order.Id);

        }
    }
}