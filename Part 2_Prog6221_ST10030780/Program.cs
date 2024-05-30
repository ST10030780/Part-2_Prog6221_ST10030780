using System;
using System.Collections.Generic;
using System.Linq;

namespace RecipeApp
{
    class Program
    {
        static List<Recipe> recipes = new List<Recipe>();

        // Delegate for notifying when a recipe exceeds 300 calories
        public delegate void RecipeCaloriesExceededHandler(string recipeName);

        static void Main(string[] args)
        {
            bool exit = false;
            while (!exit)
            {
                DisplayMenu();

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            EnterRecipeDetails();
                            break;
                        case 2:
                            DisplayAllRecipes();
                            break;
                        case 3:
                            DisplayRecipeByName();
                            break;
                        case 4:
                            ScaleRecipe();
                            break;
                        case 5:
                            ResetQuantities();
                            break;
                        case 6:
                            ClearAllData();
                            break;
                        case 7:
                            exit = true;
                            break;
                        default:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Invalid choice. Please try again.");
                            Console.ResetColor();
                            break;
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid choice. Please enter a number.");
                    Console.ResetColor();
                }
            }
        }

        static void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine(new string('-', 50));
            Console.WriteLine("Welcome to RecipeApp!");
            Console.WriteLine(new string('-', 50));
            Console.WriteLine("1. Enter Recipe Details");
            Console.WriteLine("2. Display All Recipes");
            Console.WriteLine("3. Display a Recipe by Name");
            Console.WriteLine("4. Scale Recipe");
            Console.WriteLine("5. Reset Quantities");
            Console.WriteLine("6. Clear All Data");
            Console.WriteLine("7. Exit");
            Console.WriteLine(new string('-', 50));
            Console.Write("Enter your choice: ");
        }

        static void EnterRecipeDetails()
        {
            try
            {
                Console.Write("Enter the name of the recipe: ");
                string recipeName = Console.ReadLine();

                Recipe newRecipe = new Recipe(recipeName);

                Console.Write("Enter the number of ingredients: ");
                int numIngredients = int.Parse(Console.ReadLine());

                for (int i = 0; i < numIngredients; i++)
                {
                    Console.Write($"Enter name of ingredient {i + 1}: ");
                    string name = Console.ReadLine();

                    Console.Write($"Enter quantity of {name}: ");
                    double quantity = double.Parse(Console.ReadLine());

                    Console.Write($"Enter unit of measurement for {name}: ");
                    string unit = Console.ReadLine();

                    Console.Write($"Enter the number of calories for {name}: ");
                    double calories = double.Parse(Console.ReadLine());

                    Console.Write($"Enter the food group for {name}: ");
                    string foodGroup = Console.ReadLine();

                    newRecipe.AddIngredient(new Ingredient(name, quantity, unit, calories, foodGroup));
                }

                Console.Write("Enter the number of steps: ");
                int numSteps = int.Parse(Console.ReadLine());

                for (int i = 0; i < numSteps; i++)
                {
                    Console.Write($"Enter step {i + 1}: ");
                    newRecipe.Steps.Add(Console.ReadLine());
                }

                newRecipe.CheckCalories += NotifyCaloriesExceeded;
                recipes.Add(newRecipe);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Recipe details entered successfully.");
                Console.ResetColor();
            }
            catch (FormatException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid input format. Please enter a valid number or quantity.");
                Console.ResetColor();
            }
            catch (OverflowException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Input value is too large. Please enter a smaller value.");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"An error occurred: {ex.Message}");
                Console.ResetColor();
            }
        }

        static void DisplayAllRecipes()
        {
            Console.Clear();
            Console.WriteLine(new string('-', 50));
            Console.WriteLine("List of Recipes:");
            Console.WriteLine(new string('-', 50));

            if (recipes.Any())
            {
                var sortedRecipes = recipes.OrderBy(r => r.Name).ToList();
                foreach (var recipe in sortedRecipes)
                {
                    Console.WriteLine(recipe.Name);
                }
            }
            else
            {
                Console.WriteLine("No recipes available.");
            }

            Console.WriteLine(new string('-', 50));
            Console.WriteLine("Press any key to return to the menu.");
            Console.ReadKey();
        }

        static void DisplayRecipeByName()
        {
            Console.Write("Enter the name of the recipe to display: ");
            string recipeName = Console.ReadLine();

            var recipe = recipes.FirstOrDefault(r => r.Name.Equals(recipeName, StringComparison.OrdinalIgnoreCase));

            if (recipe != null)
            {
                recipe.Display();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Recipe not found.");
                Console.ResetColor();
            }

            Console.WriteLine("Press any key to return to the menu.");
            Console.ReadKey();
        }

        static void ScaleRecipe()
        {
            try
            {
                Console.Write("Enter the name of the recipe to scale: ");
                string recipeName = Console.ReadLine();

                var recipe = recipes.FirstOrDefault(r => r.Name.Equals(recipeName, StringComparison.OrdinalIgnoreCase));

                if (recipe != null)
                {
                    Console.Write("Enter scaling factor (0.5 for half, 2 for double, 3 for triple): ");
                    double factor = double.Parse(Console.ReadLine());

                    recipe.Scale(factor);

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Recipe scaled successfully.");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Recipe not found.");
                    Console.ResetColor();
                }
            }
            catch (FormatException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid scaling factor. Please enter a valid number.");
                Console.ResetColor();
            }
            catch (OverflowException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Scaling factor is too large. Please enter a smaller value.");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"An error occurred: {ex.Message}");
                Console.ResetColor();
            }
        }

        static void ResetQuantities()
        {
            Console.Write("Enter the name of the recipe to reset: ");
            string recipeName = Console.ReadLine();

            var recipe = recipes.FirstOrDefault(r => r.Name.Equals(recipeName, StringComparison.OrdinalIgnoreCase));

            if (recipe != null)
            {
                recipe.ResetQuantities();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Quantities reset to original values.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Recipe not found.");
                Console.ResetColor();
            }
        }

        static void ClearAllData()
        {
            Console.Write("Are you sure you want to clear all data? (y/n): ");
            if (Console.ReadLine().ToLower() == "y")
            {
                recipes.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("All data cleared.");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine("Clear data operation canceled.");
            }
        }

        static void NotifyCaloriesExceeded(string recipeName)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Warning: The total calories of the recipe '{recipeName}' exceed 300.");
            Console.ResetColor();
        }
    }
}

