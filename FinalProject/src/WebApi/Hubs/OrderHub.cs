using Domain.Dtos;
using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Microsoft.AspNetCore.SignalR;

namespace WebApi.Hubs;

public class OrderHub : Hub
{
    private readonly ApplicationDbContext _dbContext;

    public OrderHub(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> AddOrder(OrderDto orderDto)
    {
        try
        {
            var order = new Order()
            {
                Id = orderDto.Id,
                RequestedAmount = orderDto.RequestedAmount,
                TotalFoundAmount = orderDto.TotalFoundAmount,
                ProductCrawlType = orderDto.ProductCrawlType,
            };

            await _dbContext.Orders.AddAsync(order);

            await _dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        await Clients.AllExcept(Context.ConnectionId).SendAsync("OrderAdd", orderDto);

        return true;
    }

}