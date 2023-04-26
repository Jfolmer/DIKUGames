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
    public class TestHealth {
        [SetUp]
        public void Setup() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();

            HP = new Health(new Vec2F (0.0f,0.0f),new Vec2F(0.2f,0.3f));
        }
        private Health HP;
        [Test]
        public void TestHP() {
            HP.LoseHealth();

            Assert.AreEqual(2, HP.GetHealth());
            Assert.AreNotEqual(3, HP.GetHealth());

            HP.LoseHealth();
            HP.LoseHealth();
            HP.LoseHealth();
            HP.LoseHealth();

            Assert.AreEqual(0, HP.GetHealth());
            Assert.AreNotEqual(-2, HP.GetHealth());
            Assert.AreNotEqual(2, HP.GetHealth());

        }
    }
}