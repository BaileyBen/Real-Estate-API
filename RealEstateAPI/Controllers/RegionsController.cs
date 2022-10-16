using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RealEstateAPI.Models.DTO;
using RealEstateAPI.Repositories;

namespace RealEstateAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegionsController : Controller
    {
        private readonly IRegionsRepository _regionsRepository;
        private readonly IMapper _mapper;

        public RegionsController(IRegionsRepository regionsRepository, IMapper mapper)
        {
            _regionsRepository = regionsRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRegionsAsync()
        {
            var regions = await _regionsRepository.GetAllAsync();

            var regionsDTO = _mapper.Map<List<Models.DTO.Region>>(regions);

            return Ok(regionsDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetRegionAsync")]
        public async Task<IActionResult> GetRegionAsync(Guid id)
        {
            var region = await _regionsRepository.GetAsync(id);

            if (region == null)
            {
                return NotFound();
            }

            var regionDTO = _mapper.Map<Models.DTO.Region>(region);

            return Ok(regionDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddRegionAsync(Models.DTO.AddRegionRequest addRegionRequest)
        {
            if (!ValidateAddRegionAsync(addRegionRequest))
            {
                return BadRequest(ModelState);
            }

            var region = new Models.Domain.Region()
            {
                State = addRegionRequest.State,
                Code = addRegionRequest.Code,
                City = addRegionRequest.City,
                Population = addRegionRequest.Population,
            };

            var response = await _regionsRepository.AddAsync(region);

            var regionDTO = _mapper.Map<Models.DTO.Region>(response);

            return CreatedAtAction(nameof(GetRegionAsync), new { id = regionDTO.Id }, regionDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRegionAsync(Guid id)
        {
            var region = await _regionsRepository.DeleteAsync(id);

            if (region == null)
            {
                return NotFound();
            }

            var regionDTO = _mapper.Map<Models.DTO.Region>(region);

            return Ok(regionDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateRegionAsync(
            [FromRoute] Guid id, [FromBody] Models.DTO.UpdateRegionRequest updateRegionRequest)
        {

            if (!ValidateUpdateAsync(updateRegionRequest))
            {
                return BadRequest(ModelState);
            }

            var region = new Models.Domain.Region()
            {
                State = updateRegionRequest.State,
                Code = updateRegionRequest.Code,
                City = updateRegionRequest.City,
                Population = updateRegionRequest.Population,
            };

            region = await _regionsRepository.UpdateASync(id, region);

            if (region == null)
            {
                return NotFound();
            }

            var regionDTO = _mapper.Map<Models.DTO.Region>(region);

            return Ok(regionDTO);
        }


        #region Private Methods

        private bool ValidateAddRegionAsync(Models.DTO.AddRegionRequest addRegionRequest)
        {

            if (addRegionRequest == null)
            {
                ModelState.AddModelError(nameof(addRegionRequest),
                    $"Add Region Data is required");
            }

            if (string.IsNullOrEmpty(addRegionRequest.State))
            {
                ModelState.AddModelError(nameof(addRegionRequest.State),
                      $"{nameof(addRegionRequest.State)} cannot be null or empty");
            }

            if (string.IsNullOrWhiteSpace(addRegionRequest.Code))
            {
                ModelState.AddModelError(nameof(addRegionRequest.Code),
                      $"{nameof(addRegionRequest.Code)} cannot be null or empty");
            }

            if (string.IsNullOrWhiteSpace(addRegionRequest.City))
            {
                ModelState.AddModelError(nameof(addRegionRequest.City),
                      $"{nameof(addRegionRequest.City)} cannot be null or empty");
            }

            if (addRegionRequest.Population < 0)
            {
                ModelState.AddModelError(nameof(addRegionRequest.Population),
                    $"{nameof(addRegionRequest.Population)} cannot be less than zero ");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }

        private bool ValidateUpdateAsync(Models.DTO.UpdateRegionRequest updateRegionRequest)
        {

            if (updateRegionRequest == null)
            {
                ModelState.AddModelError(nameof(updateRegionRequest),
                    $"Add Region Data is required");
                return false;
            }

            if (string.IsNullOrWhiteSpace(updateRegionRequest.State))
            {
                ModelState.AddModelError(nameof(updateRegionRequest.State),
                       $"{nameof(updateRegionRequest.State)} cannot be null or empty");
            }

            if (string.IsNullOrWhiteSpace(updateRegionRequest.Code))
            {
                ModelState.AddModelError(nameof(updateRegionRequest.Code),
                       $"{nameof(updateRegionRequest.Code)} cannot be null or empty");
            }

            if (string.IsNullOrWhiteSpace(updateRegionRequest.City))
            {
                ModelState.AddModelError(nameof(updateRegionRequest.City),
                       $"{nameof(updateRegionRequest.City)} cannot be null or empty");
            }

            if (updateRegionRequest.Population < 0)
            {
                ModelState.AddModelError(nameof(updateRegionRequest.Population),
                    $"{nameof(updateRegionRequest.Population)} cannot be less than zero");
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


