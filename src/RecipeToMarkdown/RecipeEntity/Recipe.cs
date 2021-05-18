using System.Collections.Generic;

namespace RecipeToMarkdown.RecipeEntity
{
    public record Recipe
    {
        [Newtonsoft.Json.JsonProperty()]
        public string Name { get; set; }

        [Newtonsoft.Json.JsonProperty()]
        public RecipeSource Source;

        [Newtonsoft.Json.JsonProperty()]
        public Step[] Steps;

        [Newtonsoft.Json.JsonProperty()]
        public string[] Remarks;
    }

    public record RecipeSource
    {
        [Newtonsoft.Json.JsonProperty()]
        public string Author;

        [Newtonsoft.Json.JsonProperty()]
        public System.Uri Url;
    }

    public record Step
    {
        [Newtonsoft.Json.JsonProperty()]
        public string Action;

        [Newtonsoft.Json.JsonProperty()]
        public string In;

        [Newtonsoft.Json.JsonProperty()]
        public Ingredient[] Ingredients;

        [Newtonsoft.Json.JsonProperty()]
        public string[] Notes;
    }

    public record Ingredient
    {
        [Newtonsoft.Json.JsonProperty("Qty")]
        public dynamic Quantity;

        [Newtonsoft.Json.JsonProperty()]
        public string Unit;

        [Newtonsoft.Json.JsonProperty()]
        public string Name;

        [Newtonsoft.Json.JsonProperty()]
        public string Modifier = null;
    }

}