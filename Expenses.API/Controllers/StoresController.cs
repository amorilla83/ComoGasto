using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Expenses.API.Models;
using Expenses.Core.ApplicationService;
using Expenses.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Expenses.API.Controllers
{
    [Route("api/stores")]
    [Produces("application/json")]
    [ApiController]
    public class StoresController : Controller
    {
        private readonly IStoreService _storeService;
        private readonly IMapper _mapper;

        public StoresController(IStoreService storeService, IMapper mapper)
        {
            _storeService = storeService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<StoreModel>), 200)]
        public async Task<IEnumerable<StoreModel>> ListAsync()
        {
            var stores = await _storeService.GetAllStoresAsync();


            var model = _mapper.Map<IEnumerable<Store>, IEnumerable<StoreModel>>(stores);

            //Pasamos la imagen a base64 para que se muestre directamente
            foreach (StoreModel store in model)
            {
                byte[] imageArray = System.IO.File.ReadAllBytes(Path.Combine(@"Resources/Images/Stores", store.Logo));
                string extension = store.Logo.Split('.').LastOrDefault();
                store.Image = $"data:image/{extension};base64, {Convert.ToBase64String(imageArray)}";
            }

            return model;
        }

        [HttpPost]
        [ProducesResponseType(typeof(StoreModel), 200)]
        [ProducesResponseType(typeof(ErrorModel), 400)]
        public async Task<IActionResult> PostAsync([FromForm] AddStoreModel body)
        {
            Store store = new Store();

            if (body.Logo.Length > 0)
            {
                try
                {
                    string fileName = Path.GetRandomFileName() + Path.GetExtension(body.Logo.FileName);
                    var filePath = Path.Combine(@"Resources/Images/Stores", fileName);

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await body.Logo.CopyToAsync(stream);
                    }

                    store.Logo = fileName;
                }
                catch (Exception ex)
                {
                    return BadRequest(new ErrorModel($"Error saving store logo {ex.Message}"));
                }
                
            }
            //var store = _mapper.Map<AddStoreModel, Store>(body);
            store.Name = body.Name;
            var result = await _storeService.SaveStoreAsync(store);

            if (!result.Success)
            {
                return BadRequest(new ErrorModel(result.Message));
            }

            var storeModel = _mapper.Map<Store, StoreModel>(store);
            return Ok(storeModel);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(StoreModel), 200)]
        [ProducesResponseType(typeof(ErrorModel), 400)]
        public async Task<IActionResult> PutAsync (int id, [FromBody] AddStoreModel body)
        {
            var store = _mapper.Map<AddStoreModel, Store>(body);
            var result = await _storeService.UpdateStoreAsync(id, store);

            if (!result.Success)
            {
                return BadRequest(new ErrorModel(result.Message));
            }

            var storeModel = _mapper.Map<Store, StoreModel>(store);
            return Ok(storeModel);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(StoreModel), 200)]
        [ProducesResponseType(typeof(ErrorModel), 400)]
        public async Task<IActionResult> DeleteAsync (int id)
        {
            var result = await _storeService.DeleteStoreAsync(id);

            if (!result.Success)
            {
                return BadRequest(new ErrorModel(result.Message));
            }

            var storeModel = _mapper.Map<Store, StoreModel>(result.Resource);
            return Ok(storeModel);
        }

    }
}
