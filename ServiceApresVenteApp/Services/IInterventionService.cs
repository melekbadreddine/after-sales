using ServiceApresVente.Models;
using ServiceApresVenteApp.Models;
using ServiceApresVenteApp.ViewModels;

public interface IInterventionService
{
    Task<List<Intervention>> GetAllInterventionsAsync();
    Task<Intervention> GetInterventionDetailsAsync(int id);
    Task<List<IntervensionPiece>> GetInterventionPiecesAsync(int interventionId);
    Task AddInterventionAsync(Intervention intervention);
    Task UpdateInterventionAsync(int id, Intervention intervention, List<PieceViewModel> pieces);
    Task DeleteInterventionAsync(int id);
}
