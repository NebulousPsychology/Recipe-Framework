using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace RecipeToMarkdown
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                Console.WriteLine($"Hello World! {DateTimeOffset.Now}");

                var recpath = System.IO.Path.Combine(Environment.GetEnvironmentVariable("UserProfile"),"Projects", "Recipe-Framework","src","Assets","Tortellini.jsonc");
                using (var fs = new System.IO.StreamReader(recpath))
                {
                    var c = fs.ReadToEnd();
                    var o = Newtonsoft.Json.JsonConvert.DeserializeObject(c);
                    var j = Newtonsoft.Json.Linq.JObject.Parse(c);

                    var k= Newtonsoft.Json.JsonConvert.DeserializeObject<RecipeEntity.Recipe>( c );

System.Console.WriteLine($"{k.Steps.ElementAt(1).Ingredients[0].QuantityRange.ToString()}");
System.Console.WriteLine($"{k.Steps.ElementAt(1).Ingredients[1].QuantityRange.ToString()}");
                    System.Console.WriteLine(c);
                }
            }
            catch (Exception sww)
            {
                Console.Error.WriteLine($"SWW: {sww}");
            }
            finally
            {
                Console.WriteLine($"Goodbye World! {DateTimeOffset.Now}");
            }
        }
    }
}
