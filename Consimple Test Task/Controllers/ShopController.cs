using Consimple_Test_Task.Models;
using Consimple_Test_Task.Models.ResponseModels;
using Consimple_Test_Task.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Consimple_Test_Task.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ShopController : ControllerBase
    {
        private readonly IClientService _clientService;
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;

        public ShopController(IClientService clientService, IProductService productService, IOrderService orderService)
        {
            _clientService = clientService;
            _productService = productService;
            _orderService = orderService;
        }

        #region Client

        [HttpPost]
        public async Task<ActionResult<Client>> AddClient(Client model)
        {
            var client = await _clientService.CreateClientAsync(model);
            return CreatedAtAction(nameof(GetClient), new { id = client.Id }, client);
        }

        [HttpPut]
        public async Task<ActionResult<Client>> UpdateClient(Client model)
        {
            var client = await _clientService.UpdateClientAsync(model);
            if (client == null)
                return NotFound();

            return Ok(client);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetClient(int id)
        {
            var client = await _clientService.GetClientAsync(id);
            if (client == null)
                return NotFound();

            return Ok(client);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteClient(int id)
        {
            await _clientService.DeleteClientAsync(id);
            return NoContent();
        }

        [HttpGet("birthdate/{dateOfBirth}")]
        public async Task<ActionResult<IEnumerable<ClientDataResponseModel>>> GetClientsByBirthDay(DateTime dateOfBirth)
        {
            var clients = await _clientService.FindClientsByDateOfBirthAsync(dateOfBirth);
            return Ok(clients);
        }

        #endregion

        #region Product

        [HttpPost]
        public async Task<ActionResult<Product>> AddProduct(Product model)
        {
            var product = await _productService.CreateProductAsync(model);
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        [HttpPut]
        public async Task<ActionResult<Product>> UpdateProduct(Product model)
        {
            var product = await _productService.UpdateProductAsync(model);
            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(Guid id)
        {
            await _productService.DeleteProductAsync(id);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(Guid id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
                return NotFound();

            return Ok(product);
        }

        #endregion

        #region Purchase

        [HttpPost]
        public async Task<ActionResult<Orders>> AddPurchase(int clientId, List<Product> listOfProducts)
        {
            var productsWithQuantity = listOfProducts.Select(product => (product.Id, 1)).ToList();
            var purchase = await _orderService.CreateOrderAsync(clientId, productsWithQuantity);
            return CreatedAtAction(nameof(DeletePurchase), new { id = purchase.Id }, purchase);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePurchase(Guid id)
        {
             _orderService.DeleteOrder(id);
            return NoContent();
        }

        #endregion
    }
}
