using DataAccess.Db.Entities;

namespace DataAccess.Db.DTOs
{
    public class AnimalDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
        public string? Area { get; set; }

        public Animal ToEntity(int id = default) => 
            new Animal() { Id = id, Name = Name, Description = Description, Category = Category, Area = Area };
    }
}
