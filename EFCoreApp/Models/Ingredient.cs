namespace EFCoreApp.Models
{
    public class Ingredient
    {
        public int IngredientId { get; set; }
        public int RecipeId { get; set; } // EF Core interprets this as a foreign key pointing to the Recipe class
        public required string Name { get; set; }
        public decimal Quantity { get; set; }
        public required string Unit { get; set; }
    }
}
