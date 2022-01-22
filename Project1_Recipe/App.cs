using System.Text.Json;

namespace Project1_Recipe
{
    internal class App
    {
        public static void Run()
        {
            var recipeJournal = new RecipeJournal();
            var options = new JsonSerializerOptions { IncludeFields = true };
            var filename = "recipes.json";
            var yourCommand = 0;
            while (yourCommand != 11)
            {
                yourCommand = Helper.CheckInt("Choose one of the following numbers:" + "\n 1 - Add recipe" + "\n 2 - Delete recipe " + "\n 3 - Search recipe " + "\n 4 - Order recipes " + "\n 5 - View your recipes " + "\n 6 - Edit recipe " + "\n 7 - Save data to recipes.json file " + "\n 8 - Get data from recipes.json file " + "\n 9 - Filter recipes by nutritional value " + "\n 10 - Change colors " +  "\n 11 - Exit", 11);
                switch (yourCommand)
                {
                    case 1:
                        recipeJournal.AddRecipe();
                        break;
                    case 2:
                        var yourCommandDelete = 0;
                        while (yourCommandDelete != 4)
                        {
                            yourCommandDelete = Helper.CheckInt("Choose one of the following numbers:" + "\n 1 - Delete recipe by name" + "\n 2 - Delete recipe by ingredient " + "\n 3 - Delete recipe by index " + "\n 4 - Go back to main menu", 4);
                            switch (yourCommandDelete)
                            {
                                case 1:
                                    recipeJournal.DeleteRecipeByName();
                                    break;
                                case 2:
                                    recipeJournal.DeleteRecipeByIngredient();
                                    break;
                                case 3:
                                    recipeJournal.DeleteRecipeByIndex();
                                    break;
                            }
                        }
                        break;
                    case 3:
                        var yourCommandSearch = 0;
                        while (yourCommandSearch != 4)
                        {
                            yourCommandSearch = Helper.CheckInt("Choose one of the following numbers:" + "\n 1 - Find recipe by name " + "\n 2 - Find recipe by ingredient " + "\n 3 - Find recipe by index " + "\n 4 - Go back to main menu", 4);
                            switch (yourCommandSearch)
                            {
                                case 1:
                                    var rec = recipeJournal.FindRecipeByName();
                                    if (rec != null)
                                    {
                                        Console.WriteLine(rec.ToString());
                                    }
                                    else
                                    {
                                        Console.WriteLine("No such recipe found.");
                                    }
                                    break;
                                case 2:
                                    var foundRecipes = recipeJournal.FindRecipeByIngredientName();
                                    foreach (var r in foundRecipes)
                                    {
                                        Console.WriteLine(r.ToString());
                                    }
                                    break;
                                case 3:
                                    rec = recipeJournal.FindRecipeByIndex();
                                    if (rec != null)
                                    {
                                        Console.WriteLine(rec.ToString());
                                    }
                                    else
                                    {
                                        Console.WriteLine("No such recipe found.");
                                    }
                                    break;
                            }
                        }
                        break;
                    case 4:
                        var yourCommandOrder = 0;
                        while (yourCommandOrder != 4)
                        {
                            yourCommandOrder = Helper.CheckInt("Choose one of the following numbers:" + "\n 1 - Order recipe by name " + "\n 2 - Order recipe by ingredient " + "\n 3 - Order recipe by nutritional value " + "\n 4 - Go back to main menu", 4);
                            switch (yourCommandOrder)
                            {
                                case 1:
                                    var mode = Helper.CheckInt("Choose sorting type:" + "\n 1 - Ascending " + "\n 2 - Descending", 2);
                                    if (mode == 1)
                                    {
                                        recipeJournal.Recipes = recipeJournal.OrderByName("Ascending");
                                        recipeJournal.ViewRecipes();
                                    }
                                    else if (mode == 2)
                                    {
                                        recipeJournal.Recipes = recipeJournal.OrderByName("Descending");
                                        recipeJournal.ViewRecipes();
                                    }
                                    break;
                                case 2:
                                    mode = Helper.CheckInt("Choose sorting type:" + "\n 1 - Ascending " + "\n 2 - Descending", 2);
                                    if (mode == 1)
                                    {
                                        recipeJournal.Recipes = recipeJournal.OrderByIngredient("Ascending");
                                        recipeJournal.ViewRecipes();
                                    }
                                    else if (mode == 2)
                                    {
                                        recipeJournal.Recipes = recipeJournal.OrderByIngredient("Descending");
                                        recipeJournal.ViewRecipes();
                                    }
                                    break;
                                case 3:
                                    mode = Helper.CheckInt("Choose sorting type:" + "\n 1 - Ascending " + "\n 2 - Descending", 2);
                                    if (mode == 1)
                                    {
                                        recipeJournal.Recipes = recipeJournal.OrderByNutritionalValue("Ascending");
                                        recipeJournal.ViewRecipes();
                                    }
                                    else if (mode == 2)
                                    {
                                        recipeJournal.Recipes = recipeJournal.OrderByNutritionalValue("Descending");
                                        recipeJournal.ViewRecipes();
                                    }
                                    break;
                            }
                        }
                        break;
                    case 5:
                        recipeJournal.ViewRecipes();
                        break;
                    case 6:
                        var recipe = recipeJournal.EditRecipe();
                        if (recipe == null)
                        {
                            Console.WriteLine("No such recipe was found.");
                            break;
                        }
                        Console.WriteLine(recipe.ToString());
                        break;
                    case 7:
                        var yourCommandJSON = Helper.CheckInt($"Do you want to change your filename from {filename}? " + "\n 1 - Yes " + "\n 2 - No " + "\n 3 - Go back to main menu", 3);
                        switch (yourCommandJSON)
                        {
                            case 1:
                                filename = Helper.CheckString("Enter your new filename: ");
                                break;
                            case 2:
                                break;
                        }
                        if (yourCommandJSON == 3)
                        {
                            break;
                        }
                        var jsonString = JsonSerializer.Serialize(recipeJournal, options);
                        File.WriteAllText(filename, jsonString);
                        var yourCommandJSON1 = Helper.CheckInt($"Do you want to display {filename} contents?" + "\n 1 - Yes " + "\n 2 - No");
                        switch (yourCommandJSON1)
                        {
                            case 1:
                                Console.WriteLine(File.ReadAllText(filename));
                                break;
                            case 2:
                                break;
                        }
                        break;
                    case 8:
                        if (File.Exists("recipes.json"))
                        {
                            try
                            {
                                recipeJournal = JsonSerializer.Deserialize<RecipeJournal>(File.ReadAllText("recipes.json"), options);
                                Console.WriteLine("Data has been loaded.");
                            }
                            catch (JsonException)
                            {
                                Console.WriteLine($"Your {filename} file is corrupted.");
                            }
                        }
                        break;
                    case 9:
                        var filtered_recipes = recipeJournal.ShallowCopy();
                        filtered_recipes.Recipes = filtered_recipes.FilterByNutritionalValue();
                        filtered_recipes.ViewRecipes();
                        break;
                    case 10:
                        var yourCommandColor = 0;
                        while (yourCommandColor != 5)
                        {
                            yourCommandColor = Helper.CheckInt("\nChoose one of the following numbers:" + "\n 1 - Change console foreground color" + "\n 2 - Change console background text color" + "\n 3 - Reset colors" + "\n 4 - Help: Show available colors" + "\n 5 - Go back to main menu", 5);
                            Helper.ColorChange(yourCommandColor);
                        }
                        break;
                }
            }
        }
    }
}
