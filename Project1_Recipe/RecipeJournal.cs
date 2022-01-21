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
            while (recipe == null)
            {
                recipe = FindRecipeByName();
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
            return Recipes[index - 1];
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
            Recipes.RemoveAt(index - 1);
            Console.WriteLine($"Recipe number {index} was deleted.");
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
            var sortByValueType = Helper.CheckInt("Choose sorting type: " + "\n 1 - Proteins" + "\n 2 - Fats" + "\n 3 - Carbohydrates", 3);
            return sortByValueType switch
            {
                1 => mode == "Ascending" ? Recipes.OrderBy(recipe => recipe.NutritionalValue.Proteins).ToList() : Recipes.OrderByDescending(recipe => recipe.NutritionalValue.Proteins).ToList(),
                2 => mode == "Ascending" ? Recipes.OrderBy(recipe => recipe.NutritionalValue.Fats).ToList() : Recipes.OrderByDescending(recipe => recipe.NutritionalValue.Fats).ToList(),
                3 => mode == "Ascending" ? Recipes.OrderBy(recipe => recipe.NutritionalValue.Carbohydrates).ToList() : Recipes.OrderByDescending(recipe => recipe.NutritionalValue.Carbohydrates).ToList(),
                _ => Recipes,
            };
        }

        public List<Recipe> OrderByIngredient(string mode)
        {
            return mode == "Ascending" ? Recipes.OrderBy((recipe => recipe), new IngredientComparer()).ToList() : Recipes.OrderByDescending((recipe => recipe), new IngredientComparer()).ToList();
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
    }
}
