using Consimple_Test_Task.Models;
using System;
using System.Collections.Generic;

namespace Consimple_Test_Task.Services.Interfaces
{
    public interface IOrderService
    {
        Task<Orders> CreateOrderAsync(int clientId, List<(Guid productId, int quantity)> products);
        void DeleteOrder(Guid id);
    }

}
