using System;
using System.Collections.Generic;
using System.Linq;
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
    public class FormatsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<FormatsController> _logger;
        private readonly IFormatService _formatService;

        public FormatsController (IMapper mapper, ILogger<FormatsController> logger,
            IFormatService formatService)
        {
            _mapper = mapper;
            _logger = logger;
            _formatService = formatService;
        }

        //GET api/formats
        [HttpGet]
        [ProducesResponseType(typeof(ItemModel), 200)]
        [ProducesResponseType(typeof(ErrorModel), 400)]
        public async Task<IActionResult> GetAsync ()
        {
            var formats = await _formatService.GetAllFormatsAsync();

            _logger.LogInformation(AppLoggingEvents.Read, $"Se han obtenido un total de {formats.Count()} formatos");

            return Ok(_mapper.Map<IEnumerable<Format>, IEnumerable<ItemModel>>(formats));
        }
        

        // POST api/formats
        [HttpPost]
        [ProducesResponseType(typeof(FormatModel), 200)]
        [ProducesResponseType(typeof(ErrorModel), 400)]
        public async Task<IActionResult> PostAsync([FromBody] AddFormatModel body)
        {
            Format format = new()
            {
                Name = body.Name,
            };

            var result = await _formatService.SaveFormatAsync(format);

            if (!result.Success)
            {
                return BadRequest(new ErrorModel(result.Message));
            }

            _logger.LogInformation(AppLoggingEvents.Create, $"Añadida el formato con Id {result.Resource.Id} ");
            //    $"relacionado con la marca {body.ParentId}");

            ItemModel formatModel = _mapper.Map<Format, ItemModel>(result.Resource);

            return Ok(formatModel);
        }
    }
}
