using Phc.Data;
using Phc.Data.Dto;
using Phc.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace Phc.Service
{
    public class BandService : IBandService
    {
        private PhcContext _context;

        public BandService(PhcContext context)
        {
            _context = context;
        }

        public async Task<Band> GetBandByIdAsync(long id)
        {
            Band b = _context.Bands.SingleOrDefault(b => b.Id == id);
            return b;
        }

        public async Task<Band> GetBandByNameAsync(string name)
        {
            try{
              Console.WriteLine(name);
              Band output = await _context.Bands
                  .FirstOrDefaultAsync(b => b.Name == name);
              if(output is null){
                // throw a guy
              }
              return output;
            }
            catch{
              return null;
            }
        }


        public Task<List<Band>> GetAllBands()
        {
            return _context.Bands.ToListAsync();
        }

        public Band AddBand(Band band)
        {
            Band saveBand = new Band()
            {
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

        public Band AddBand(BandDto band)
        {
            Band saveBand = new Band()
            {
                Id = 0, // this does not matter
                Name = band.Name,
                Formed = DateTime.SpecifyKind(band.Formed, DateTimeKind.Utc),
                AddedOn = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
                LastModified = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
            };
            _context.Bands.Add(saveBand);
            _context.SaveChanges();
            return saveBand;
        }

        public async Task<bool> DeleteBand(string name)
        {
            Band band = await _context.Bands.FirstAsync(b => b.Name == name);
            if (band is null)
            {
                return false;
            }
            else
            {
                _context.Bands.Remove(band);
                _context.SaveChanges();
                return true;
            }
        }
    }
}
