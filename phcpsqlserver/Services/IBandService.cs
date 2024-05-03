using Phc.Data;
using Phc.Data.Dto;

namespace Phc.Service.Interface{
  public interface IBandService
  {
    public Task<Band> GetBandByIdAsync(long id);
    public IEnumerable<Band> GetAllBands();
    public Band AddBand(Band band);
    public Band AddBand(BandDto band);
  }
}
