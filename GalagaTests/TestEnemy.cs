using NUnit.Framework;
using Galaga;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Events;
using DIKUArcade.Input;

namespace GalagaTests {
    
    [TestFixture]
    public class TestEnemy {
        [SetUp]
        public void Setup() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();

            Jens = new Enemy(
                new DynamicShape(new Vec2F(0.5f, 1.0f), new Vec2F(0.1f, 0.1f)),
                new ImageStride(4, Path.Combine("Assets", "Images", "BlueMonster.png")), 3);
        }
        private Enemy Jens;
        [Test]
        public void TestEnrage() {
            Jens.Hit();
            Jens.Hit();
            Jens.Enrage();

            Assert.AreEqual(Jens.GetHP(), 1);
            Assert.AreNotEqual(Jens.GetHP(), 0);
            Assert.AreNotEqual(Jens.GetHP(), 2);
            Assert.AreNotEqual(Jens.GetHP(), 3);
            
            Assert.AreEqual(Jens.GetSpeed(), 0.006f);
            Assert.AreNotEqual(Jens.GetSpeed(), 1.006f);
            Assert.AreNotEqual(Jens.GetSpeed(), 0.003f);
            Assert.AreNotEqual(Jens.GetSpeed(), 99999099999.006f);
        }
    }
}