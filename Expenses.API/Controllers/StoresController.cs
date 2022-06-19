using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Expenses.API.Models;
using Expenses.API.Models.Stores;
using Expenses.Core.ApplicationService;
using Expenses.Core.Entities;
using Expenses.Core.Entities.Communication;
using Expenses.Core.Entities.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Expenses.API.Controllers
{
    [Route("api/Stores")]
    [Produces("application/json")]
    [ApiController]
    public class StoresController : Controller
    {
        private readonly IStoreService _storeService;
        private readonly IMapper _mapper;
        private readonly ILogger<StoresController> _logger;

        public StoresController(IStoreService storeService, IMapper mapper, ILogger<StoresController> logger)
        {
            _storeService = storeService;
            _mapper = mapper;
            _logger = logger;
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

            _logger.LogInformation(AppLoggingEvents.Read, $"Se han obtenido un total de {model.Count()} stores");

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
                string fileName = await SaveLogo(body.Logo);
                if (string.IsNullOrEmpty(fileName))
                {
                    return BadRequest(new ErrorModel($"Error saving store logo"));
                }
                else
                {
                    store.Logo = fileName;
                }

            }
            store.Name = body.Name;
            var result = await _storeService.SaveStoreAsync(store);

            if (!result.Success)
            {
                return BadRequest(new ErrorModel(result.Message));
            }

            _logger.LogInformation(AppLoggingEvents.Create, $"Añadida la store con Id {result.Resource.StoreId}");

            var storeModel = _mapper.Map<Store, StoreModel>(store);
            return Ok(storeModel);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(StoreModel), 200)]
        [ProducesResponseType(typeof(ErrorModel), 400)]
        public async Task<IActionResult> PutAsync (int id, [FromForm] EditStoreModel body)
        {
            //Puede q haya q actualizar la imagen
            Store newStore = new Store();
            StoreResponse oldStore = await _storeService.FindStoreByIdAsync(id);
            string fileToDelete = string.Empty;

            if (oldStore == null)
            {
                return BadRequest(new ErrorModel("La tienda no existe en base de datos"));
            }

            if (!string.IsNullOrEmpty(body.Name))
            {
                newStore.Name = body.Name;
            }
            else
            {
                newStore.Name = oldStore.Resource.Name;
            }

            if (body.Logo != null && body.Logo.Length > 0)
            {
                string fileName = await SaveLogo(body.Logo);
                newStore.Logo = fileName;
                fileToDelete = oldStore.Resource.Logo;
            }
            else
            {
                newStore.Logo = oldStore.Resource.Logo;
            }

            var result = await _storeService.UpdateStoreAsync(id, newStore);

            if (!result.Success)
            {
                return BadRequest(new ErrorModel(result.Message));
            }

            _logger.LogInformation(AppLoggingEvents.Update, $"Actualizada la store con Id {result.Resource.StoreId}");

            if (!string.IsNullOrEmpty(fileToDelete))
            {
                _logger.LogDebug($"Eliminamos la imagen {fileToDelete}");
                System.IO.File.Delete(Path.Combine(@"Resources/Images/Stores", fileToDelete));
            }

            var storeModel = _mapper.Map<Store, StoreModel>(newStore);
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

            _logger.LogInformation(AppLoggingEvents.Delete, $"Eliminada la store con Id {id}");


            _logger.LogDebug($"Eliminamos la imagen {result.Resource.Logo}");
            System.IO.File.Delete(Path.Combine(@"Resources/Images/Stores", result.Resource.Logo));

            var storeModel = _mapper.Map<Store, StoreModel>(result.Resource);
            return Ok(storeModel);
        }

        private static async Task<string> SaveLogo (IFormFile logo)
        {
            try
            {
                string fileName = Path.GetRandomFileName() + Path.GetExtension(logo.FileName);
                var filePath = Path.Combine(@"Resources/Images/Stores", fileName);

                using (var stream = System.IO.File.Create(filePath))
                {
                    await logo.CopyToAsync(stream);
                }

                return fileName;
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}
