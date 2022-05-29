using Classes;
using NSubstitute;
using NUnit.Framework;

namespace Tests
{
    public class SurvivorShould
    {
        private Survivor _survivor;
        private Equipament _equipamentBaseballBat;
        private Equipament _equipamentKatana;
        private IGame _game = Substitute.For<IGame>();
        private IZombie _zombie = Substitute.For<IZombie>();
        [SetUp]
        public void SetUp()
        {
            _survivor = new Survivor("Juan", _game);
            _equipamentBaseballBat = new Equipament("Baseball bat");
            _equipamentKatana = new Equipament("Katana");
        }
        
        [Test]
        public void HasAName()
        {
            Assert.AreEqual("Juan", _survivor.name);
        }
        
        [Test]
        public void StartWithNoWounds()
        {
            Assert.AreEqual(0, _survivor.wounds);
        }
        
        [Test]
        public void DieWithTwoWounds()
        {
            _survivor.ReceiveWound();
            _survivor.ReceiveWound();
            
            Assert.IsFalse(_survivor.isAlive);
        }

        [Test]
        public void NotifyToGameThatHeIsDead()
        {
            _survivor.ReceiveWound();
            _survivor.ReceiveWound();

            _game.Received(1).ASurvivorDie(_survivor);
        }
        
        [Test]
        public void HaveThreeActionsPerTurn()
        {
            Assert.AreEqual(3, _survivor.actions);
        }
        
        [Test]
        public void HaveTwoEquipamentInHand()
        {
            _survivor.Equipate(_equipamentBaseballBat, "In Reserve");
            _survivor.Equipate(_equipamentKatana, "In Hand");
            _survivor.Equipate(_equipamentKatana, "In Hand");
            _survivor.Equipate(_equipamentKatana, "In Hand");
            
            
            Assert.AreEqual(2, _survivor.EquipamentInHand());
        }
        
        [Test]
        public void Have5EquipamentInReserve()
        {
            _survivor.Equipate(_equipamentBaseballBat, "In Reserve");
            _survivor.Equipate(_equipamentKatana, "In Reserve");
            _survivor.Equipate(_equipamentKatana, "In Hand");
            _survivor.Equipate(_equipamentKatana, "In Reserve");
            _survivor.Equipate(_equipamentKatana, "In Hand");
            _survivor.Equipate(_equipamentKatana, "In Reserve");
            _survivor.Equipate(_equipamentKatana, "In Hand");
            
            Assert.AreEqual(5, _survivor.equipament.Count);
        }
        
        [Test]
        public void HaveOneLessPiecesWhenReceiveWound()
        {
            _survivor.Equipate(_equipamentBaseballBat, "In Reserve");
            _survivor.Equipate(_equipamentKatana, "In Reserve");
            _survivor.Equipate(_equipamentKatana, "In Hand");
            _survivor.Equipate(_equipamentKatana, "In Reserve");
            _survivor.Equipate(_equipamentKatana, "In Reserve");
            
            _survivor.ReceiveWound();
            
            _survivor.Equipate(_equipamentKatana, "In Reserve");
            
            Assert.AreEqual(4, _survivor.equipament.Count);
        }
        
        [Test]
        public void StartWithZeroExperience()
        {
            Assert.AreEqual(0, _survivor.experience);
        }
        
        [Test]
        public void StartWithBlueLevel()
        {
            Assert.AreEqual("Blue", _survivor.level);
        }
        
        [Test]
        public void WinExperienceWhenKillAZombie()
        {
            _survivor.experience = 3;
            _zombie.ReceiveDamage(_survivor);
            
            Assert.AreEqual(4, _survivor.experience);
        }
        
        [Test]
        public void LevelUpToYellowWhenHaveSixExperience()
        {
            _survivor.experience = 5;
            _survivor.ReceiveExperience(1);
            
            Assert.AreEqual("Yellow", _survivor.level);
        }
        
        [Test]
        public void LevelUpToOrangeWhenHaveEighteenExperience()
        {
            _survivor.experience = 17;

            _survivor.ReceiveExperience(1);
            
            Assert.AreEqual("Orange", _survivor.level);
        }
        [Test]
        public void LevelUpToRedWhenHaveFortyTwoExperience()
        {
            _survivor.experience = 41;

            _survivor.ReceiveExperience(1);
            
            Assert.AreEqual("Red", _survivor.level);
        }

        [Test]
        public void NotifyToGameThatLevelUp()
        {
            _survivor.experience = 17;
            _survivor.ReceiveExperience(1);

            _game.Received(1).ASurvivorLevelUp();
        }
        
    }
}
