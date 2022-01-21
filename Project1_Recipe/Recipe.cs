using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Project1_Recipe
{
    public class Recipe
    {
        public string Name { get; set; }

        public List<Ingredient>? Ingredients { get; set; }

        public NutritionalValue? NutritionalValue { get; set; }

        public override string ToString()
        {
            var recipe = " " + Name + "\n   Ingredients: ";
            foreach (Ingredient ingredient in Ingredients)
            {
                recipe += "\n     " + ingredient.Amount + ingredient.Unit + " of " + ingredient.Name + ".";
            }
            recipe += "\n   Nutritional Value: ";
            recipe += $"\n     Proteins = {NutritionalValue.Proteins}." + $"\n     Fats = {NutritionalValue.Fats}." + $"\n     Carbohydrates = {NutritionalValue.Carbohydrates}.\n";
            return recipe;
        }
    }
}
