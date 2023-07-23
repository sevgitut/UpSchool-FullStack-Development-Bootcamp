using Domain.Dtos;
using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Microsoft.AspNetCore.SignalR;

namespace WebApi.Hubs;

public class ProductHub : Hub
{
    private readonly ApplicationDbContext _dbContext;

    public ProductHub(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> AddProduct(ProductDto productDto, CancellationToken cancellationToken)
    {
        try
        {
            var product = new Product()
            {
                Id = productDto.Id,
                OrderId = productDto.OrderId,
                Name = productDto.Name,
                Picture = productDto.Picture,
                SalePrice = productDto.SalePrice,
                Price = productDto.Price
            };

            await _dbContext.Products.AddAsync(product, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            throw;
        }

        await Clients.AllExcept(Context.ConnectionId).SendAsync("ProductAdd", productDto);

        return true;
    }
}