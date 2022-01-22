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
                    if (res >= 0.0)
                    {
                        break;
                    }
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

        public static void ColorChange(int yourCommandColor)
        {
            switch (yourCommandColor)
            {
                case 1:
                    var char_color = CheckString("Enter preferred color: ").ToCharArray();
                    char_color[0] = char.ToUpper(char_color[0]);
                    if (char_color[0] == 'D' && char_color[3] == 'k')
                    {
                        char_color[4] = char.ToUpper(char_color[4]);
                    }
                    var color = new string(char_color);
                    var c = Enum.TryParse(typeof(ConsoleColor), color, out var choice);
                    if (!c)
                    {
                        Console.WriteLine("There is no such color available.");
                        break;
                    }
                    Console.ForegroundColor = (ConsoleColor) choice;
                    break;
                case 2:
                    char_color = CheckString("Enter preferred color: ").ToCharArray();
                    char_color[0] = char.ToUpper(char_color[0]);
                    char_color[4] = char.ToUpper(char_color[4]);
                    color = new string(char_color);
                    c = Enum.TryParse(typeof(ConsoleColor), color, out var ch);
                    if (!c)
                    {
                        Console.WriteLine("There is no such color available.");
                        break;
                    }
                    Console.BackgroundColor = (ConsoleColor) ch;
                    break;
                case 3:
                    Console.ResetColor();
                    break;
                case 4:
                    foreach (ConsoleColor consoleColor in Enum.GetValues(typeof(ConsoleColor)))
                    {
                        Console.WriteLine($"{consoleColor}");
                    }
                    break;
            }
        }
    }
}
