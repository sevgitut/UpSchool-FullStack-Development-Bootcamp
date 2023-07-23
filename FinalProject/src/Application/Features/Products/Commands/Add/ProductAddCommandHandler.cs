using Application.Common.Interfaces;
using Domain.Common;
using Domain.Entities;
using MediatR;

namespace Application.Features.Products.Commands.Add
{
    public class ProductAddCommandHandler : IRequestHandler<ProductAddCommand, Response<Guid>>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        //private readonly HttpClient _httpClient;

        public ProductAddCommandHandler(IApplicationDbContext applicationDbContext, HttpClient httpClient)
        {
            _applicationDbContext = applicationDbContext;
            //_httpClient = httpClient;
        }

        public async Task<Response<Guid>> Handle(ProductAddCommand request, CancellationToken cancellationToken)
        {
            var product = new Product()
            {
                OrderId = request.OrderId,
                Name = request.Name,
                Picture = request.Picture,
                IsOnSale = request.IsOnSale,
                Price = request.Price,
                SalePrice = request.SalePrice,
                //CreatedOn=DateTimeOffset.Now,
                //IsDeleted=false,
            };

            await _applicationDbContext.Products.AddAsync(product, cancellationToken);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            //var content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
            //var response = await _httpClient.PostAsync("https://example.com/api/users", content);

            return new Response<Guid>($"The new product {product.Name} was successfully added to the db.", product.Id);
        }
    }
}
