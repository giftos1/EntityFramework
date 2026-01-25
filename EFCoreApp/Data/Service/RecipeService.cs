using EFCoreApp.Models;
using Microsoft.EntityFrameworkCore;

/*NOTE EF Core logs all the SQL statements it runs as
LogLevel.Information events by default, so you can easily
see what queries are running against the database.*/

namespace EFCoreApp.Data.Service
{
    public class RecipeService : IRecipeService
    {
        private readonly AppDbContext _context;
        public RecipeService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Recipe>> GetAllRecipes()
        {
            var recipes = await _context.Recipes.ToListAsync();
            return recipes;
        }

        public async Task Add(Recipe recipe)
        {
            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();
        }

        public async Task<RecipeDetailView> GetRecipe(int id)
        {
            var recipe = await _context.Recipes
                .Where(r => r.RecipeId == id)
                .Select(r => new RecipeDetailView
                {
                    Id = r.RecipeId,
                    Name = r.Name,
                    TimeToCook = r.TimeToCook,
                    Method = r.Method,
                    Ingredients = r.Ingredients
                        .Select(i => new RecipeDetailView.Item
                        {
                            Name = i.Name,
                            Quantity = i.Quantity,
                            Unit = i.Unit
                        }).ToList()
                })
                .SingleOrDefaultAsync();

            if (recipe == null)
            {
                throw new InvalidOperationException($"Recipe with id {id} not found.");
            }

            return recipe;
        }

        // update via view model
        public async Task updateRecipe(int id, RecipeDetailView model)
        {
            var recipe = await _context.Recipes
                .Where(r => r.RecipeId == id)
                .Include(r => r.Ingredients)
                .SingleOrDefaultAsync();

            if (recipe == null)
            {
                throw new InvalidOperationException($"Recipe with id {id} not found.");
            }

            // Update recipe properties
            recipe.Name = model.Name;
            recipe.TimeToCook = model.TimeToCook;
            recipe.Method = model.Method;

            // Update ingredients
            recipe.Ingredients.Clear();
            foreach (var ingredient in model.Ingredients)
            {
                recipe.Ingredients.Add(new Ingredient
                {
                    Name = ingredient.Name,
                    Quantity = ingredient.Quantity,
                    Unit = ingredient.Unit
                });
            }

            // save changes asynchronously
            await _context.SaveChangesAsync();
        }

        // update via entity

        /*public async Task UpdateRecipe(Recipe updatedRecipe)
        {
            var recipe = await _context.Recipes.FindAsync(updatedRecipe.RecipeId);

            if (recipe == null)
            {
                throw new InvalidOperationException($"Recipe with id {updatedRecipe.RecipeId} not found.");
            }

            // Update recipe properties
            recipe.Name = updatedRecipe.Name;
            recipe.TimeToCook = updatedRecipe.TimeToCook;
            recipe.Method = updatedRecipe.Method;

            // Update ingredients
            recipe.Ingredients.Clear();
            foreach (var ingredient in updatedRecipe.Ingredients)
            {
                recipe.Ingredients.Add(new Ingredient
                {
                    Name = ingredient.Name,
                    Quantity = ingredient.Quantity,
                    Unit = ingredient.Unit
                });
            }

            // save changes asynchronously
            await _context.SaveChangesAsync();
        }*/

        public async Task DeleteRecipe(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null)
            {
                throw new InvalidOperationException($"Recipe with id {id} not found.");
            }
            recipe.IsDeleted = true;
            await _context.SaveChangesAsync();
        }

        //public async Task<ICollection<Recipe>> GetRecipes() {
        //    return await _context.Recipes
        //        .Where(r => !r.IsDeleted)
        //        .Select(r => new Recipe
        //        {
        //            RecipeId = r.RecipeId,
        //            Name = r.Name,
        //            TimeToCook = r.TimeToCook

        //        })
        //        .ToListAsync();
        //}


        //public async Task<int> CreateRecipe(CreateRecipeCommand cmd)
        //{
        //    var recipe = new Recipe
        //    {
        //        Name = cmd.Name,
        //        TimeToCook = new TimeSpan(cmd.TimeToCookHours, cmd.TimeToCookMins, 0),
        //        Method = cmd.Method,
        //        IsVegetarian = cmd.IsVegetarian,
        //        IsVegan = cmd.IsVegan,
        //        Ingredients = cmd.Ingredients.Select(i =>
        //        new Ingredient
        //        {
        //            Name = i.Name,
        //            Quantity = i.Quantity,
        //            Unit = i.Unit
        //        }).ToList()
        //    };
        //    _context.Add(recipe);
        //    await _context.SaveChangesAsync();
        //    return recipe.RecipeId;
        //}


    }
}
