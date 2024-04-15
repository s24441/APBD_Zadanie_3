using DataAccess.Db;
using DataAccess.Db.DTOs;
using DataAccess.Db.Entities;
using DataAccess.Db.Validators;
using DataAccess.Enums;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccess.Repositories
{
    public interface IPjatkRepository
    {
        Task<int?> GetIdByName(string name, CancellationToken cancellationToken = default);

        Task<IEnumerable<AnimalDto>> GetAnimals(AnimalProperty? orderBy, CancellationToken cancellationToken = default);
        Task<bool> AddAnimal(AnimalDto animal, CancellationToken cancellationToken = default);
        Task<bool?> UpdateAnimal(int id, AnimalDto animal, CancellationToken cancellationToken = default);
        Task<bool> DeleteAnimal(int id, CancellationToken cancellationToken = default);
    }

    public class PjatkRepository : IPjatkRepository
    {
        private readonly PjatkDbContext _context;
        public PjatkRepository(PjatkDbContext context) 
        {
            _context = context;
        }

        public async Task<int?> GetIdByName(string name, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(name))
                return default;

            return (await _context.Animals.FirstOrDefaultAsync(animal => animal.Name.Contains(name), cancellationToken))?.Id;
        }

        public async Task<IEnumerable<AnimalDto>> GetAnimals(AnimalProperty? orderBy, CancellationToken cancellationToken = default) 
        {
            Expression<Func<Animal, string>>? orderExpression = orderBy switch { 
                AnimalProperty.Name => animal => animal.Name,
                AnimalProperty.Description => animal => animal.Description,
                AnimalProperty.Category => animal => animal.Category,
                AnimalProperty.Area => animal => animal.Area,
                _ => default
            };

            var query = orderExpression != default ? _context.Animals.OrderBy(orderExpression) : _context.Animals.AsQueryable();

            var entries = await query.ToListAsync(cancellationToken);

            return entries.Select(entry => entry.ToDto());
        }

        public async Task<bool> AddAnimal(AnimalDto animal, CancellationToken cancellationToken = default)
        {
            if (animal == null || animal.NotValid()) 
                return false;

            var entity = animal.ToEntity();

            _context.Animals.Add(entity);

            var result = await _context.SaveChangesAsync(cancellationToken);

            return result == 1;
        }

        public async Task<bool?> UpdateAnimal(int id, AnimalDto animal, CancellationToken cancellationToken = default)
        {
            var entity = await _context.Animals
                .FirstOrDefaultAsync(animal => animal.Id == id, cancellationToken);

            if (entity == null)
                return default;

            if (animal.NotValid() || entity.Equals(animal))
                return false;

            _context.Animals.Update(entity.UpdateFromDto(animal));

            var result = await _context.SaveChangesAsync(cancellationToken);

            return result == 1;
        }

        public async Task<bool> DeleteAnimal(int id, CancellationToken cancellationToken = default)
        {
            var entity = await _context.Animals
                .FirstOrDefaultAsync(animal => animal.Id == id, cancellationToken);

            if (entity == null)
                return false;

            _context.Animals.Remove(entity);

            var result = await _context.SaveChangesAsync(cancellationToken);

            return result == 1;
        }
    }
}
