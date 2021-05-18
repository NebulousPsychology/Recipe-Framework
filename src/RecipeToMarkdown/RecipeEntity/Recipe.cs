using System.Collections.Generic;

namespace RecipeToMarkdown.RecipeEntity
{
    public class Recipe
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

    public class RecipeSource
    {
        [Newtonsoft.Json.JsonProperty()]
        public string Author;

        [Newtonsoft.Json.JsonProperty()]
        public System.Uri Url;
    }

    public class Step
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

    public class Ingredient
    {
        [Newtonsoft.Json.JsonProperty()]
        public string Unit;

        [Newtonsoft.Json.JsonProperty()]
        public string Name;

        [Newtonsoft.Json.JsonProperty()]
        public string Modifier = null;

        [Newtonsoft.Json.JsonProperty("Qty")]
        public dynamic QuantityRange;
    }

}