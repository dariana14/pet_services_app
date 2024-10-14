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
    public class PricesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly PublicDTOBllMapper<App.DTO.v1_0.Price, App.BLL.DTO.Price> _mapper;


        public PricesController(IAppBLL bll, IMapper autoMapper)
        {
            _bll = bll;
            _mapper = new PublicDTOBllMapper<App.DTO.v1_0.Price, App.BLL.DTO.Price>(autoMapper);
        }

        // GET: api/Prices
        [HttpGet]
        public async Task<ActionResult<List<App.DTO.v1_0.Price>>> GetPrices()
        {
            var res = (await _bll.Prices.GetAllAsync())
                .Select(e => _mapper.Map(e))
                .ToList();
            return Ok(res);
        }

        // GET: api/Prices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<App.DTO.v1_0.Price>> GetPrice(Guid id)
        {
            
            var price = _mapper.Map(await _bll.Prices.FirstOrDefaultAsync(id));
            
            if (price == null)
            {
                return NotFound();
            }
            return price;
        }

        // PUT: api/Prices/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrice(Guid id, App.DTO.v1_0.Price price)
        {
            if (id != price.Id)
            {
                return BadRequest();
            }
            
            var bllPrice = _mapper.Map(price);
             _bll.Prices.Update(bllPrice);

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PriceExists(id))
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

        // POST: api/Prices
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<App.DTO.v1_0.Price>> PostPrice(App.DTO.v1_0.Price price)
        {
            var bllPrice = _mapper.Map(price);
            bllPrice!.Id = Guid.NewGuid();
            _bll.Prices.Add(bllPrice);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetPrice", new { id = bllPrice.Id }, _mapper.Map(bllPrice));
        }

        // DELETE: api/Prices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrice(Guid id)
        {
            var price = await _bll.Prices.FirstOrDefaultAsync(id);
            if (price == null)
            {
                return NotFound();
            }

            await _bll.Prices.RemoveAsync(price);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private bool PriceExists(Guid id)
        {
            return _bll.Prices.ExistsAsync(id).Result;
        }
    }
}