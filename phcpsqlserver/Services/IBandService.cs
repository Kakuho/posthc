using Phc.Data;
using Phc.Data.Dto;

namespace Phc.Service.Interface
{
    public interface IBandService
    {
        public Task<Band> GetBandByIdAsync(long id);
        public Task<Band> GetBandByNameAsync(string name);
        public Task<List<Band>> GetAllBands();
        public Band AddBand(Band band);
        public Band AddBand(BandDto band);
        public Task<bool> DeleteBand(string name);
    }
}
