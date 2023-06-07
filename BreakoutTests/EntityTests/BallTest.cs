namespace Breakout.Tests {
    [TestFixture]
    public class BallTests {

        private Ball? ball;

        [SetUp]
        public void Setup() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            var shape = new DynamicShape(new Vec2F(0.5f, 0.015f), new Vec2F(0.03f, 0.03f)); 
            var image = new Image(Path.Combine("Assets", "Images", "SofieBold.png"));
            ball = new Ball(shape, image);
        }
        [Test]
        public void TestRenderBall(){
            Assert.DoesNotThrow(() => ball?.Render());
        }
        [Test]
        public void TestUpdateDirection(){
            ball?.UpdateDirection(0.5f, 0.5f); 
            Assert.AreEqual(0.5f, ball?.shape.Direction.X);
            Assert.AreEqual(0.5f, ball?.shape.Direction.Y);
        }
        [Test]
        public void TestMove(){
            ball?.UpdateDirection(0.5f, 0.5f);
            ball?.Launch(new Vec2F(0.5f, 0.5f));
            ball?.Move();
            Assert.AreNotEqual(0.5f, ball?.shape.Position.X);
            Assert.AreNotEqual(0.5f, ball?.shape.Position.Y);
        }       
        [Test]
        public void TestBallNotDeletedInBounds(){
            ball?.UpdateDirection(0.5f, 0.5f);
            ball?.Launch(new Vec2F(0.5f, 0.5f));
            ball?.Move();
            Assert.IsFalse(ball?.IsDeleted() ?? true);
        }
        [Test]
        public void TestBallIsDeletedOutBounds(){
            ball?.UpdateDirection(-0.5f, -0.5f);
            ball?.Launch(new Vec2F(-0.5f, -0.5f));
            ball?.Move();
            Assert.IsFalse(ball?.IsDeleted() ?? false);
        }
    }
}