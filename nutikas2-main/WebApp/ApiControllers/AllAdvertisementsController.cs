using App.Contracts.BLL;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using AutoMapper;
using WebApp.Helpers;

namespace WebApp.ApiControllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AllAdvertisementsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly PublicDTOBllMapper<App.DTO.v1_0.Advertisement, App.BLL.DTO.Advertisement> _mapper;

        public AllAdvertisementsController(IAppBLL bll, IMapper autoMapper)
        {
            _bll = bll;
            _mapper = new PublicDTOBllMapper<App.DTO.v1_0.Advertisement, App.BLL.DTO.Advertisement>(autoMapper);
        }

        // GET: api/Advertisements
        [HttpGet]
        public async Task<ActionResult<List<App.DTO.v1_0.Advertisement>>> GetAdvertisements()
        {
            var res = (await _bll.Advertisements.GetAllAsync())
                .Select(e => _mapper.Map(e))
                .ToList();
            return Ok(res);
        }
        
        
        [HttpGet("{id}")]
        public async Task<ActionResult<App.DTO.v1_0.Advertisement>> GetAdvertisement(Guid id)
        {
            var advertisement = _mapper.Map(await _bll.Advertisements.FirstOrDefaultAsync(id));
            
            if (advertisement == null)
            {
                return NotFound();
            }
            return advertisement;
        }

    }
}
