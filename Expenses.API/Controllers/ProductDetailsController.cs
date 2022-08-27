using System;
using System.Collections.Generic;
using Expenses.Core.ApplicationService;
using Expenses.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Expenses.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductDetailsController : ControllerBase
    {
        private readonly IProductDetailsService _productDetailsService;

        public ProductDetailsController(IProductDetailsService productDetailsService)
        {
            _productDetailsService = productDetailsService;
        }

        // GET api/productBrands
        [HttpGet]
        public ActionResult<IEnumerable<ProductDetails>> Get([FromQuery] Filter filter)
        {
            //try
            //{
            //    return Ok(_productBrandService.GetFilteredProductBrands(filter));
            //}
            //catch (Exception ex)
            //{
            //    return BadRequest(ex.Message);
            //}

            //return _productBrandService.GetAllProductBrands();
            return null;
        }

        // GET api/productBrands/5
        [HttpGet("{id}")]
        public ActionResult<ProductDetails> Get(int id)
        {
            //return _productBrandService.FindProductBrandById(id);
            return null;
        }

        // POST api/productBrands
        [HttpPost]
        public ActionResult<ProductDetails> Post([FromBody] ProductDetails productBrand)
        {
            //try
            //{
            //    return Ok( _productBrandService.SaveProductBrand(productBrand));
            //}
            //catch (Exception e)
            //{
            //    return BadRequest(e.Message);
            //}
            return null;
        }

        // PUT api/productBrands/5
        [HttpPut("{id}")]
        public ActionResult<ProductDetails> Put(int id, [FromBody] ProductDetails productBrand)
        {
            //El BadRequest debe validarse en el controlador para no llegar más lejos
            //if (id == 0 || id != productBrand.Id)
            //{
            //    return BadRequest("Parameter Id and ProductBrandId must be the same");
            //}
            //return Ok(_productBrandService.UpdateProductBrand(productBrand));
            return null;
        }

        // DELETE api/productBrands/5
        [HttpDelete("{id}")]
        public ActionResult<ProductDetails> Delete(int id)
        {
            //ProductDetails p = _productBrandService.DeleteProductBrand(id);
            //if (p == null)
            //{
            //    return StatusCode(404, $"ProductBrand not found with id {id}");
            //}
            //return Ok($"ProductBrand with id {id} deleted");
            return null;
        }
    }
}
