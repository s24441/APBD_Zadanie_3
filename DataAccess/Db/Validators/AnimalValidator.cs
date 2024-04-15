using DataAccess.Db.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Db.Validators
{
    public static class AnimalValidator
    {
        public static bool NotValid(this Animal animal) => 
            animal.Name == null || animal.Description == null || animal.Category == null || animal.Area == null;
    }
}
