using ListOfEmployees.ViewModels;
using ListOfEmployees.Models;

namespace ListOfEmployees.Interfaces
{
    public interface IOrganizationRepository
    {
        Task CreateAsync(Organization organization);

        Task<List<Organization>> GetAllAsync();

        Task UpdateAsync(Organization organization);

        Task DeleteAsync(Organization organization);

        Task<Organization> GetOrganization(int id);
    }
}
