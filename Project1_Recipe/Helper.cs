namespace Project1_Recipe
{
    public class Helper
    {
        public List<Ingredient> AddIngredients(int number)
        {
            var ingredients = new List<Ingredient>();
            for (int i = 0; i < number; i++)
            {
                var ingredientName = CheckString($"Enter ingredient {i + 1} name: ");
                while (ingredients.Any(ingredient => ingredient.Name == ingredientName))
                {
                    ingredientName = CheckString($"Enter ingredient {i + 1} name: "); ;
                }

                var amount = CheckDouble($"Enter amount of ingredient {ingredientName}: ");

                Console.WriteLine("\n Choose number of preferred unit:" + "\n 1 - g" + "\n 2 - kg" + "\n 3 - ml" + "\n 4 - l" + "\n 5 - teaspoon" + "\n 6 - spoon");
                var unit = Enum.GetName(typeof(Unit), CheckInt("Enter new Unit: ", 6) - 1);

                var ingredient = new Ingredient { Amount = amount, Unit = unit, Name = ingredientName };
                ingredients.Add(ingredient);
            }
            return ingredients;
        }

        public NutritionalValue AddNutritionalValue()
        {
            var proteins = CheckDouble("Enter proteins value: ");
            var fats = CheckDouble("Enter fats value: ");
            var carbohydrates = CheckDouble("Enter carbohydrates value: ");
            return new NutritionalValue { Proteins = proteins, Fats = fats, Carbohydrates = carbohydrates };
        }

        public static int CheckInt(string message, int limit = 0)
        {
            Console.WriteLine(message);
            var str = Console.ReadLine();
            while (str == null)
            {
                Console.WriteLine("Enter valid data: ");
                str = Console.ReadLine();
            }
            int res;
            while (true)
            {
                if (int.TryParse(str, out res))
                {
                    if (limit != 0 && res <= limit && !(res <= 0))
                    {
                        break;
                    }
                    else if (limit == 0 && !(res < 0))
                    {
                        break;
                    }
                }
                Console.WriteLine("Please enter valid integer value: ");
                str = Console.ReadLine();
            }
            return res;
        }

        public static double CheckDouble(string message)
        {
            Console.WriteLine(message);
            var str = Console.ReadLine();
            while (str == null)
            {
                Console.WriteLine("Enter valid data: ");
                str = Console.ReadLine();
            }
            double res;
            while (true)
            {
                if (double.TryParse(str, out res))
                {
                    break;
                }
                Console.WriteLine("Please enter valid double value: ");
                str = Console.ReadLine();
            }
            return res;
        }

        public static string CheckString(string message)
        {
            Console.WriteLine(message);
            var str = Console.ReadLine();
            while (str == null || string.IsNullOrWhiteSpace(str))
            {
                Console.WriteLine("Enter valid data: ");
                str = Console.ReadLine();
            }
            return str;
        }
    }
}
