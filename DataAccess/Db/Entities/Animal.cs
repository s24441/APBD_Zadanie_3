using DataAccess.Db.DTOs;

namespace DataAccess.Db.Entities
{
    public class Animal : IEquatable<AnimalDto>
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual string Category { get; set; }
        public virtual string Area { get; set; }

        public bool Equals(AnimalDto? other) => other != null && 
            Name == other.Name 
            && 
            Description == other.Description 
            && 
            Category == other.Category 
            && 
            Area == other.Area;

        public AnimalDto ToDto() => new AnimalDto() { Name = Name, Description = Description, Category = Category, Area = Area };

        public Animal UpdateFromDto(AnimalDto dto)
        {
            if (dto.Name != null && Name != dto.Name)
                Name = dto.Name;
            if (dto.Description != null && Description != dto.Description)
                Description = dto.Description;
            if (dto.Category != null && Category != dto.Category)
                Category = dto.Category;
            if (dto.Area != null && Area != dto.Area)
                Area = dto.Area;

            return this;
        }
    }
}
