using CityInfo.API.Models;
using Microsoft.AspNetCore.Mvc;
using TestAPI;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/Cities")]
    public class CitiesController: ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<CityDto>> GetCities()
        {
            return Ok(CitiesDataStore.current.Cities);
        }

        [HttpGet("{id}")]
        public ActionResult<CityDto> GetCity(int id)
        {

            var cityToReturn = CitiesDataStore.current.Cities
                 .FirstOrDefault(c => c.Id == id);

            if(cityToReturn  == null)
            {
                return NotFound();
            }

            return Ok(cityToReturn);
   
        }

    }
}
