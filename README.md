Description
RecipeApp is a console-based application that allows users to manage multiple recipes. Users can enter, display, scale, and reset recipes, along with additional functionalities like handling units of measurement and tracking calories. The application is designed to be user-friendly, featuring improved error handling, colored text for better readability, and confirmations for critical actions.

Features
Enter an unlimited number of recipes with names.
Add ingredients with quantities, units, calories, and food groups.
Enter and display steps for each recipe.
Display a list of all recipes sorted alphabetically by name.
Choose and display a specific recipe by name.
Scale recipes by a factor (e.g., 0.5, 2, 3) with correct unit adjustments.
Reset scaled recipes back to their original values.
Confirm before clearing all data to prevent accidental loss.
Notify the user when the total calories of a recipe exceed 300.

Requirements
.NET Framework 4.8
Installation
Clone the repository or download the source code.
Open the solution in Visual Studio.
Build the solution to restore dependencies and compile the code.

Usage
Run the application.
Follow the on-screen instructions to navigate through the menu:
Enter Recipe Details: Add a new recipe with ingredients and steps.
Display All Recipes: View all recipes sorted alphabetically.
Display a Recipe by Name: View the details of a specific recipe.
Scale Recipe: Scale the quantities of a recipe by a given factor.
Reset Quantities: Reset the scaled recipe back to its original quantities.
Clear All Data: Confirm and clear all recipe data.
Exit: Exit the application.

Code Structure
Program.cs: Contains the main program logic and menu handling.
Recipe.cs: Defines the Recipe class, which includes methods for adding ingredients, scaling, resetting, and displaying recipes.
Ingredient.cs: Defines the Ingredient class, which includes properties for name, quantity, unit, calories, and food group.
Advanced Features
Error Handling: Comprehensive error handling for null values, incorrect types, and other potential issues.
Colored Text: Uses colored text to enhance readability and user interaction.
Delegates: Utilizes a delegate to notify users when a recipe exceeds 300 calories.
Comments and Standards: Includes meaningful comments and adheres to international coding standards for better readability and maintainability.
Separate Files: Classes are split into separate files for better organization.
