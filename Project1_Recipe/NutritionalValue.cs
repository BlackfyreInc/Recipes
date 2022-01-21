using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Project1_Recipe
{
    public class NutritionalValue
    {   
        public double Proteins { get; set; }
        public double Fats { get; set; }
        public double Carbohydrates { get; set; }
    }
}
