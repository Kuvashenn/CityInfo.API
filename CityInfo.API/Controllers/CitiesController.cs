using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CityInfo.API.Services;
using CityInfo.API.Models;

namespace CityInfo.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Cities")]
    public class CitiesController : Controller
    {
      
        private ICityInfoRepository _cityInfoRepository;

        public CitiesController(ICityInfoRepository cityInfoRepository)
        {
            _cityInfoRepository = cityInfoRepository;
        }

        [HttpGet]
        public ActionResult GetCities()
        {
            //return Ok(CitiesDataStore.Current.Cities);
            var cityEntities = _cityInfoRepository.GetCities();

            var results  = new List<CityWithoutPointsOfInterstDto>();

            foreach (var cityEntity in cityEntities)
            {
                results.Add(new CityWithoutPointsOfInterstDto
                {
                    Id = cityEntity.Id,
                    Description = cityEntity.Description,
                    Name = cityEntity.Name
                });
            }

            return Ok(results);
        }

        [HttpGet("{id}")]
        public IActionResult GetCity(int id, bool includePointsOfInterest)
        {
            var city = _cityInfoRepository.GetCity(id, includePointsOfInterest);
            if (city == null) return NotFound();

            
            if (includePointsOfInterest)
            {
                var result = new CityDto()
                {
                    Id = city.Id,
                    Name = city.Name,
                    Description = city.Description
                };
                foreach (var pointOfInterest in city.PointsOfInterest)
                {
                    result.PointsOfInterest.Add(
                        new PointOfInterestDto()
                        {
                            Id = pointOfInterest.Id,
                            Name = pointOfInterest.Name,
                            Description = pointOfInterest.Description
                        });
                }

                return Ok(result);
            }

            var cityWithoutPointsOfInterest = new CityWithoutPointsOfInterstDto()
            {
                Id = city.Id,
                Name = city.Name,
                Description = city.Description
            };

            return Ok(cityWithoutPointsOfInterest);
         }
    }
}