using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Expenses.API.Models;
using Expenses.API.Models.Brands;
using Expenses.Core.ApplicationService;
using Expenses.Core.Entities;
using Expenses.Core.Entities.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace Expenses.API.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<BrandsController> _logger;

        private readonly IBrandService _brandService;

        public BrandsController(IBrandService brandService,
            IMapper mapper, ILogger<BrandsController> logger)
        {
            _brandService = brandService;
            _mapper = mapper;
            _logger = logger;
        }

        // GET api/brands
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<BrandModel>), 200)]
        public async Task<IEnumerable<BrandModel>> ListAsync()
        {
            var brands = await _brandService.GetAllBrandsAsync();

            _logger.LogInformation(AppLoggingEvents.Read, $"Se han obtenido un total de {brands.Count()} marcas");

            return _mapper.Map<IEnumerable<Brand>, IEnumerable<BrandModel>>(brands);
        }

        // POST api/brands
        [HttpPost]
        [ProducesResponseType(typeof(BrandModel), 200)]
        [ProducesResponseType(typeof(ErrorModel), 400)]
        public async Task<IActionResult> PostAsync([FromBody] AddBrandModel body)
        {
            Brand brand = new Brand()
            {
                Name = body.Name
            };

            if (body.ProductId.HasValue)
            {
                brand.ProductList = new List<Product>() { new Product { Id = body.ProductId.Value } };
            }

            var result = await _brandService.SaveBrandAsync(brand);

            if (!result.Success)
            {
                return BadRequest(new ErrorModel(result.Message));
            }

            _logger.LogInformation(AppLoggingEvents.Create, $"Añadida la marca con Id {result.Resource.Id} " +
                $"relacionada con el producto {body.ProductId}");

            BrandModel brandModel = _mapper.Map<Brand, BrandModel>(result.Resource);

            return Ok(brandModel);
        }
    }
}
