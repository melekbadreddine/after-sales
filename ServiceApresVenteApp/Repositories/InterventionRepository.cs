using Microsoft.EntityFrameworkCore;
using ServiceApresVente.Models;
using ServiceApresVenteApp.Models;

public class InterventionRepository : IInterventionRepository
{
    private readonly ApplicationDbContext _context;

    public InterventionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Intervention>> GetAllAsync()
    {
        return await _context.Interventions.Include(i => i.Reclamation).ToListAsync();
    }

    public async Task<Intervention> GetByIdAsync(int id)
    {
        return await _context.Interventions
            .Include(i => i.Reclamation)
            .FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<List<IntervensionPiece>> GetInterventionPiecesAsync(int interventionId)
    {
        return await _context.Set<IntervensionPiece>()
            .Where(ip => ip.InterventionId == interventionId)
            .ToListAsync();
    }

    public async Task AddAsync(Intervention intervention)
    {
        await _context.Interventions.AddAsync(intervention);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Intervention intervention)
    {
        _context.Interventions.Update(intervention);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var intervention = await GetByIdAsync(id);
        if (intervention != null)
        {
            _context.Interventions.Remove(intervention);
            await _context.SaveChangesAsync();
        }
    }
}
