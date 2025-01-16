using ServiceApresVenteApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceApresVenteApp.Repositories
{
    public interface IIntervensionPieceRepository
    {
        Task<IEnumerable<IntervensionPiece>> GetByInterventionIdAsync(int interventionId);
        Task AddAsync(IntervensionPiece intervensionPiece);
        Task RemoveByInterventionIdAsync(int interventionId);
    }
}
