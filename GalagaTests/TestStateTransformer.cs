using System;
using GalagaStates;
using NUnit.Framework;

namespace GalagaTests {
    
    [TestFixture]
    public class TestGameState {
        
        [SetUp]
        public void Setup() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();

            TestString = "GAME_RUNNING";
        }
        private string TestString;
        [Test]
        public void TestToString(){
            var A = GameStateType.MainMenu;
            Assert.AreEqual(StateTransformer.TransformStateToString(A), "MAIN_MENU");
            Assert.AreNotEqual(StateTransformer.TransformStateToString(A), "GAME_PAUSED");
            Assert.AreNotEqual(StateTransformer.TransformStateToString(A), "GAME_RUNNING");
        }
        [Test]
        public void TestToState() {
            var A = GameStateType.MainMenu;
            Assert.AreEqual(StateTransformer.TransformStringToState("MAIN_MENU"), A);
            Assert.AreNotEqual(StateTransformer.TransformStringToState("GAME_PAUSED"), A);
            Assert.AreNotEqual(StateTransformer.TransformStringToState("GAME_RUNNING"), A);
        }
    }
}