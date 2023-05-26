namespace BreakoutTests.BlockTest{
    [TestFixture]
    public class Tests{  
        [SetUp]
        public void Init(){
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            Vec2F pos =  new Vec2F(0.0f,0.0f);
            Vec2F ext = new Vec2F(1.0f,0.9f);
            IBaseImage img = new Image(Path.Combine("Assets", "Images", "blue-block.png"));
            IBaseImage redimg = new Image(Path.Combine("Assets", "Images", "red-block.png"));
            block = new BaseBlock(new DynamicShape(pos,ext),img, false);
            redblock = new BaseBlock(new DynamicShape(pos,ext),redimg, false);
            hardy = new HardenedBlock(new DynamicShape(pos,ext),img, false);
            unb = new UnbreakableBlock(new DynamicShape(pos,ext),redimg, false);
        }
        private BaseBlock? block;
        private BaseBlock? notblock;
        private BaseBlock? redblock;
        private HardenedBlock? hardy;
        private UnbreakableBlock? unb;

        [Test]
        public void ColourDifferenceTest(){
            Assert.AreNotEqual(block, redblock);
        }

        [Test]
        public void CreationTest(){
            Assert.AreNotEqual(block,notblock);
        }

        [Test]
        public void TestRectangular(){
            Assert.AreNotEqual(block?.shape.Extent.Y, block?.shape.Extent.X);
        }

        [Test]
        public void HitTest(){
            Assert.AreEqual(block?.HP,1);
            Assert.That(!block.IsDeleted());
            block?.Hit();
            Assert.AreEqual(block?.HP,0);
            Assert.AreNotEqual(block?.HP,1);
            Assert.That(block.IsDeleted());
        }
        [Test]
        public void TestHardenedBlock(){
            Assert.AreEqual(hardy?.BlockType,"Hardened");
            Assert.AreEqual(hardy.GetHP(),2);
        }
        [Test]
        public void TestUnbreakableBlock(){
            Assert.AreEqual(unb?.HP,1);
            Assert.That(!unb.IsDeleted());
            unb?.Hit();
            Assert.AreEqual(unb.HP,1);
            Assert.That(!unb.IsDeleted());
        }
        
    }
}