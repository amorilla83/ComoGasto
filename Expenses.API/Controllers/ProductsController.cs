using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Expenses.Core.ApplicationService;
using Expenses.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Expenses.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController (IProductService productService)
        {
            _productService = productService;
        }

        // GET api/products
        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {
            return _productService.GetAllProducts();
        }

        // GET api/products/5
        [HttpGet("{id}")]
        public ActionResult<Product> Get(int id)
        {
            return _productService.FindProductByIdIncludeBrands(id);
        }

        // POST api/products
        [HttpPost]
        public ActionResult<Product> Post([FromBody] Product product)
        {
            if (string.IsNullOrEmpty(product.Name))
            {
                return BadRequest("Name is required");
            }

            return _productService.SaveProduct(product);            
        }

        // PUT api/products/5
        [HttpPut("{id}")]
        public ActionResult<Product> Put(int id, [FromBody] Product product)
        {
            //El BadRequest debe validarse en el controlador para no llegar más lejos
            if (id == 0 || id != product.Id)
            {
                return BadRequest("Parameter Id and ProductId must be the same");
            }
            return Ok(_productService.UpdateProduct(product));
        }

        // DELETE api/products/5
        [HttpDelete("{id}")]
        public ActionResult<Product> Delete(int id)
        {
            Product p = _productService.DeleteProduct(id);
            if (p == null)
            {
                return StatusCode(404, $"Product not found with id {id}");
            }
            return Ok($"Product with id {id} deleted");
        }
    }
}
