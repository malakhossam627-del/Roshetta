using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rosetta.DAL.Entites.Catalog
{
    public class Ingredient
    {
        public int IngredientId { get; set; }
        public string NameEn { get; set; } = string.Empty;
        public string NameAr { get; set; } = string.Empty;

        public virtual ICollection<MedicineIngredient> MedicineIngredients { get; set; } = new HashSet<MedicineIngredient>();
        public virtual ICollection<Interaction.Interaction> InteractionsAsIngredientA { get; set; } = new HashSet<Interaction.Interaction>();
        public virtual ICollection<Interaction.Interaction> InteractionsAsIngredientB { get; set; } = new HashSet<Interaction.Interaction>();
        public virtual ICollection<Interaction.CheckItemIngredient> CheckItemIngredients { get; set; } = new HashSet<Interaction.CheckItemIngredient>();
    }
}
