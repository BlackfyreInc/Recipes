using Project1_Recipe.Comparers;

namespace Project1_Recipe
{
    public class RecipeJournal
    {
        public List<Recipe> Recipes = new List<Recipe>();

        private Helper helper = new Helper();

        public void AddRecipe()
        {
            var name = Helper.CheckString("Enter your recipe name: ");
            var numberOfIngredient = Helper.CheckInt("Enter number of ingredients you want do add: ");
            var ingredients = helper.AddIngredients(numberOfIngredient);
            var nutritionalValue = helper.AddNutritionalValue();
            var recipe = new Recipe { Name = name, Ingredients = ingredients, NutritionalValue = nutritionalValue };
            Recipes.Add(recipe);
            Console.WriteLine("Recipe has been added.");
        }

        public Recipe EditRecipe()
        {
            var recipe = FindRecipeByName();
            if (recipe == null)
            {
                return null;
            }
            var yourCommandEdit = Helper.CheckInt("\n 1 - Edit name" + "\n 2 - Edit ingredients" + "\n 3 - Edit nutritional value", 3);
            switch (yourCommandEdit)
            {
                case 1:
                    recipe.Name = Helper.CheckString("Enter new name: "); ;
                    break;
                case 2:
                    var numberOfIngredients = Helper.CheckInt("Enter number of ingredient you want to add: ");
                    recipe.Ingredients = helper.AddIngredients(numberOfIngredients);
                    break;
                case 3:
                    var proteins = Helper.CheckDouble("Enter proteins value: ");
                    var fats = Helper.CheckDouble("Enter fats value: ");
                    var carbohydrates = Helper.CheckDouble("Enter carbohydrates value: ");
                    recipe.NutritionalValue = new NutritionalValue { Proteins=proteins, Fats=fats, Carbohydrates=carbohydrates};
                    break;
            }
            Console.WriteLine("Recipe has been edited.");
            return recipe;
        }

        public Recipe FindRecipeByName()
        {
            var name = Helper.CheckString("Enter recipe name you want to find: ");
            var rec = Recipes.Find(recipe => recipe.Name.ToUpper().Equals(name.ToUpper()));
            return rec;
        }

        public Recipe FindRecipeByIndex()
        {
            var index = Helper.CheckInt("Enter number of recipe you want to find: ", Recipes.Count);
            try
            {
                var rec = Recipes[index - 1];
                return rec;
            }
            catch
            {
                return null;
            }
        }

        public List<Recipe> FindRecipeByIngredientName()
        {
            var ingredient = Helper.CheckString("Enter needed ingredient name: ");
            var recipes = (from recipe in Recipes
                    from ing in recipe.Ingredients
                    where ing.Name.ToLower() == ingredient.ToLower()
                    select recipe).ToList();

            if (recipes.Count == 0)
            {
                Console.WriteLine("No such recipes found.");
                return recipes;
            }
            else { return recipes; }
        }

        public void DeleteRecipeByName()
        {
            var recipe = FindRecipeByName();
            if (recipe != null)
            {
                Recipes.Remove(recipe);
                Console.WriteLine("Recipe with entered name was deleted.");
            }
        }

        public void DeleteRecipeByIndex()
        {
            var index = Helper.CheckInt("Enter number of recipe you want to delete: ", Recipes.Count);
            try
            {
                Recipes.RemoveAt(index - 1);
                Console.WriteLine($"Recipe number {index} was deleted.");
            }
            catch 
            {
                Console.WriteLine("No such recipe was found.");
            }
        }

        public void DeleteRecipeByIngredient()
        {
            List<Recipe> recipes = FindRecipeByIngredientName();
            if (Recipes.Count() != 0)
            {
                Recipes = Recipes.Except(recipes).ToList();
                Console.WriteLine("Recipes with that ingredient were deleted.");
            }
        }

        public List<Recipe> OrderByName(string mode)
        {
            return mode == "Ascending" ? Recipes.OrderBy(recipe => recipe.Name).ToList() : Recipes.OrderByDescending(recipe => recipe.Name).ToList();
        }

        public List<Recipe> OrderByNutritionalValue(string mode)
        {
            var sortByValueType = Helper.CheckInt("Choose sorting type: " + "\n 1 - Overall" + "\n 2 - Proteins" + "\n 3 - Fats" +  "\n 3 - Carbohydrates", 4);
            return sortByValueType switch
            {
                1 => mode == "Ascending" ? Recipes.OrderBy(recipe => recipe.NutritionalValue.Proteins + recipe.NutritionalValue.Fats + recipe.NutritionalValue.Carbohydrates).ToList() : Recipes.OrderByDescending(recipe => recipe.NutritionalValue.Proteins + recipe.NutritionalValue.Fats + recipe.NutritionalValue.Carbohydrates).ToList(),
                2 => mode == "Ascending" ? Recipes.OrderBy(recipe => recipe.NutritionalValue.Proteins).ToList() : Recipes.OrderByDescending(recipe => recipe.NutritionalValue.Proteins).ToList(),
                3 => mode == "Ascending" ? Recipes.OrderBy(recipe => recipe.NutritionalValue.Fats).ToList() : Recipes.OrderByDescending(recipe => recipe.NutritionalValue.Fats).ToList(),
                4 => mode == "Ascending" ? Recipes.OrderBy(recipe => recipe.NutritionalValue.Carbohydrates).ToList() : Recipes.OrderByDescending(recipe => recipe.NutritionalValue.Carbohydrates).ToList(),
                _ => Recipes
            };
        }

        public List<Recipe> OrderByIngredient(string mode)
        {
            return mode == "Ascending" ? Recipes.OrderBy((recipe => recipe), new IngredientComparer()).ToList() : Recipes.OrderByDescending((recipe => recipe), new IngredientComparer()).ToList();
        }

        public List<Recipe> FilterByNutritionalValue()
        {
            Console.WriteLine("Note: filter does not ovewrite or change your journal. It creates copy and works with it.");
            var recipes_filter = Recipes;
            int yourFilterCommand = Helper.CheckInt("Choose to filter by: " + "\n 1 - Overall" + "\n 2 - Proteins" + "\n 3 - Fats" + "\n 4 - Carbohydrates", 4);
            double min = 0.0;
            double max = 0.0;
            switch (yourFilterCommand)
            {
                case 1:
                    min = Helper.CheckDouble("Enter minimum nutritional value: ");
                    max = Helper.CheckDouble("Enter maximum nutritional value: ");
                    recipes_filter = recipes_filter.Where(recipe => recipe.NutritionalValue.Proteins + recipe.NutritionalValue.Fats + recipe.NutritionalValue.Carbohydrates >= min && recipe.NutritionalValue.Proteins + recipe.NutritionalValue.Fats + recipe.NutritionalValue.Carbohydrates <= max).ToList();
                    break;
                case 2:
                    min = Helper.CheckDouble("Enter minimum value of proteins: ");
                    max = Helper.CheckDouble("Enter maximum of proteins: ");
                    recipes_filter = recipes_filter.Where(recipe => recipe.NutritionalValue.Proteins >= min && recipe.NutritionalValue.Proteins <= max).ToList();
                    break;
                case 3:
                    min = Helper.CheckDouble("Enter minimum value of fats: ");
                    max = Helper.CheckDouble("Enter maximum value of fats: ");
                    recipes_filter = recipes_filter.Where(recipe => recipe.NutritionalValue.Fats >= min && recipe.NutritionalValue.Fats <= max).ToList();
                    break;
                case 4:
                    min = Helper.CheckDouble("Enter minimum value of carbohydrates: ");
                    max = Helper.CheckDouble("Enter maximum value of carbohydrates: ");
                    recipes_filter = recipes_filter.Where(recipe => recipe.NutritionalValue.Carbohydrates >= min && recipe.NutritionalValue.Carbohydrates <= max).ToList();
                    break;
            }
            return recipes_filter;
        }

        public void ViewRecipes()
        {
            Console.Write("Your recipes: ");
            if (Recipes.Count > 0)
            {
                for (int i = 0; i < Recipes.Count; i++)
                {
                    var j = i + 1;
                    Console.WriteLine("\n " + j + "." + Recipes[i].ToString());
                }
            }
            else
            {
                Console.WriteLine("Your journal does not contain any recipes."); 
            }
            
        }

        public RecipeJournal ShallowCopy()
        {
            return (RecipeJournal) this.MemberwiseClone();
        }
    }
}
