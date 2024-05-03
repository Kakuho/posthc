using Phc.Data;
using Phc.Data.Dto;
using Phc.Service.Interface;

namespace Phc.Service{
  public class BandService: IBandService{
    private PhcContext _context;

    public BandService(PhcContext context){
      _context = context;
    }

    public async Task<Band> GetBandByIdAsync(long id){
      Band b = _context.Bands.SingleOrDefault(b => b.Id == id);
      return b;
    }

    public Band AddBand(Band band){
      Band saveBand = new Band(){
        Id = band.Id,
        Name = band.Name,
        Formed = DateTime.SpecifyKind(band.Formed, DateTimeKind.Utc),
        AddedOn = DateTime.SpecifyKind(band.AddedOn, DateTimeKind.Utc),
        LastModified = DateTime.SpecifyKind(band.LastModified, DateTimeKind.Utc),
      };
      _context.Bands.Add(saveBand);
      _context.SaveChanges();
      return saveBand;
    }

    public Band AddBand(BandDto band){
      Band saveBand = new Band(){
        Id = 0,
        Name = band.Name,
        Formed = DateTime.SpecifyKind(band.Formed, DateTimeKind.Utc),
        AddedOn = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
        LastModified = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
      };
      _context.Bands.Add(saveBand);
      _context.SaveChanges();
      return saveBand;
    }
  }
}
