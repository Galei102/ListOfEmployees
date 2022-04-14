using ListOfEmployees.Data;
using ListOfEmployees.Models;
using ListOfEmployees.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ListOfEmployees.Repositorys
{
    public class SubdivisionRepository : ISubdivisionRepository
    {
        private readonly ApplicationDbContext db;

        public SubdivisionRepository(ApplicationDbContext _db)
        {
            db = _db;
        }
        public async Task CreateAsync(Subdivision subdivision)
        {
            db.Subdivisions.Add(subdivision);
            await db.SaveChangesAsync();
        }

        public async Task<List<Subdivision>> ListSubdivisions(int id)
        {
            return await db.Subdivisions
                .Where(s => s.OrganizationId == id)
                .OrderByDescending(s => s.Id)
                .ToListAsync();
        }

        public async Task UpdateAsync(Subdivision subdivision)
        {
            db.Subdivisions.Update(subdivision);
            await db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Subdivision subdivision)
        {
            db.Subdivisions.Remove(subdivision);
            await db.SaveChangesAsync();
        }

        public async Task<Subdivision> GetSubdivision(int id)
        {
            return await db.Subdivisions.FindAsync(id);
        }
    }
}
