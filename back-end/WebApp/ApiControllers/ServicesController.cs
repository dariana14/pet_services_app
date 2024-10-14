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
    public class ServicesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly PublicDTOBllMapper<App.DTO.v1_0.Service, App.BLL.DTO.Service> _mapper;

        public ServicesController(IAppBLL bll, IMapper autoMapper)
        {
            _bll = bll;
            _mapper = new PublicDTOBllMapper<App.DTO.v1_0.Service, App.BLL.DTO.Service>(autoMapper);
        }

        // GET: api/Services
        [HttpGet]
        public async Task<ActionResult<List<App.DTO.v1_0.Service>>> GetServices()
        {
            var res = (await _bll.Services.GetAllAsync())
                .Select(e => _mapper.Map(e))
                .ToList();
            return Ok(res);
        }

        // GET: api/Services/5
        [HttpGet("{id}")]
        public async Task<ActionResult<App.DTO.v1_0.Service>> GetService(Guid id)
        {
            
            var service = _mapper.Map(await _bll.Services.FirstOrDefaultAsync(id));
            
            if (service == null)
            {
                return NotFound();
            }
            return service;
        }

        // PUT: api/Services/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutService(Guid id, App.DTO.v1_0.Service service)
        {
            if (id != service.Id)
            {
                return BadRequest();
            }
            
            var bllService = _mapper.Map(service);
             _bll.Services.Update(bllService);

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceExists(id))
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

        // POST: api/Services
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<App.DTO.v1_0.Service>> PostService(App.DTO.v1_0.Service service)
        {
            var bllService = _mapper.Map(service);
            bllService!.Id = Guid.NewGuid();
            _bll.Services.Add(bllService);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetService", new { id = bllService.Id }, _mapper.Map(bllService));
        }

        // DELETE: api/Services/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteService(Guid id)
        {
            var service = await _bll.Services.FirstOrDefaultAsync(id);
            if (service == null)
            {
                return NotFound();
            }

            await _bll.Services.RemoveAsync(service);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private bool ServiceExists(Guid id)
        {
            return _bll.Services.ExistsAsync(id).Result;
        }
    }
}
