using DataAccess.Db.DTOs;

namespace DataAccess.Db.Validators
{
    public static class AnimalDtoValidator
    {
        public static bool NotValid(this AnimalDto animal) =>
            animal.Name == null && animal.Description == null && animal.Category == null && animal.Area == null;
    }
}
