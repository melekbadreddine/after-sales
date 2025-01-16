using ServiceApresVenteApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceApresVenteApp.Services
{
    public interface IIntervensionPieceService
    {
        Task AddPiecesToInterventionAsync(int interventionId, List<IntervensionPiece> pieces);
        Task<IEnumerable<IntervensionPiece>> GetPiecesByInterventionAsync(int interventionId);
    }
}
