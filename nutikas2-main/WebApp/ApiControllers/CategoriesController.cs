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
    public class CategoriesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly PublicDTOBllMapper<App.DTO.v1_0.Category, App.BLL.DTO.Category> _mapper;

        public CategoriesController(IAppBLL bll, IMapper autoMapper)
        {
            _bll = bll;
            _mapper = new PublicDTOBllMapper<App.DTO.v1_0.Category, App.BLL.DTO.Category>(autoMapper);
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<List<App.DTO.v1_0.Category>>> GetCategories()
        {
            var res = (await _bll.Categories.GetAllAsync())
                .Select(e => _mapper.Map(e))
                .ToList();
            return Ok(res);
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<App.DTO.v1_0.Category>> GetCategory(Guid id)
        {
            
            var category = _mapper.Map(await _bll.Categories.FirstOrDefaultAsync(id));
            
            if (category == null)
            {
                return NotFound();
            }
            return category;
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(Guid id, App.DTO.v1_0.Category category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }
            
            var bllCategory = _mapper.Map(category);
             _bll.Categories.Update(bllCategory);

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
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

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<App.DTO.v1_0.Category>> PostCategory(App.DTO.v1_0.Category category)
        {
            var bllCategory = _mapper.Map(category);
            bllCategory!.Id = new Guid();
            _bll.Categories.Add(bllCategory);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetCategory", new { id = category.Id }, category);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            var category = await _bll.Categories.FirstOrDefaultAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            await _bll.Categories.RemoveAsync(category);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoryExists(Guid id)
        {
            return _bll.Categories.ExistsAsync(id).Result;
        }
    }
}
