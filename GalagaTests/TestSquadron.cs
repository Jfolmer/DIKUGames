using NUnit.Framework;
using Galaga;
using DIKUArcade.Math;
using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Events;
using DIKUArcade.Input;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using Galaga.Squadron;
using System.Collections.Generic;
using System.IO;

namespace GalagaTests {
    
    [TestFixture]
    public class TestSquadron {
        [SetUp]
        public void Setup() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            
            enemyStridesBlue = ImageStride.CreateStrides(4, Path.Combine("Assets",
            "Images", "BlueMonster.png"));
            
            TestColSquad = new ColSquad();
            TestColEnemies = TestColSquad.Enemies;
            TestColSquad.CreateEnemies(enemyStridesBlue,enemyStridesBlue);
            
            TestArrowSquad = new ArrowSquad();
            TestArrowEnemies = TestArrowSquad.Enemies;
            TestArrowSquad.CreateEnemies(enemyStridesBlue,enemyStridesBlue);
            
            TestDiagSquad = new DiagSquad();
            TestDiagEnemies = TestDiagSquad.Enemies;
            TestDiagSquad.CreateEnemies(enemyStridesBlue,enemyStridesBlue);

            TestBoxSquad = new BoxSquad();
            TestBoxEnemies = TestBoxSquad.Enemies;
            TestBoxSquad.CreateEnemies(enemyStridesBlue,enemyStridesBlue);

            TestDiamondSquad = new DiamondSquad();
            TestDiamondEnemies = TestDiamondSquad.Enemies;
            TestDiamondSquad.CreateEnemies(enemyStridesBlue,enemyStridesBlue);
        }
        private List<Image> enemyStridesBlue;
        private ISquadron TestArrowSquad;
        private ISquadron TestDiagSquad;
        private ISquadron TestDiamondSquad;
        private ISquadron TestColSquad;
        private ISquadron TestBoxSquad;
        private EntityContainer<Enemy> TestArrowEnemies;
        private EntityContainer<Enemy> TestDiagEnemies;
        private EntityContainer<Enemy> TestDiamondEnemies;
        private EntityContainer<Enemy> TestColEnemies;
        private EntityContainer<Enemy> TestBoxEnemies;
        [Test]
        public void TestEnemyCountCol() {
            Assert.AreEqual(8, TestColEnemies.CountEntities());
            Assert.AreNotEqual(7,TestColEnemies.CountEntities());
            Assert.AreNotEqual(-1,TestColEnemies.CountEntities());
            Assert.AreNotEqual(9, TestColEnemies.CountEntities());
        }
        [Test]
        public void TestEnemyCountArrow() {
            Assert.AreEqual(8, TestArrowEnemies.CountEntities());
            Assert.AreNotEqual(7,TestArrowEnemies.CountEntities());
            Assert.AreNotEqual(-1,TestArrowEnemies.CountEntities());
            Assert.AreNotEqual(9, TestArrowEnemies.CountEntities());
        }
        [Test]
        public void TestEnemyCountDiag() {
            Assert.AreEqual(8, TestDiagEnemies.CountEntities());
            Assert.AreNotEqual(7,TestDiagEnemies.CountEntities());
            Assert.AreNotEqual(-1,TestDiagEnemies.CountEntities());
            Assert.AreNotEqual(9, TestDiagEnemies.CountEntities());
        }
        [Test]
        public void TestEnemyCountDiamond() {
            Assert.AreEqual(8, TestDiamondEnemies.CountEntities());
            Assert.AreNotEqual(7,TestDiamondEnemies.CountEntities());
            Assert.AreNotEqual(-1,TestDiamondEnemies.CountEntities());
            Assert.AreNotEqual(9, TestDiamondEnemies.CountEntities());
        }
        [Test]
        public void TestEnemyCountBox() {
            Assert.AreEqual(8, TestBoxEnemies.CountEntities());
            Assert.AreNotEqual(7,TestBoxEnemies.CountEntities());
            Assert.AreNotEqual(-1,TestBoxEnemies.CountEntities());
            Assert.AreNotEqual(9, TestBoxEnemies.CountEntities());
        }
    }
}