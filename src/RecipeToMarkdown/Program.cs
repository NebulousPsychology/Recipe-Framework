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
                // ...
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
