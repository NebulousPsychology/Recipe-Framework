using System;

namespace RecipeToMarkdown
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                Console.WriteLine($"Hello World! {DateTimeOffset.Now}");
                using (var fs = new System.IO.StreamReader($"{Environment.CurrentDirectory}/../Assets/Tortellini.jsonc"))
                {
                    var c = fs.ReadToEnd();
                    var o = Newtonsoft.Json.JsonConvert.DeserializeObject(c);
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
