namespace RecipeToMarkdown.RecipeEntity
{
    class Recipe
    {
        string Name{get;set;}
        RecipeSource Source;
        System.Collections.Generic.IEnumerable<object> Steps;
        string[] Remarks;
    }

    class RecipeSource{
        string Author;
        System.Uri Url;
    }

    class Step {
        string Action;

    }
    class Ingredient{
        string Unit;
        string Name;
        string Modifier=null;
        Quantity Quantity;
    }
    class Quantity{
        double Min, Max;
    }

}