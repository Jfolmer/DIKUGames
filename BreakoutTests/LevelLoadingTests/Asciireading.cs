using System.IO;

namespace BreakoutTests.LevelLoadingTests{
    [TestFixture]
    public class Tests{  
        [SetUp]
        public void Init(){
            reader = new AsciiReader();
            loader = new LevelLoader();
        }
        private AsciiReader reader;
        private LevelLoader loader;

        [Test]
        public void EmptyTest(){
            reader.Read(Path.Combine("emptytest.txt"));
            Assert.AreEqual((reader.GetMap())[0],"");
        }
    }
}