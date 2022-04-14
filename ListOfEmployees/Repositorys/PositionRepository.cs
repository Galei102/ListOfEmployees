using ListOfEmployees.Interfaces;
using ListOfEmployees.Models;
using ListOfEmployees.Data;
using Microsoft.EntityFrameworkCore;

namespace ListOfEmployees.Repositorys
{
    public class PositionRepository : IPositionRepository
    {
        private readonly ApplicationDbContext db;

        public PositionRepository(ApplicationDbContext _db)
        {
            db = _db;
        }

        public async Task CreateAsync(Position position)
        {
            db.Positions.Add(position);
            await db.SaveChangesAsync();
        }

        public async Task<List<Position>> ListPositions(int id)
        {
            return await db.Positions
                .Where(p => p.SubdivisionId == id)
                .OrderByDescending(p => p.Id)
                .ToListAsync();           
        }

        public async Task UpdateAsync(Position position)
        {
            db.Positions.Update(position);
            await db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Position position)
        {
            db.Positions.Remove(position);
            await db.SaveChangesAsync();
        }

        public async Task<Position> GetPosition(int id)
        {
            return await db.Positions.FindAsync(id);
        }

        public async Task<List<Position>> GetAllAsync()
        {
            return await db.Positions.ToListAsync();
        }
    }
}
