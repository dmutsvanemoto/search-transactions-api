using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Transactions.API.Business;
using Transactions.API.Data.Models;

namespace Transactions.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ILogger<SearchController> _logger;
        private readonly ISearchService _searchService;

        public SearchController(ILogger<SearchController> logger, ISearchService searchService)
        {
            _logger = logger;
            _searchService = searchService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> SearchByDescription([FromQuery]string description)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(description))
                {
                    return BadRequest();
                }

                var merchantName = await _searchService.GetMerchantNameByDescriptionAsync(description);

                if (string.IsNullOrWhiteSpace(merchantName))
                {
                    return NotFound();
                }

                return Ok(merchantName);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"Failed to search for a merchant name using the description = '{description}'");
            }
            
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }

        [HttpPut]
        [Route("")]
        public async Task<IActionResult> CreateMerchantDescription([FromBody] MerchantDescription merchantDescription)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(merchantDescription.Description))
                {
                    return BadRequest();
                }

                await _searchService.CreateMerchantDescriptionAsync(merchantDescription);

                return NoContent();
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"Failed to create MerchantDescription. Description = '{merchantDescription.Description}', Merchant = '{merchantDescription.Merchant}'");
            }

            return StatusCode((int)HttpStatusCode.InternalServerError);
        }
    }
}
