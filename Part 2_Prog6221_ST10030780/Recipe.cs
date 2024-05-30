using System;
using System.Collections.Generic;
using System.Linq;

namespace RecipeApp
{
    class Recipe
    {
        public string Name { get; private set; }
        public List<Ingredient> Ingredients { get; private set; }
        public List<string> Steps { get; private set; }

        public event Program.RecipeCaloriesExceededHandler CheckCalories;

        public Recipe(string name)
        {
            Name = name;
            Ingredients = new List<Ingredient>();
            Steps = new List<string>();
        }

        public void AddIngredient(Ingredient ingredient)
        {
            Ingredients.Add(ingredient);
            CheckTotalCalories();
        }

        public void Display()
        {
            Console.Clear();
            Console.WriteLine(new string('-', 50));
            Console.WriteLine($"Recipe: {Name}");
            Console.WriteLine(new string('-', 50));
            Console.WriteLine("Ingredients:");

            foreach (var ingredient in Ingredients)
            {
                Console.WriteLine($"{ingredient.Quantity} {ingredient.Unit} of {ingredient.Name} ({ingredient.Calories} calories, {ingredient.FoodGroup})");
            }

            Console.WriteLine("\nSteps:");
            for (int i = 0; i < Steps.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Steps[i]}");
            }

            Console.WriteLine(new string('-', 50));
            Console.WriteLine($"Total Calories: {Ingredients.Sum(i => i.Calories)}");
            Console.WriteLine(new string('-', 50));
        }

        public void Scale(double factor)
        {
            foreach (var ingredient in Ingredients)
            {
                ingredient.Quantity *= factor;
            }
            CheckTotalCalories();
        }

        public void ResetQuantities()
        {
            foreach (var ingredient in Ingredients)
            {
                ingredient.ResetQuantity();
            }
        }

        private void CheckTotalCalories()
        {
            double totalCalories = Ingredients.Sum(i => i.Calories);
            if (totalCalories > 300)
            {
                CheckCalories?.Invoke(Name);
            }
        }
    }
}
