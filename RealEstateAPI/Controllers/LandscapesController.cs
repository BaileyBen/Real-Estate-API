using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstateAPI.Repositories;
using System.Data;

namespace RealEstateAPI.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class LandscapesController : Controller
    {
        private readonly ILandscapesRepository _landscapesRepository;
        private readonly IMapper _mapper;

        public LandscapesController(ILandscapesRepository landscapesRepository, IMapper mapper)
        {
            _landscapesRepository = landscapesRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAllLandscapesAsync()
        {
            var landscapes = await _landscapesRepository.GetAllAsync();

            var landscapesDTO = _mapper.Map<List<Models.DTO.Landscape>>(landscapes);

            return Ok(landscapesDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetLandscapeById")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetLandscapeById(Guid id)
        {
            var landscape = await _landscapesRepository.GetAsync(id);

            if (landscape == null)
            {
                return NotFound();
            }

            var landscapeDTO = _mapper.Map<Models.DTO.Landscape>(landscape);

            return Ok(landscapeDTO);
        }

        [HttpPost]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> AddLandscapeAsync(Models.DTO.AddLandscapeRequest landscapeRequest)
        {

            if (!ValidateAddLandscapeAsync(landscapeRequest))
            {
                return BadRequest(ModelState);
            }

            var landscapeDomain = new Models.Domain.Landscape()
            {
                Type = landscapeRequest.Type
            };

            var landscape = await _landscapesRepository.AddAsync(landscapeDomain);

            var landscapeDTO = _mapper.Map<Models.DTO.Landscape>(landscapeDomain);

            return CreatedAtAction(nameof(GetLandscapeById), new { id = landscapeDTO.Id }, landscapeDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> UpdateLandscapeAsync(Guid id, Models.DTO.UpdateLandscapeRequest updateLandscapeRequest)
        {

            if (!ValidateUpdateLandscape(updateLandscapeRequest))
            {
                return BadRequest(ModelState);
            }

            var landscapeDomain = new Models.Domain.Landscape()
            {
                Type = updateLandscapeRequest.Type
            };

            landscapeDomain = await _landscapesRepository.UpdateAsync(id, landscapeDomain);

            if (landscapeDomain == null)
            {
                return NotFound();
            }

            var landscapeDTO = _mapper.Map<Models.DTO.Landscape>(landscapeDomain);

            return Ok(landscapeDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> DeleteLandscapeAsync(Guid id)
        {
            var landscapeDomain = await _landscapesRepository.DeleteAsync(id);

            if (landscapeDomain == null)
            {
                return NotFound();
            }

            var landscapeDTO = _mapper.Map<Models.DTO.Landscape>(landscapeDomain);

            return Ok(landscapeDTO);

        }


        #region Private Methods

        private bool ValidateAddLandscapeAsync(Models.DTO.AddLandscapeRequest landscapeRequest)
        {
            if (landscapeRequest == null)
            {
                ModelState.AddModelError(nameof(landscapeRequest),
                    $"{nameof(landscapeRequest)} Is Required");
                return false;
            }

            if (string.IsNullOrWhiteSpace(landscapeRequest.Type))
            {
                ModelState.AddModelError(nameof(landscapeRequest),
                   $"{nameof(landscapeRequest)} Is Required");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }

        private bool ValidateUpdateLandscape(Models.DTO.UpdateLandscapeRequest updateLandscapeRequest)
        {
            if (updateLandscapeRequest == null)
            {
                ModelState.AddModelError(nameof(updateLandscapeRequest),
                    $"{nameof(updateLandscapeRequest)} Is Required");
                return false;
            }

            if (string.IsNullOrWhiteSpace(updateLandscapeRequest.Type))
            {
                ModelState.AddModelError(nameof(updateLandscapeRequest),
                   $"{nameof(updateLandscapeRequest)} Is Required");
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
