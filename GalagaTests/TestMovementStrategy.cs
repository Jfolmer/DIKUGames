using NUnit.Framework;
using Galaga;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Events;
using DIKUArcade.Input;
using Galaga.MovementStrategy;

namespace GalagaTests {
    
    [TestFixture]
    public class TestMovementStrategy {
        [SetUp]
        public void Setup() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();

            Nomove = new NoMove();

            Downmove = new Down();

            Zigmove = new ZigZagDown();
        
            Jens = new Enemy(
                new DynamicShape(new Vec2F(0.5f, 1.0f), new Vec2F(0.1f, 0.1f)),
                new ImageStride(4, Path.Combine("Assets", "Images", "BlueMonster.png")), 3);
        }
        private IMovementStrategy Nomove;
        private IMovementStrategy Zigmove;
        private IMovementStrategy Downmove;
        private Enemy Jens;

        [Test]
        public void TestNoMove() {
            Nomove.MoveEnemy(Jens);

            Assert.AreEqual(1.0f, Jens.GetShape().Position.Y);
            Assert.AreNotEqual(1.1f, Jens.GetShape().Position.Y);
            Assert.AreNotEqual(0.09f, Jens.GetShape().Position.Y);
        }
        [Test]
        public void TestDown() {
            Downmove.MoveEnemy(Jens);

            Assert.AreEqual(0.997f, Jens.GetShape().Position.Y);
            Assert.AreNotEqual(1.1f, Jens.GetShape().Position.Y);
            Assert.AreNotEqual(0.09f, Jens.GetShape().Position.Y);
        }
        [Test]
        public void TestZigZagDown() {
            Zigmove.MoveEnemy(Jens);

            Assert.GreaterOrEqual(1.0f, Jens.GetShape().Position.Y);
            Assert.AreNotEqual(1.1f, Jens.GetShape().Position.Y);
            Assert.AreNotEqual(0.09f, Jens.GetShape().Position.Y);
        }
    }
}