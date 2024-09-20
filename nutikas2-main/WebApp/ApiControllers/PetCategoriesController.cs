using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Contracts.BLL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain;
using Asp.Versioning;
using AutoMapper;
using WebApp.Helpers;

namespace WebApp.ApiControllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PetCategoriesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly PublicDTOBllMapper<App.DTO.v1_0.PetCategory, App.BLL.DTO.PetCategory> _mapper;

        public PetCategoriesController(IAppBLL bll, IMapper autoMapper)
        {
            _bll = bll;
            _mapper = new PublicDTOBllMapper<App.DTO.v1_0.PetCategory, App.BLL.DTO.PetCategory>(autoMapper);
        }

        // GET: api/PetCategories
        [HttpGet]
        public async Task<ActionResult<List<App.DTO.v1_0.PetCategory>>> GetPetCategories()
        {
            var res = (await _bll.PetCategories.GetAllAsync())
                .Select(e => _mapper.Map(e))
                .ToList();
            return Ok(res);
        }

        // GET: api/PetCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<App.DTO.v1_0.PetCategory>> GetPetCategory(Guid id)
        {
            
            var petCategory = _mapper.Map(await _bll.PetCategories.FirstOrDefaultAsync(id));
            
            if (petCategory == null)
            {
                return NotFound();
            }
            return petCategory;
        }

        // PUT: api/PetCategories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPetCategory(Guid id, App.DTO.v1_0.PetCategory petCategory)
        {
            if (id != petCategory.Id)
            {
                return BadRequest();
            }
            
            var bllPetCategory = _mapper.Map(petCategory);
             _bll.PetCategories.Update(bllPetCategory);

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PetCategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/PetCategories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<App.DTO.v1_0.PetCategory>> PostPetCategory(App.DTO.v1_0.PetCategory petCategory)
        {
            var bllPetCategory = _mapper.Map(petCategory);
            bllPetCategory!.Id = new Guid();
            _bll.PetCategories.Add(bllPetCategory);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetPetCategory", new { id = petCategory.Id }, petCategory);
        }

        // DELETE: api/PetCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePetCategory(Guid id)
        {
            var petCategory = await _bll.PetCategories.FirstOrDefaultAsync(id);
            if (petCategory == null)
            {
                return NotFound();
            }

            await _bll.PetCategories.RemoveAsync(petCategory);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private bool PetCategoryExists(Guid id)
        {
            return _bll.PetCategories.ExistsAsync(id).Result;
        }
    }
}
