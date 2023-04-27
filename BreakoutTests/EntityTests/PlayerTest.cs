namespace BreakoutTests.PlayerTests{
    [TestFixture]
    public class Tests{  
        [SetUp]
        public void Init(){
            reader = new AsciiReader();
            loader = new LevelLoader();
            blocks = new EntityContainer<Entity>();
        }
        private AsciiReader reader;
        private LevelLoader loader;
        private EntityContainer<Entity> blocks;

        [Test]
        public void EmptyTest(){
            reader.Read(Path.Combine("BreakoutTests","Assets","Levels","level1.txt"));
            Assert.AreEqual((reader.GetMap())[0],"");
        }
    }
}