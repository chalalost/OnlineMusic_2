using Music_2.Data.Models.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music_2.BackApi.Services.Order
{
    public interface IOrderService
    {
        Task<List<OrderViewModel>> GetAll();
    }
}
