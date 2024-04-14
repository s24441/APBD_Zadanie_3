using DataAccess.Db.DTOs;

namespace DataAccess.Db.Entities
{
    public class Animal
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual string Category { get; set; }
        public virtual string Area { get; set; }

        public AnimalDto ToDto() => new AnimalDto() { Name = Name, Description = Description, Category = Category, Area = Area };
    }
}
