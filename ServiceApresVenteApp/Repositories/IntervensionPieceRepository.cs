using Microsoft.EntityFrameworkCore;
using ServiceApresVenteApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceApresVenteApp.Repositories
{
    public class IntervensionPieceRepository : IIntervensionPieceRepository
    {
        private readonly ApplicationDbContext _context;

        public IntervensionPieceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<IntervensionPiece>> GetByInterventionIdAsync(int interventionId)
        {
            return await _context.Set<IntervensionPiece>()
                .Where(ip => ip.InterventionId == interventionId)
                .ToListAsync();
        }

        public async Task AddAsync(IntervensionPiece intervensionPiece)
        {
            await _context.Set<IntervensionPiece>().AddAsync(intervensionPiece);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveByInterventionIdAsync(int interventionId)
        {
            var intervensionPieces = await _context.Set<IntervensionPiece>()
                .Where(ip => ip.InterventionId == interventionId)
                .ToListAsync();

            _context.Set<IntervensionPiece>().RemoveRange(intervensionPieces);
            await _context.SaveChangesAsync();
        }
    }
}
