using DataAccess.Db;
using DataAccess.Db.DTOs;
using DataAccess.Db.Entities;
using DataAccess.Enums;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccess.Repositories
{
    public interface IPjatkRepository
    {
        Task<IEnumerable<AnimalDto>> GetAnimals(AnimalProperty orderBy);
        Task<bool> AddAnimal(AnimalDto animal);
        Task<bool> UpdateAnimal(int id, AnimalDto animal);
    }

    public class PjatkRepository : IPjatkRepository
    {
        private readonly PjatkDbContext _context;
        public PjatkRepository(PjatkDbContext context) 
        {
            _context = context;
        }

        public async Task<IEnumerable<AnimalDto>> GetAnimals(AnimalProperty orderBy) 
        {
            Expression<Func<Animal, string>> orderExpression = orderBy switch { 
                AnimalProperty.Name => animal => animal.Name,
                AnimalProperty.Description => animal => animal.Description,
                AnimalProperty.Category => animal => animal.Category,
                AnimalProperty.Area => animal => animal.Area,
                _ => throw new ArgumentException()
            };

            var entries = await _context.Animals
                .OrderBy(orderExpression)
                .ToListAsync();
            return entries.Select(entry => entry.ToDto());
        }

        public async Task<bool> AddAnimal(AnimalDto animal)
        {
            _context.Animals.Add(animal.ToEntity());

            var result = await _context.SaveChangesAsync();

            return result == 1;
        }

        public async Task<bool> UpdateAnimal(int id, AnimalDto animal)
        {
            var entity = await _context.Animals.FirstOrDefaultAsync(animal => animal.Id == id);
            if (entity == null)
                return false;

            if (animal.Name != null)
                entity.Name = animal.Name;
            if (animal.Description != null)
                entity.Description = animal.Description;
            if (animal.Category != null)
                entity.Category = animal.Category;
            if (animal.Area != null)
                entity.Area = animal.Area;

            _context.Animals.Update(entity);
            var result = await _context.SaveChangesAsync();

            return result == 1;
        }
    }
}
