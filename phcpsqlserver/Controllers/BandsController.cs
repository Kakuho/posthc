using Microsoft.AspNetCore.Mvc;
using Phc.Data.Dto;
using Phc.Data;
using Phc.Service.Interface;
using Phc.Exceptions;

namespace Phc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BandsController : ControllerBase
    {
        private readonly PhcContext _context;
        private readonly IBandService _bandservice;

        public BandsController(PhcContext context, IBandService bandservice)
        {
            _context = context;
            _bandservice = bandservice;
        }

        [HttpGet()]
        public async Task<ActionResult<List<Band>>> GetAllBands()
        {
            List<BandResponseDto> allbands = await _bandservice.GetAllBandsAsync();
            return Ok(allbands);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BandResponseDto>> GetBandById(long id)
        {
            try{
                BandResponseDto b = await _bandservice.GetBandByIdAsync(id);
                return Ok(b);
            }
            catch(BandNotFoundException e){
                
                return new JsonResult(new ErrorDto(){StatusCode = 404, Error = e.Message}){StatusCode = 404};
            }
        }

        [HttpPost]
        public async Task<ActionResult<BandResponseDto>> PostBand(BandInputDto band)
        {
            try{
                BandResponseDto saved = await _bandservice.AddBandAsync(band);
                return new CreatedAtActionResult(nameof(PostBand), "Bands", new { id = saved.Id }, saved);
            }
            catch(BandExistsException e){
                return new JsonResult(new ErrorDto(){StatusCode = 400, Error = e.Message}){StatusCode = 400};
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBand(int id)
        {
            try{
                await _bandservice.DeleteBandByIdAsync(id);
                return Ok($"band with id {id} successfully deleted");
            }
            catch(BandNotFoundException e){
                return new JsonResult(new ErrorDto(){StatusCode = 404, Error = e.Message}){StatusCode = 404};
            }
            catch(BandNonEmptyException e){
                return new JsonResult(new ErrorDto(){StatusCode = 400, Error = e.Message}){StatusCode = 400};
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBand(int id, [FromBody] BandInputDto album)
        {
            try{
                await _bandservice.UpdateBandAsync(id, album);
                return Ok();
            }
            catch(BandNotFoundException e){
                return new JsonResult(new ErrorDto(){StatusCode = 404, Error = e.Message}){StatusCode = 404};
            }
        }
    }
}
