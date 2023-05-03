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
        private AsciiReader? reader;
        private LevelLoader? loader;
        private EntityContainer<Entity>? blocks;
        private EntityContainer<Entity>? emptycontrolgroup;

        [Test]
        public void EmptyTest(){
            reader?.Read(Path.Combine("Assets","Levels","emptytest.txt"));
            Assert.AreEqual(loader?.ReadLevel(reader?.GetMap(),reader?.GetMeta(),reader?.GetLegend()),blocks);
        }

        [Test]
        public void Level1Test(){
            reader = new AsciiReader();
            reader?.Read(Path.Combine("Assets","Levels","level1.txt"));
            Assert.AreNotEqual(loader?.ReadLevel(reader?.GetMap(),reader?.GetMeta(),reader?.GetLegend()),blocks);
        }

        [Test]
        public void MapTest(){
            reader = new AsciiReader();
            reader?.Read(Path.Combine("Assets","Levels","level3.txt"));
            Assert.AreEqual(reader?.GetMap()[0], "bbbbbbbbbbbb");
            Assert.AreNotEqual(reader?.GetMap()[4], "bbbbbbbbbbbb");
            Assert.AreEqual(reader?.GetMap()[2][1], '#');
        }

        [Test]
        public void MetaTest(){
            reader = new AsciiReader();
            reader?.Read(Path.Combine("Assets","Levels","columns.txt"));
            Assert.AreEqual(reader?.GetMeta()[0], "Name: Columns");
        }

        [Test]
        public void LegendTest(){
            reader = new AsciiReader();
            reader?.Read(Path.Combine("Assets","Levels","level2.txt"));
            Assert.AreEqual(reader?.GetLegend()['h'], "green-block.png");
        }

    }
}