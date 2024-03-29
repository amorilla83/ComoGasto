﻿using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using AutoMapper;
using Expenses.API.Models;
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
    public class PurchaseController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<PurchaseController> _logger;

        private readonly IPurchaseService _purchaseService;

        public PurchaseController(IMapper mapper, ILogger<PurchaseController> logger,
            IPurchaseService purchaseService)
        {
            _mapper = mapper;
            _logger = logger;
            _purchaseService = purchaseService;
        }

        // GET api/purchase
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PurchaseModel>), 200)]
        public async Task<IEnumerable<PurchaseModel>> ListAsync()
        {
            var purchases = await _purchaseService.GetAllAsync();

            _logger.LogInformation(AppLoggingEvents.Read, $"Se han obtenido un total de {purchases.Count()} compras");

            return _mapper.Map<IEnumerable<Purchase>, IEnumerable<PurchaseModel>>(purchases);
        }


        // GET api/purchase/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(IEnumerable<ProductPurchaseModel>), 200)]
        [ProducesResponseType(typeof(ErrorModel), 400)]
        public async Task<IActionResult> GetPurchaseAsync(int id)
        {
            var result = await _purchaseService.GetPurchaseByIdAsync(id);

            if (!result.Success)
            {
                return BadRequest(new ErrorModel(result.Message));
            }

            _logger.LogInformation(AppLoggingEvents.Read, $"Se han obtenido los datos de la compra con id {id}");

            var purchaseModel = _mapper.Map<Purchase, PurchaseModel>(result.Resource);

            return Ok(purchaseModel);
        }

        // GET api/purchase/{id}/product
        [HttpGet("{id}/product")]
        [ProducesResponseType(typeof(IEnumerable<ProductPurchaseModel>), 200)]
        public async Task<IEnumerable<ProductPurchaseModel>> ListProductsAsync(int id)
        {
            var productPurchase = await _purchaseService.GetProductsPurchase(id);

            _logger.LogInformation(AppLoggingEvents.Read, $"Se han obtenido un total de " +
                $"{productPurchase.Count()} productos para la compra {id}");

            return _mapper.Map<IEnumerable<ProductPurchase>, IEnumerable<ProductPurchaseModel>>(productPurchase);
        }

        // POST api/purchase
        [HttpPost]
        [ProducesResponseType(typeof(PurchaseModel), 200)]
        [ProducesResponseType(typeof(ErrorModel), 400)]
        public async Task<IActionResult> PostAsync([FromBody] AddPurchaseModel body)
        {
            Purchase purchase = _mapper.Map<AddPurchaseModel, Purchase>(body);

            var result = await _purchaseService.SavePurchaseAsync(purchase);

            if (!result.Success)
            {
                return BadRequest(new ErrorModel(result.Message));
            }

            _logger.LogInformation(AppLoggingEvents.Create, $"Añadida la compra con Id {result.Resource.Id}");

            var purchaseModel = _mapper.Map<Purchase, PurchaseModel>(result.Resource);

            return Ok(purchaseModel);
        }

        // PUT api/purchase/5/product
        [HttpPut("{id}/product")]
        [ProducesResponseType(typeof(ProductPurchaseModel), 200)]
        [ProducesResponseType(typeof(ErrorModel), 400)]
        public async Task<IActionResult> PutProductAsync(int id, [FromBody] AddProductPurchaseModel body)
        {
            ProductPurchase product = _mapper.Map<AddProductPurchaseModel, ProductPurchase>(body);

            var result = await _purchaseService.AddProductToPurchase(id, product);

            if (!result.Success)
            {
                return BadRequest(new ErrorModel(result.Message));
            }

            _logger.LogInformation(AppLoggingEvents.Create, $"Añadido el producto {body.ProductId} a la compra" +
                $" con Id {result.Resource.PurchaseId}");

            var purchaseModel = _mapper.Map<ProductPurchase, ProductPurchaseModel>(result.Resource);

            return Ok(purchaseModel);
        }

        // PUT api/purchase/5/product/3
        [HttpPut("{id}/product/{idProductPurchase}")]
        [ProducesResponseType(typeof(ProductPurchaseModel), 200)]
        [ProducesResponseType(typeof(ErrorModel), 400)]
        public async Task<IActionResult> PutProductPurchaseAsync(int id, int idProductPurchase, [FromBody] AddProductPurchaseModel body)
        {
            ProductPurchase product = _mapper.Map<AddProductPurchaseModel, ProductPurchase>(body);

            var result = await _purchaseService.AddProductToPurchase(id, product);

            if (!result.Success)
            {
                return BadRequest(new ErrorModel(result.Message));
            }

            _logger.LogInformation(AppLoggingEvents.Create, $"Añadido el producto {body.ProductDetail.ProductId} a la compra" +
                $" con Id {result.Resource.PurchaseId}");

            var purchaseModel = _mapper.Map<ProductPurchase, ProductPurchaseModel>(result.Resource);

            return Ok(purchaseModel);
        }

        //PUT api/purchase/3
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(PurchaseModel), 200)]
        [ProducesResponseType(typeof(ErrorModel), 400)]
        public async Task<IActionResult> PutPurchaseAsync (int id, [FromBody] PurchaseModel body)
        {
            if (id != body.IdPurchase)
            {
                return BadRequest(new ErrorModel("Los datos de la petición no son correctos"));
            }

            Purchase purchase = _mapper.Map<PurchaseModel, Purchase>(body);

            var result = await _purchaseService.UpdatePurchaseAsync(purchase);

            if (!result.Success)
            {
                return BadRequest(new ErrorModel(result.Message));
            }

            _logger.LogInformation(AppLoggingEvents.Create, $"Modificada la compra con Id {result.Resource.Id}");

            var purchaseModel = _mapper.Map<Purchase, PurchaseModel>(result.Resource);

            return Ok(purchaseModel);
        }


        [HttpDelete("{idPurchase}/product/{idProductPurchase}")]
        [ProducesResponseType(typeof(ProductPurchaseModel), 200)]
        [ProducesResponseType(typeof(ErrorModel), 400)]
        public async Task<IActionResult> DeleteProductAsync(int idPurchase, int idProductPurchase)
        {
            var result = await _purchaseService.DeleteProductFromPurchase(idPurchase, idProductPurchase);

            if (!result.Success)
            {
                return BadRequest(new ErrorModel(result.Message));
            }

            _logger.LogInformation(AppLoggingEvents.Delete, $"Eliminado el producto {idProductPurchase} de la compra" +
                $" con Id {idPurchase}");

            var purchaseModel = _mapper.Map<ProductPurchase, ProductPurchaseModel>(result.Resource);

            return Ok(purchaseModel);

        }

        [HttpDelete("{idPurchase}")]
        public async Task<IActionResult> DeleteAsync (int idPurchase)
        {
            var result = await _purchaseService.DeletePurchase(idPurchase);

            if (!result.Success)
            {
                return BadRequest(new ErrorModel(result.Message));
            }

            _logger.LogInformation(AppLoggingEvents.Delete, $"Eliminada la compra {idPurchase}");

            return Ok();
        }
    }
}
