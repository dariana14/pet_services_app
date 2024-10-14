using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    /// <inheritdoc />
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ServicePetCategoriesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly PublicDTOBllMapper<App.DTO.v1_0.ServicePetCategory, App.BLL.DTO.ServicePetCategory> _mapper;

        /// <inheritdoc />
        public ServicePetCategoriesController(IAppBLL bll, IMapper autoMapper)
        {
            _bll = bll;
            _mapper = new PublicDTOBllMapper<App.DTO.v1_0.ServicePetCategory, App.BLL.DTO.ServicePetCategory>(autoMapper);
        }

        // GET: api/ServicePetCategories
        /// <summary>
        /// Return all ServicePetCategory by serviceId
        /// </summary>
        /// <returns>list of ServicePetCategories</returns>
        [ProducesResponseType<IEnumerable<App.DTO.v1_0.Advertisement>>((int) HttpStatusCode.OK)]
        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpGet]
        public async Task<ActionResult<List<App.DTO.v1_0.ServicePetCategory>>> GetServicePetCategories(Guid serviceId)
        {
            var res = (await _bll.ServicePetCategories.GetAllByServiceIdAsync(serviceId))
                .Select(e => _mapper.Map(e))
                .ToList();
            return Ok(res);
        }

        // GET: api/ServicePetCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<App.DTO.v1_0.ServicePetCategory>> GetServicePetCategory(Guid id)
        {
            
            var servicePetCategory = _mapper.Map(await _bll.ServicePetCategories.FirstOrDefaultAsync(id));
            
            if (servicePetCategory == null)
            {
                return NotFound();
            }
            return servicePetCategory;
        }

        // PUT: api/ServicePetCategories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutServicePetCategory(Guid id, App.DTO.v1_0.ServicePetCategory servicePetCategory)
        {
            if (id != servicePetCategory.Id)
            {
                return BadRequest();
            }
            
            var bllServicePetCategory = _mapper.Map(servicePetCategory);
             _bll.ServicePetCategories.Update(bllServicePetCategory);

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServicePetCategoryExists(id))
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

        // POST: api/ServicePetCategories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<App.DTO.v1_0.ServicePetCategory>> PostServicePetCategory(App.DTO.v1_0.ServicePetCategory servicePetCategory)
        {
            var bllServicePetCategory = _mapper.Map(servicePetCategory);
            bllServicePetCategory!.Id = Guid.NewGuid();
            _bll.ServicePetCategories.Add(bllServicePetCategory);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetServicePetCategory", new { id = bllServicePetCategory.Id }, _mapper.Map(bllServicePetCategory));
        }

        // DELETE: api/ServicePetCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServicePetCategory(Guid id)
        {
            var servicePetCategory = await _bll.ServicePetCategories.FirstOrDefaultAsync(id);
            if (servicePetCategory == null)
            {
                return NotFound();
            }

            await _bll.ServicePetCategories.RemoveAsync(servicePetCategory);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private bool ServicePetCategoryExists(Guid id)
        {
            return _bll.ServicePetCategories.ExistsAsync(id).Result;
        }
    }
}
