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
            block = new BaseBlock(new DynamicShape(pos,ext),img);
            redblock = new BaseBlock(new DynamicShape(pos,ext),redimg);
        }
        private BaseBlock? block;
        private BaseBlock? notblock;
        private BaseBlock? redblock;

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

        
    }
}