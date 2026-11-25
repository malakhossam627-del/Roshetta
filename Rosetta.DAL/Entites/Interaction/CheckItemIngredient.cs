using Rosetta.DAL.Entites.Catalog;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rosetta.DAL.Entites.Interaction
{
    public class CheckItemIngredient
    {
        public int CheckItemId { get; set; }
        public int IngredientId { get; set; }

        public virtual InteractionCheckItem CheckItem { get; set; } = null!;
        public virtual Ingredient Ingredient { get; set; } = null!;
    }
}
