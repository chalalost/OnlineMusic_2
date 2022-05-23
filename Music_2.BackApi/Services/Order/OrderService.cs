using Microsoft.EntityFrameworkCore;
using Music_2.Data.EF;
using Music_2.Data.Models.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music_2.BackApi.Services.Order
{
    public class OrderService : IOrderService
    {
        private readonly OnlineMusicDbContext _context;

        public OrderService(OnlineMusicDbContext context)
        {
            _context = context;
        }
        public async Task<List<OrderViewModel>> GetAll()
        {
            var orders = await _context.Orders
                .Select(x => new OrderViewModel()
                {
                    Id = x.Id,
                    OrderDate = x.OrderDate,
                    UserName = x.AppUser.UserName,
                    ShipName = x.ShipName,
                    ShipAddress = x.ShipAddress,
                    ShipEmail = x.ShipEmail,
                    ShipPhoneNumber = x.ShipPhoneNumber,
                    OrderDetails = x.OrderDetails,
                    Status = x.Status
                }).ToListAsync();

            return orders;
        }
    }
}
