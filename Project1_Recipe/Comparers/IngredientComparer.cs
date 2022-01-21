namespace Project1_Recipe.Comparers
{
    public class IngredientComparer : IComparer<Recipe>
    {
        public int Compare(Recipe recipe1, Recipe recipe2)
        {
            var finalAmount1 = SumAmount(recipe1.Ingredients);
            var finalAmount2 = SumAmount(recipe2.Ingredients);
            if (finalAmount1 > finalAmount2)
            {
                return 1;
            }
            else if (finalAmount1 < finalAmount2)
            {
                return -1;
            }
            else
            {
                return 0;
            }

        }

        public double SumAmount(List<Ingredient> ingredients)
        {
            var res = 0.0;
            foreach (var ingredient in ingredients)
            {
                var unit = ingredient.Unit;
                if (unit == "g" || unit == "ml")
                {
                    res += ingredient.Amount;
                }
                else if (unit == "kg" || unit == "l")
                {
                    res += ingredient.Amount * 1000;
                }
                else if (unit == "spoon")
                {
                    res += ingredient.Amount * 15;
                }
                else if (unit == "teaspoon")
                {
                    res += ingredient.Amount * 6;
                }
            }
            return res;
        }
    }
}
