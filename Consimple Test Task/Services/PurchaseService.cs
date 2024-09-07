using Consimple_Test_Task.Models;
using Consimple_Test_Task.Models.ResponseModels;
using Consimple_Test_Task.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Consimple_Test_Task.Services
{
    public class PurchaseService : IOrderService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IClientService _clientService;
        private readonly IProductService _productService;

        public PurchaseService(ApplicationDbContext dbContext, IClientService clientService, IProductService productService)
        {
            _dbContext = dbContext;
            _clientService = clientService;
            _productService = productService;
        }

        public async Task<Orders> CreateOrderAsync(int clientId, List<(Guid productId, int quantity)> products)
        {
            var client = await _clientService.GetClientAsync(clientId);
            if (client == null)
            {
                throw new ArgumentException("Client not found");
            }

            int totalPrice = 0;

            var purchase = new Orders
            {
                ClientId = clientId,
                Date = DateTime.Today,
                TotalPrice = 0 
            };

            _dbContext.Orders.Add(purchase);
            await _dbContext.SaveChangesAsync();

            foreach (var (productId, quantity) in products)
            {
                var product = await _productService.GetProductByIdAsync(productId);
                if (product == null)
                {
                    throw new ArgumentException($"Product with ID {productId} not found");
                }

                totalPrice += product.Price * quantity;

                var purchaseProduct = new OrderProduct
                {
                    OrderId = purchase.Id,
                    ProductId = productId,
                    Quantity = quantity
                };

                _dbContext.OrderProduct.Add(purchaseProduct);
            }

            purchase.TotalPrice = totalPrice;
            await _dbContext.SaveChangesAsync();

            return purchase;
        }


        public void DeleteOrder(Guid id)
        {
            var purchase = _dbContext.Orders.Include(p => p.PurchaseProducts)
                                               .FirstOrDefault(x => x.Id == id);
            if (purchase == null)
            {
                throw new ArgumentException("Order not found");
            }

            _dbContext.OrderProduct.RemoveRange(purchase.PurchaseProducts);
            _dbContext.Orders.Remove(purchase);
            _dbContext.SaveChanges();
        }
    }
}
