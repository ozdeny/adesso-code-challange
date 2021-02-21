using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdessoRideShare.Core;
using AdessoRideShare.DTOs;
using AdessoRideShare.Services;
using AdessoRideShare.Validators;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AdessoRideShare
{
    [Route("api/[controller]")]
    [ApiController]
    public class RidesController : ControllerBase
    {
        private readonly IMapper _mapper;

        private readonly IRideService _rideService;

        public RidesController(IRideService rideService, IMapper mapper)
        {
            _rideService = rideService;
            _mapper = mapper;
        }

        // GET: api/<RidesController>
        [HttpGet]
        public IEnumerable<Ride> Get([FromQuery]SearchRidesDTO searchRides)
        {
            return _rideService.GetUserRides(searchRides.DepartureCityId, searchRides.ArrivalCityId, searchRides.UserId);
        }

        [HttpGet("path/{departureCityId}/{arrivalCityId}")]
        public async Task<IEnumerable<Ride>> GetRidesWithOverlappingPaths(int departureCityId, int arrivalCityId)
        {
            return await _rideService.GetRidesWithOverlappingPath(departureCityId, arrivalCityId);
        }

        //// GET api/<RidesController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<RidesController>
        [HttpPost]
        public async Task<ActionResult<SaveRideDTO>> Post([FromBody] SaveRideDTO saveRide)
        {
            var validator = new SaveRideValidator();

            var validationResult = await validator.ValidateAsync(saveRide);
            
            if(!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var rideToCreate = _mapper.Map<SaveRideDTO, Ride>(saveRide);

            var createdRide = await _rideService.CreateRide(rideToCreate);

            return Ok(createdRide);
        }

        // PUT api/<RidesController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] UpdateRideVisibilityDTO updateRide)
        {
            var validator = new UpdateRideVisibilityValidator();

            var validationResult = await validator.ValidateAsync(updateRide);

            if (id <= 0 || !validationResult.IsValid )
            {
                return BadRequest(validationResult.Errors);
            }

           var updatedRide = await _rideService.UpdateRide(new Ride()
            {
                RideId = id,
                CreatedUserId = updateRide.UserId,
                IsActive = updateRide.IsActive
            });

            if(updatedRide != null)
            {
                return Ok(updatedRide);
            } else
            {
                return NotFound();
            }
        }

        [HttpPut("{id}/passenger/{passengerId}")]
        public async Task<ActionResult<SaveRideDTO>> Put(int id, int passengerId)
        {
            //var validator = new SaveRideValidator();

            //var validationResult = await validator.ValidateAsync(saveRide);

            //if (!validationResult.IsValid)
            //{
            //    return BadRequest(validationResult.Errors);
            //}


            var ride = await _rideService.JoinRide(new Ride()
            {
                RideId = id
            }, passengerId);

            if(ride != null)
            {
               return  Ok(ride);
            } else
            {
                return new StatusCodeResult(406); // TODO Set
            }

        }

        // DELETE api/<RidesController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
