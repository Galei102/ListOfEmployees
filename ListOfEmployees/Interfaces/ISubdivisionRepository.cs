using ListOfEmployees.Models;

namespace ListOfEmployees.Interfaces
{
    public interface ISubdivisionRepository
    {
        Task CreateAsync(Subdivision subdivision);

        Task<List<Subdivision>> ListSubdivisions(int id);

        Task UpdateAsync(Subdivision subdivision);

        Task DeleteAsync(Subdivision subdivision);

        Task<Subdivision> GetSubdivision(int id);
    }
}
