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
    public class StatusesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly PublicDTOBllMapper<App.DTO.v1_0.Status, App.BLL.DTO.Status> _mapper;

        /// <inheritdoc />
        public StatusesController(IAppBLL bll, IMapper autoMapper)
        {
            _bll = bll;
            _mapper = new PublicDTOBllMapper<App.DTO.v1_0.Status, App.BLL.DTO.Status>(autoMapper);
        }

        // GET: api/Statuses
        /// <summary>
        /// Return all statuses by name
        /// </summary>
        /// <returns>list of Statuses</returns>
        [HttpGet]
        [ProducesResponseType<IEnumerable<App.DTO.v1_0.Advertisement>>((int) HttpStatusCode.OK)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<List<App.DTO.v1_0.Status>>> GetStatuses()
        {
            var res = (await _bll.Statuses.GetAllAsync())
                            .Select(e => _mapper.Map(e))
                            .ToList();
            return Ok(res);
        }

        // GET: api/Statuses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<App.DTO.v1_0.Status>> GetStatus(Guid id)
        {
            
            var status = _mapper.Map(await _bll.Statuses.FirstOrDefaultAsync(id));
            
            if (status == null)
            {
                return NotFound();
            }
            return status;
        }

        // PUT: api/Statuses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStatus(Guid id, App.DTO.v1_0.Status status)
        {
            if (id != status.Id)
            {
                return BadRequest();
            }
            
            var bllStatus = _mapper.Map(status);
             _bll.Statuses.Update(bllStatus);

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StatusExists(id))
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

        // POST: api/Statuses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<App.DTO.v1_0.Status>> PostStatus(App.DTO.v1_0.Status status)
        {
            _bll.Statuses.Add(_mapper.Map(status));
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetStatus", new { id = status.Id }, status);
        }

        // DELETE: api/Statuses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStatus(Guid id)
        {
            var status = await _bll.Statuses.FirstOrDefaultAsync(id);
            if (status == null)
            {
                return NotFound();
            }

            await _bll.Statuses.RemoveAsync(status);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private bool StatusExists(Guid id)
        {
            return _bll.Statuses.ExistsAsync(id).Result;
        }
    }
}
