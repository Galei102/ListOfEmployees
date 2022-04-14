using ListOfEmployees.Interfaces;
using ListOfEmployees.Models;
using ListOfEmployees.ViewModels;
using ListOfEmployees.Data;
using Microsoft.EntityFrameworkCore;

namespace ListOfEmployees.Repositorys
{
    public class OrganizationRepository : IOrganizationRepository
    {
        private readonly ApplicationDbContext db;

        public OrganizationRepository(ApplicationDbContext _db)
        {
            db = _db;
        }
        public async Task CreateAsync(Organization organization)
        {
            db.Add(organization);
            await db.SaveChangesAsync();
        }

        public async Task<List<Organization>> GetAllAsync()
        {
            return await db.Organizations
                .OrderByDescending(o => o.Id)
                .ToListAsync();                                         //Выводим список организации по убыванию
        }
                
        public async Task UpdateAsync(Organization organization)
        {
            db.Organizations.Update(organization);
            await db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Organization organization)
        {
            db.Organizations.Remove(organization);
            await db.SaveChangesAsync();
        }

        public async Task<Organization> GetOrganization(int id)
        {
            return await db.Organizations.FindAsync(id);
        }
    }
}
