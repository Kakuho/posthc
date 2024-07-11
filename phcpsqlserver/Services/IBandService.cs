using Phc.Data;
using Phc.Data.Dto;

namespace Phc.Service.Interface
{
    public interface IBandService
    {
        public Task<List<BandResponseDto>> GetAllBandsAsync();
        public Task<BandResponseDto> GetBandByIdAsync(long id);
        public Task<BandResponseDto> AddBandAsync(BandInputDto band);
        public Task DeleteBandByIdAsync(long id);
        //public Task<BandResponseDto> UpdateBandAsync(long id, BandInputDto band);
        public Task/*<BandResponseDto>*/ UpdateBandAsync(long id, BandInputDto band);
    }
}
