using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Expenses.Core.ApplicationService;
using Expenses.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Expenses.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductBrandsController : ControllerBase
    {
        private readonly IProductBrandService _productBrandService;

        public ProductBrandsController(IProductBrandService productBrandService)
        {
            _productBrandService = productBrandService;
        }

        // GET api/productBrands
        [HttpGet]
        public ActionResult<IEnumerable<ProductBrand>> Get()
        {
            return _productBrandService.GetAllProductBrands();
        }

        // GET api/productBrands/5
        [HttpGet("{id}")]
        public ActionResult<ProductBrand> Get(int id)
        {
            return _productBrandService.FindProductBrandById(id);
        }

        // POST api/productBrands
        [HttpPost]
        public ActionResult<ProductBrand> Post([FromBody] ProductBrand productBrand)
        {
            try
            {
                return Ok( _productBrandService.SaveProductBrand(productBrand));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        // PUT api/productBrands/5
        [HttpPut("{id}")]
        public ActionResult<ProductBrand> Put(int id, [FromBody] ProductBrand productBrand)
        {
            //El BadRequest debe validarse en el controlador para no llegar más lejos
            if (id == 0 || id != productBrand.Id)
            {
                return BadRequest("Parameter Id and ProductBrandId must be the same");
            }
            return Ok(_productBrandService.UpdateProductBrand(productBrand));
        }

        // DELETE api/productBrands/5
        [HttpDelete("{id}")]
        public ActionResult<ProductBrand> Delete(int id)
        {
            ProductBrand p = _productBrandService.DeleteProductBrand(id);
            if (p == null)
            {
                return StatusCode(404, $"ProductBrand not found with id {id}");
            }
            return Ok($"ProductBrand with id {id} deleted");
        }
    }
}
