using ServiceApresVente.Models;

public interface IInterventionRepository
{
    Task<List<Intervention>> GetAllAsync();
    Task<Intervention> GetByIdAsync(int id);
    Task AddAsync(Intervention intervention);
    Task UpdateAsync(Intervention intervention);
    Task DeleteAsync(int id);
}
