namespace EFCoreApp.Models
{
    public class RecipeDetailView
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public TimeSpan TimeToCook { get; set; }

        public required string Method { get; set; }

        public required List<Item> Ingredients { get; set; }

        public class Item
        {
            public int IngredientId { get; set; }
            public required string Name { get; set; }
            public decimal Quantity { get; set; }
            public required string Unit { get; set; }
        }
    }
}
