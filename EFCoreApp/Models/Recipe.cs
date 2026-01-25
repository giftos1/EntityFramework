namespace EFCoreApp.Models
{
    public class Recipe
    {
        public int RecipeId { get; set; }
        public required string Name { get; set; }
        public TimeSpan TimeToCook { get; set; }
        public bool IsDeleted { get; set; }
        public required string Method { get; set; }
        public bool IsVegetarian { get; set; }
        public bool IsVegan { get; set; }
        public required ICollection<Ingredient> Ingredients { get; set; } // represents a many-to-one relationship in which each Recipe can have multiple Ingredients but each Ingredient is associated with a single Recipe.
    }

    //EF Core identifies this pattern of an Id suffix as indicating
    //the primary key of the table.
}
