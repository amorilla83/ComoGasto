using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Expenses.API.Models;
using Expenses.Core.ApplicationService;
using Expenses.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Expenses.API.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class DetailsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<DetailsController> _logger;
        private readonly IProductDetailsService _productDetailsService;

        public DetailsController(IMapper mapper, ILogger<DetailsController> logger,
            IProductDetailsService productDetailsService)
        {
            _mapper = mapper;
            _logger = logger;
            _productDetailsService = productDetailsService;
        }

        // GET api/details/formats/{id}
        [HttpGet]
        [Route("formats/{id}")]
        public ActionResult<IEnumerable<ItemModel>> GetFormatsByBrand(int id)
        {
            try
            {
                var formats = _productDetailsService.GetFormatsByBrand(id);

                return Ok(_mapper.Map<List<Format>, List<ItemModel>>(formats));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

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
    }
}

