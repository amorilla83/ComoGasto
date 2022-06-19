using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Expenses.API.Models;
using Expenses.API.Models.Brands;
using Expenses.Core.ApplicationService;
using Expenses.Core.Entities;
using Expenses.Core.Entities.Communication;
using Expenses.Core.Entities.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Expenses.API.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IBrandService _brandService;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController (IProductService productService,
            IBrandService brandService,
            IMapper mapper, ILogger<ProductsController> logger)
        {
            _productService = productService;
            _brandService = brandService;
            _mapper = mapper;
            _logger = logger;
        }

        // GET api/products
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProductModel>), 200)]
        public async Task<IEnumerable<ProductModel>> ListAsync()
        {
            var products = await _productService.GetAllProductsAsync();

            _logger.LogInformation(AppLoggingEvents.Read, $"Se han obtenido un total de {products.Count()} products");

            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductModel>>(products);
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(IEnumerable<Product>), 200)]
        [ProducesResponseType(typeof(ErrorModel), 400)]
        public async Task<ProductModel> GetDetailsAsync (int id)
        {
            var details = await _productService.GetProductDetailsAsync(id);

            _logger.LogInformation(AppLoggingEvents.Read, $"Se han obtenido los detalles del producto {id}");

            return _mapper.Map<Product, ProductModel>(details);
        }

        [HttpGet]
        [Route("{id}/Brands")]
        [ProducesResponseType(typeof(IEnumerable<BrandModel>), 200)]
        [ProducesResponseType(typeof(ErrorModel), 400)]
        public async Task<IEnumerable<BrandModel>> ListBrandAsync (int id)
        {
            //var brands = await _brandService.GetBrandsByProduct(id);

            //_logger.LogInformation(AppLoggingEvents.Read, $"Se han obtenido un total de {brands.Count()} marcas " +
            //    $" para el producto con id {id}");

            //return _mapper.Map<IEnumerable<Brand>, IEnumerable<BrandModel>>(brands);
            return null;
        }


        // POST api/products
        [HttpPost]
        [ProducesResponseType(typeof(ProductModel), 200)]
        [ProducesResponseType(typeof(ErrorModel), 400)]
        public async Task<IActionResult> PostAsync([FromBody] AddItemModel body)
        {
            Product product = new Product();

            product.Name = body.Name;

            var result = await _productService.SaveProductAsync(product);

            if (!result.Success)
            {
                return BadRequest(new ErrorModel(result.Message));
            }

            _logger.LogInformation(AppLoggingEvents.Create, $"Añadida el producto con Id {result.Resource.Id}");

            var productModel = _mapper.Map<Product, ProductModel>(product);

            return Ok(productModel);
        }

        // PUT api/products/5
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ProductModel), 200)]
        [ProducesResponseType(typeof(ErrorModel), 400)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] AddItemModel body)
        {
            //El BadRequest debe validarse en el controlador para no llegar más lejos
            //if (id == 0 || id != product.Id)
            //{
            //    return BadRequest("Parameter Id and ProductId must be the same");
            //}

            //Product newProduct = new Product();
            //ProductResponse oldProduct = await _productService.FindProductByIdAsync(id);

            //if (oldProduct == null)
            //{
            //    return BadRequest(new ErrorModel("El producto no existe en base de datos"));
            //}

            //newProduct.Name = body.Name;

            //var result = await _productService.UpdateProductAsync(id, newProduct);

            //if (!result.Success)
            //{
            //    return BadRequest(new ErrorModel(result.Message));
            //}

            //_logger.LogInformation(AppLoggingEvents.Update, $"Actualizada el producto con Id {result.Resource.Id}");

            //var productModel = _mapper.Map<Product, ProductModel>(newProduct);

            //return Ok(productModel);
            return null;
        }

        // DELETE api/products/5
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ProductModel), 200)]
        [ProducesResponseType(typeof(ErrorModel), 400)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            //var result = await _productService.DeleteProductAsync(id);

            //if (!result.Success)
            //{
            //    return BadRequest(new ErrorModel(result.Message));
            //}

            //_logger.LogInformation(AppLoggingEvents.Delete, $"Eliminado el producto con Id {id}");

            //var productModel = _mapper.Map<Product, ProductModel>(result.Resource);

            //return Ok(productModel);
            return null;
        }
    }
}
