namespace BreakoutTests.LevelLoadingTests{
    [TestFixture]
    public class Tests{  
        [SetUp]
        public void Init(){
            reader = new AsciiReader();
            loader = new LevelLoader();
            blocks = new EntityContainer<Entity>();
            emptycontrolgroup = new EntityContainer<Entity>();
        }
        private AsciiReader reader;
        private LevelLoader loader;
        private EntityContainer<Entity> blocks;
        private EntityContainer<Entity> emptycontrolgroup;

        [Test]
        public void EmptyTest(){
            reader.Read(Path.Combine("Assets","Levels","emptytest.txt"));
            Assert.AreEqual(loader.ReadLevel(reader.GetMap(),reader.GetMeta(),reader.GetLegend()),blocks);
        }
        [Test]
        public void Level1Test(){
            reader.Read(Path.Combine("Assets","Levels","level1.txt"));
            Assert.AreNotEqual(loader.ReadLevel(reader.GetMap(),reader.GetMeta(),reader.GetLegend()),blocks);
        }

    }
}