using CityInfo.API.Models;
using Microsoft.AspNetCore.Mvc;
using TestAPI;
using TestAPI.Models;

namespace CityInfo.API.Controllers
{
    //  /api/cities/3/pointsofinterest
    [Route("api/cities/{cityId}/pointsofinterest")]
    [ApiController]
    public class PointsOfInterestController : ControllerBase
    {
        #region Get All

        [HttpGet]
        public ActionResult<IEnumerable<PointOfInterestDto>>
            GetPointsOfInterest(int cityId)
        {
            var city =
                CitiesDataStore.current.Cities
                .FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            return Ok(city.PointsOfInterest);
        }


        #endregion

        #region Get BY Id

        [HttpGet("{pointOfInterestId}", Name = "GetPointOfInterest")]
        public ActionResult<PointOfInterestDto> GetPointOfInterest(int cityId, int pointOfInterestId)
        {
            var city = CitiesDataStore.current.Cities
                        .FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            var point = city.PointsOfInterest
                .FirstOrDefault(p => p.Id == pointOfInterestId);

            if (point == null)
            {
                return NotFound();
            }

            return Ok(point);
        }


        #endregion

        #region Post Point Of Interest

        [HttpPost]
        public ActionResult<PointOfInterestDto> CreatePointOfInterest(int cityId, [FromBody] PointOfInterestForCreationDto pointOfInterest)
        {
            if (!ModelState.IsValid) return BadRequest();

            var city = CitiesDataStore.current
                .Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            var maxpointOfInterestId = CitiesDataStore.current.Cities
                .SelectMany(c => c.PointsOfInterest)
                .Max(p => p.Id);

            var createPoint = new PointOfInterestDto()
            {
                Id = ++maxpointOfInterestId,
                Name = pointOfInterest.Name,
                Description = pointOfInterest.Description
            };

            city.PointsOfInterest.Add(createPoint);

            return CreatedAtAction("GetPointOfInterest",
                new
                {
                    cityId = cityId,
                    pointOfInterestId = createPoint.Id
                }, createPoint);
        }

        #endregion

        #region Update PointsOFInterest

        [HttpPut]
        public ActionResult UpdatePointOfInterest(int cityId,int interestId, [FromBody] PointOdInerestForUpdate model)
        {
            if (!ModelState.IsValid) return BadRequest();

            var city = CitiesDataStore.current
                .Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            var point = CitiesDataStore.current.Cities
                .FirstOrDefault(c => c.Id == interestId);

            if(point == null)
            {
                return NotFound();
            }

            city.Name = model.Name;
            city.Description = model.Description;

            return NoContent();
        }

        #endregion
    }
}

