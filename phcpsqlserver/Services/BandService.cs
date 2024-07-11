using Phc.Data;
using Phc.Data.Dto;
using Phc.Service.Interface;
using Microsoft.EntityFrameworkCore;
using Phc.Utilities;
using Phc.Exceptions;

// Class to represent domain logic for the Band entity

namespace Phc.Service
{
    public class BandService : IBandService
    {
        private PhcContext _context;

        public BandService(PhcContext context)
        {
            _context = context;
        }

        public async Task<List<BandResponseDto>> GetAllBandsAsync()
        {
            List<Band> bands = await _context.Bands.ToListAsync();
            List<BandResponseDto> payload = new List<BandResponseDto>();
            bands.ForEach(b => payload.Add(new BandResponseDto(){
                Id = b.Id,
                Name = b.Name,
                Genre = b.Genre,
                Formed = b.Formed 
                })
            );
            return payload;
        }

        public async Task<BandResponseDto> GetBandByIdAsync(long id)
        {
            Band b = await _context.Bands
                        .SingleOrDefaultAsync(b => b.Id == id)
                        ?? throw new BandNotFoundException($"Band with Id {id} not found");
            return new BandResponseDto(){
                Id = b.Id,
                Name = b.Name,
                Genre = b.Genre,
                Formed = b.Formed
            };
        }

        public async Task<BandResponseDto> AddBandAsync(BandInputDto band)
        {
            // error checking to ensure no band with the same name can be inserted
            string sanitisedBandName = StringSanitiser.Transform(band.Name);
            int count = await _context.Bands.CountAsync(b => b.Name.ToLower().TrimStart().TrimEnd() == sanitisedBandName);
            if(count > 0){
                throw new BandExistsException($"cannot add band, there is already a band with name {band.Name} in the db");
            }
            // constructing the band to save
            Band saveBand = new Band()
            {
                Id = 0,
                Name = band.Name,
                AddedOn = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
                LastUpdated = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
            };
            if(band.Formed != null){
                DateTime val = band.Formed ?? default; // it will never be default here, however i need to cast it to non nullable
                saveBand.Formed = DateTime.SpecifyKind(val, DateTimeKind.Utc);
            }
            await _context.Bands.AddAsync(saveBand);
            await _context.SaveChangesAsync();
            // now we return the representational state to the user
            return new BandResponseDto(){
                Id = saveBand.Id,
                Name = saveBand.Name,
                Genre = saveBand.Genre,
                Formed = saveBand.Formed
            };
        }

        public async Task DeleteBandByIdAsync(long id)
        {
            Band band = await _context.Bands
                        .Include(b => b.Albums)
                        .FirstOrDefaultAsync(b => b.Id == id)
                        ?? throw new BandNotFoundException($"Band with id {id} cannot be found");
            // Guarantee: Each album has a relation band, and cannot exist without a band relation.
            // we need to check if it has any albums, because if it does then you cannot delete.
            if(band.Albums.Count == 0){
                throw new BandNonEmptyException("The band currently has albums attached, first delete its albums");
            }
            _context.Bands.Remove(band);
            await _context.SaveChangesAsync();
        }

        public async Task/*<BandResponseDto>*/ UpdateBandAsync(long id, BandInputDto dto){
            // first ensure id is valid
            Band band = await _context.Bands
                        .Include(b => b.Albums)
                        .FirstAsync(b => b.Id == id)
                        ?? throw new BandNotFoundException($"band with id {id} cannot be found");
            // perform the changes
            band.Name = dto.Name;
            _context.Bands.Update(band);
            await _context.SaveChangesAsync();
            
        }
    }
}
