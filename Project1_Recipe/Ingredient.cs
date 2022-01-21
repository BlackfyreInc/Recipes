using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Project1_Recipe
{
    public class Ingredient
    {
        public double Amount { get; set; }
        public string Unit { get; set; }
        public string? Name { get; set; }
    }
}
