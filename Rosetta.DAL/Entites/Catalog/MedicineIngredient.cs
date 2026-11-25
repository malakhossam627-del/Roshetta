using System;
using System.Collections.Generic;
using System.Text;

namespace Rosetta.DAL.Entites.Catalog
{
    public class MedicineIngredient
    {
        public int MedicineId { get; set; }
        public int IngredientId { get; set; }
        public string? IngredientStrength { get; set; }

        public virtual Medicine Medicine { get; set; } = null!;
        public virtual Ingredient Ingredient { get; set; } = null!;
    }
}
