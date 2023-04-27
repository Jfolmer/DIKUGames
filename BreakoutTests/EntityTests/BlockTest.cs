namespace BreakoutTests.BlockTest{
    [TestFixture]
    public class Tests{  
        [SetUp]
        public void Init(){
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            Vec2F pos =  new Vec2F(0.0f,0.0f);
            Vec2F ext = new Vec2F(1.0f,1.0f);
            IBaseImage img = new Image(Path.Combine("Assets", "Images", "blue-block.png"));
            block = new BaseBlock(new DynamicShape(pos,ext),img);
        }
        private Block block;
        private Block notblock;
        [Test]
        public void CreationTest(){
            Assert.AreNotEqual(block,notblock);
        }
        [Test]
        public void HitTest(){
            block.Hit();
            Assert.AreEqual(block.HP,0);
        }
    }
}