using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace RecipeToMarkdown
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            try
            {
                Console.WriteLine($"Hello World! {DateTimeOffset.Now}");
//TODO: something with ConfigurationBuilder https://www.daveabrock.com/2021/01/19/config-top-level-programs/ https://docs.microsoft.com/en-us/troubleshoot/dotnet/csharp/store-custom-information-config-file
                var recpath = System.IO.Path.Combine(Environment.GetEnvironmentVariable("UserProfile"),"Projects", "Recipe-Framework","src","Assets","Tortellini.jsonc");
                using (System.IO.StreamReader fs = new(recpath))
                {
                    var c = fs.ReadToEnd();
                    var (o,j) = (Newtonsoft.Json.JsonConvert.DeserializeObject(c), Newtonsoft.Json.Linq.JObject.Parse(c));

                    var k= Newtonsoft.Json.JsonConvert.DeserializeObject<RecipeEntity.Recipe>( c );

                    System.Console.WriteLine($"{k.Steps.ElementAt(1)}");
                    foreach (var i in k.Steps.ElementAt(1).Ingredients[..3]){
                        System.Console.WriteLine(i);
                    }
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
