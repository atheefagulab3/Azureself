using Microsoft.AspNetCore.Mvc;
using prj.Service.Interface;
using prj.Model;
using System.Diagnostics;


namespace prj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdditionalTravellersController : ControllerBase
    {
        private readonly IAdditinalTravellersService _service;

        public AdditionalTravellersController(IAdditinalTravellersService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdditionalTraveller>>> Get()
        {
            try
            {
                var travelers = await _service.GetAdditionalTraveller();
                return Ok(travelers);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [HttpGet("{additionalId}")]
        public async Task<ActionResult<AdditionalTraveller>> GetAdditionalTravellerById(int additionalId)
        {
            try
            {
                var traveler = await _service.GetAdditionalTravellerById(additionalId);
                if (traveler == null)
                {
                    return NotFound();
                }
                return Ok(traveler);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [HttpGet("CustomerId/{customerId}")]
        public async Task<ActionResult<List<AdditionalTraveller>>> GetAdditionalTravellerByCustomerId(int customerId)
        {
            try
            {
                var travelers = await _service.GetAdditionalTravellerByCustomerId(customerId);
                if (travelers == null || travelers.Count == 0)
                {
                    return NotFound();
                }
                return Ok(travelers);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<AdditionalTraveller>> AddAdditionalTraveller(AdditionalTraveller additionalTraveller)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var addedTraveler = await _service.AddAdditionalTraveller(additionalTraveller);
                if (addedTraveler == null)
                {
                    return BadRequest();
                }
                return Ok(addedTraveler);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [HttpPut("{customerId}")]
        public async Task<ActionResult<AdditionalTraveller>> UpdateAdditionalTraveller(int customerId, AdditionalTraveller additionalTraveller)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updatedTraveler = await _service.UpdateAdditionalTraveller(customerId, additionalTraveller);
                if (updatedTraveler == null)
                {
                    return NotFound();
                }
                return Ok(updatedTraveler);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [HttpDelete("{customerId}")]
        public async Task<ActionResult<AdditionalTraveller>> DeleteAdditionalTraveller(int customerId)
        {
            try
            {
                var deletedTraveler = await _service.DeleteAdditionalTraveller(customerId);
                if (deletedTraveler == null)
                {
                    return NotFound();
                }
                return Ok(deletedTraveler);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }
    }
}
