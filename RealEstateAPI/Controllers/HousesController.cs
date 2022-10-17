using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstateAPI.Models.DTO;
using RealEstateAPI.Repositories;
using System.Data;

namespace RealEstateAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HousesController : Controller
    {
        private readonly IHousesRepository _housesRepository;
        private readonly IMapper _mapper;
        private readonly IRegionsRepository _regionsRepository;

        public HousesController(IHousesRepository housesRepository, IMapper mapper,
            IRegionsRepository regionsRepository)
        {
            _housesRepository = housesRepository;
            _mapper = mapper;
            _regionsRepository = regionsRepository;
        }

        [HttpGet]
        [Authorize(Roles = "reader")]
        public async Task<IActionResult> GetAllHousesAsync()
        {

            var houses = await _housesRepository.GetAllAsync();

            var housesDTO = _mapper.Map<List<Models.DTO.House>>(houses);

            return Ok(housesDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetHouseAsync")]
        [Authorize(Roles = "reader")]
        public async Task<IActionResult> GetHouseAsync(Guid id)
        {
            var house = await _housesRepository.GetAsync(id);

            var houseDTO = _mapper.Map<Models.DTO.House>(house);

            return Ok(houseDTO);
        }

        [HttpPost]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> AddHouseAsync([FromBody] Models.DTO.AddHouseRequest addHouseRequest)
        {

            if (!ValidateAddHouseAsync(addHouseRequest))
            {
                return BadRequest(ModelState);
            }

            var houseDomain = new Models.Domain.House
            {
                Price = addHouseRequest.Price,
                Status = addHouseRequest.Status,
                Suburb = addHouseRequest.Suburb,
                Postcode = addHouseRequest.Postcode,
                Bedroom = addHouseRequest.Bedroom,
                Bathroom = addHouseRequest.Bathroom,
                Room = addHouseRequest.Room,
                Shed = addHouseRequest.Shed,
                SqMeter = addHouseRequest.SqMeter,
                Text = addHouseRequest.Text,
                RegionId = addHouseRequest.RegionId,
                LandscapeId = addHouseRequest.LandscapeId
            };

            houseDomain = await _housesRepository.AddAsync(houseDomain);

            var houseDTO = new Models.DTO.House
            {
                Price = addHouseRequest.Price,
                Status = addHouseRequest.Status,
                Suburb = addHouseRequest.Suburb,
                Postcode = addHouseRequest.Postcode,
                Bedroom = addHouseRequest.Bedroom,
                Bathroom = addHouseRequest.Bathroom,
                Room = addHouseRequest.Room,
                Shed = addHouseRequest.Shed,
                SqMeter = addHouseRequest.SqMeter,
                Text = addHouseRequest.Text,
                RegionId = addHouseRequest.RegionId,
                LandscapeId = addHouseRequest.LandscapeId
            };

            return CreatedAtAction(nameof(GetHouseAsync), new { id = houseDTO.Id }, houseDTO);

        }

        [HttpPut]
        [Route("{id:guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> UpdateHouseAsync(
            [FromRoute] Guid id, [FromBody] Models.DTO.UpdateHouseRequest updateHouseRequest)
        {
            var houseDomain = new Models.Domain.House
            {
                Price = updateHouseRequest.Price,
                Status = updateHouseRequest.Status,
                Suburb = updateHouseRequest.Suburb,
                Postcode = updateHouseRequest.Postcode,
                Bedroom = updateHouseRequest.Bedroom,
                Bathroom = updateHouseRequest.Bathroom,
                Room = updateHouseRequest.Room,
                Shed = updateHouseRequest.Shed,
                SqMeter = updateHouseRequest.SqMeter,
                Text = updateHouseRequest.Text,
                RegionId = updateHouseRequest.RegionId,
                LandscapeId = updateHouseRequest.LandscapeId
            };

            houseDomain = await _housesRepository.UpdateAsync(id, houseDomain);

            if (houseDomain == null)
            {
                return NotFound();
            }
            else
            {
                var walkDTO = new Models.DTO.House
                {
                    Price = houseDomain.Price,
                    Status = houseDomain.Status,
                    Suburb = houseDomain.Suburb,
                    Postcode = houseDomain.Postcode,
                    Bedroom = houseDomain.Bedroom,
                    Bathroom = houseDomain.Bathroom,
                    Room = houseDomain.Room,
                    Shed = houseDomain.Shed,
                    SqMeter = houseDomain.SqMeter,
                    Text = houseDomain.Text,
                    RegionId = houseDomain.RegionId,
                    LandscapeId = houseDomain.LandscapeId
                };

                return Ok(walkDTO);
            }
        }

        [HttpDelete]
        [Route("{id:guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> DeleteHouseAsync(Guid id)
        {
            var houseDomain = await _housesRepository.GetAsync(id);

            if (houseDomain == null)
            {
                return NotFound();
            }

            var houseDTO = _mapper.Map<Models.DTO.House>(houseDomain);

            return Ok(houseDTO);
        }

        #region Private Methods

        private bool ValidateAddHouseAsync(Models.DTO.AddHouseRequest addHouseRequest)
        {
            if (addHouseRequest == null)
            {
                ModelState.AddModelError(nameof(addHouseRequest),
                    $"{nameof(addHouseRequest)} cannot be empty");
            }

            if (addHouseRequest.Price < 0)
            {
                ModelState.AddModelError(nameof(addHouseRequest.Price),
                    $"{nameof(addHouseRequest.Price)} Must be greater than zero");
            }

            if (string.IsNullOrEmpty(addHouseRequest.Status))
            {
                ModelState.AddModelError(nameof(addHouseRequest.Status),
                      $"{nameof(addHouseRequest.Status)} cannot be null or empty");
            }

            if (string.IsNullOrEmpty(addHouseRequest.Suburb))
            {
                ModelState.AddModelError(nameof(addHouseRequest.Suburb),
                      $"{nameof(addHouseRequest.Suburb)} cannot be null or empty");
            }

            if (addHouseRequest.Postcode < 200)
            {
                ModelState.AddModelError(nameof(addHouseRequest.Postcode),
                    $"{nameof(addHouseRequest.Postcode)} Must be greater than Two Hundred");
            }

            if (addHouseRequest.Postcode < 200)
            {
                ModelState.AddModelError(nameof(addHouseRequest.Postcode),
                    $"{nameof(addHouseRequest.Postcode)} Must be greater than Two Hundred");
            }
            if (addHouseRequest.Bedroom < 0)
            {
                ModelState.AddModelError(nameof(addHouseRequest.Bedroom),
                    $"{nameof(addHouseRequest.Bedroom)} Must be atleast zero");
            }

            if (addHouseRequest.Bathroom < 0)
            {
                ModelState.AddModelError(nameof(addHouseRequest.Bathroom),
                    $"{nameof(addHouseRequest.Bathroom)} Must be atleast zero");
            }

            if (addHouseRequest.Room < 0)
            {
                ModelState.AddModelError(nameof(addHouseRequest.Room),
                    $"{nameof(addHouseRequest.Room)} Must be atleast zero");
            }

            if (addHouseRequest.Shed < 0)
            {
                ModelState.AddModelError(nameof(addHouseRequest.Shed),
                    $"{nameof(addHouseRequest.Shed)} Must be atleast zero");
            }

            if (addHouseRequest.SqMeter < 300)
            {
                ModelState.AddModelError(nameof(addHouseRequest.SqMeter),
                    $"{nameof(addHouseRequest.SqMeter)} Must be Three Hundred");
            }

            if (string.IsNullOrEmpty(addHouseRequest.Text))
            {
                ModelState.AddModelError(nameof(addHouseRequest.Text),
                      $"{nameof(addHouseRequest.Text)} cannot be null or empty");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }

        private bool ValidateUpdateHouseAsync(Models.DTO.UpdateHouseRequest updateHouseRequest)
        {
            if (updateHouseRequest == null)
            {
                ModelState.AddModelError(nameof(updateHouseRequest),
                    $"{nameof(updateHouseRequest)} cannot be empty");
            }

            if (updateHouseRequest.Price < 0)
            {
                ModelState.AddModelError(nameof(updateHouseRequest.Price),
                    $"{nameof(updateHouseRequest.Price)} Must be greater than zero");
            }

            if (string.IsNullOrEmpty(updateHouseRequest.Status))
            {
                ModelState.AddModelError(nameof(updateHouseRequest.Status),
                      $"{nameof(updateHouseRequest.Status)} cannot be null or empty");
            }

            if (string.IsNullOrEmpty(updateHouseRequest.Suburb))
            {
                ModelState.AddModelError(nameof(updateHouseRequest.Suburb),
                      $"{nameof(updateHouseRequest.Suburb)} cannot be null or empty");
            }

            if (updateHouseRequest.Postcode < 200)
            {
                ModelState.AddModelError(nameof(updateHouseRequest.Postcode),
                    $"{nameof(updateHouseRequest.Postcode)} Must be greater than Two Hundred");
            }

            if (updateHouseRequest.Postcode < 200)
            {
                ModelState.AddModelError(nameof(updateHouseRequest.Postcode),
                    $"{nameof(updateHouseRequest.Postcode)} Must be greater than Two Hundred");
            }
            if (updateHouseRequest.Bedroom < 0)
            {
                ModelState.AddModelError(nameof(updateHouseRequest.Bedroom),
                    $"{nameof(updateHouseRequest.Bedroom)} Must be atleast zero");
            }

            if (updateHouseRequest.Bathroom < 0)
            {
                ModelState.AddModelError(nameof(updateHouseRequest.Bathroom),
                    $"{nameof(updateHouseRequest.Bathroom)} Must be atleast zero");
            }

            if (updateHouseRequest.Room < 0)
            {
                ModelState.AddModelError(nameof(updateHouseRequest.Room),
                    $"{nameof(updateHouseRequest.Room)} Must be atleast zero");
            }

            if (updateHouseRequest.Shed < 0)
            {
                ModelState.AddModelError(nameof(updateHouseRequest.Shed),
                    $"{nameof(updateHouseRequest.Shed)} Must be atleast zero");
            }

            if (updateHouseRequest.SqMeter < 300)
            {
                ModelState.AddModelError(nameof(updateHouseRequest.SqMeter),
                    $"{nameof(updateHouseRequest.SqMeter)} Must be Three Hundred");
            }

            if (string.IsNullOrEmpty(updateHouseRequest.Text))
            {
                ModelState.AddModelError(nameof(updateHouseRequest.Text),
                      $"{nameof(updateHouseRequest.Text)} cannot be null or empty");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }

        #endregion

    }
}

