using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace R2M.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task TestMethod1()
        {
            await RecipeToMarkdown.Program.Main(null);
        }

        [TestMethod]
        public void AssetsFolderPresent()
        {
            string assetsPath = System.IO.Path.Join( System.IO.Path.GetDirectoryName( System.Reflection.Assembly.GetExecutingAssembly().Location), "Assets");
            Assert.IsTrue(System.IO.Directory.Exists(assetsPath),$"Assets Folder '{assetsPath??"nul"}' is missing");

            var assetsFiles = System.IO.Directory.GetFiles(assetsPath);
            Assert.AreEqual<int>(actual: assetsFiles.Length, expected: 3);

            System.Console.WriteLine();
            foreach (var path in assetsFiles){
                System.Console.WriteLine(path);
            }
        }
    }
}
