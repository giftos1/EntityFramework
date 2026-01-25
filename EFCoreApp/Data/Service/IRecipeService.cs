using EFCoreApp.Models;

namespace EFCoreApp.Data.Service
{
    public interface IRecipeService
    {
        Task<IEnumerable<Recipe>> GetAllRecipes();
        Task Add(Recipe recipe);

        Task<RecipeDetailView> GetRecipe(int id);

        //Task<ICollection<Recipe>> GetRecipes();
    }
}