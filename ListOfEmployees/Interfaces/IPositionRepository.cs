using ListOfEmployees.Models;

namespace ListOfEmployees.Interfaces
{
    public interface IPositionRepository
    {
        Task CreateAsync(Position position);

        Task<List<Position>> ListPositions(int id);

        Task UpdateAsync(Position position);

        Task DeleteAsync(Position position);

        Task<Position> GetPosition(int id);

        Task<List<Position>> GetAllAsync();
    }
}
