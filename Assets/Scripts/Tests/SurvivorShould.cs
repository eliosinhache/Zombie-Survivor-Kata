using Classes;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using NUnit.Framework;

namespace Tests
{
    public class SurvivorShould
    {
        private Survivor _survivor;
        private ISkillTree _skillTree = Substitute.For<ISkillTree>();
        private Equipment _equipmentBaseballBat;
        private Equipment _equipmentKatana;
        private IGame _game = Substitute.For<IGame>();
        private IZombie _zombie = Substitute.For<IZombie>();
        [SetUp]
        public void SetUp()
        {
            _survivor = new Survivor("Juan", _game, _skillTree);
            _equipmentBaseballBat = new Equipment("Baseball bat");
            _equipmentKatana = new Equipment("Katana");
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
            _survivor.Equipate(_equipmentBaseballBat, "In Reserve");
            _survivor.Equipate(_equipmentKatana, "In Hand");
            _survivor.Equipate(_equipmentKatana, "In Hand");
            _survivor.Equipate(_equipmentKatana, "In Hand");
            
            
            Assert.AreEqual(2, _survivor.EquipmentInHand());
        }
        
        [Test]
        public void Have5EquipamentInReserve()
        {
            _survivor.Equipate(_equipmentBaseballBat, "In Reserve");
            _survivor.Equipate(_equipmentKatana, "In Reserve");
            _survivor.Equipate(_equipmentKatana, "In Hand");
            _survivor.Equipate(_equipmentKatana, "In Reserve");
            _survivor.Equipate(_equipmentKatana, "In Hand");
            _survivor.Equipate(_equipmentKatana, "In Reserve");
            _survivor.Equipate(_equipmentKatana, "In Hand");
            
            Assert.AreEqual(5, _survivor.equipment.Count);
        }
        
        [Test]
        public void HaveOneLessPiecesWhenReceiveWound()
        {
            _survivor.Equipate(_equipmentBaseballBat, "In Reserve");
            _survivor.Equipate(_equipmentKatana, "In Reserve");
            _survivor.Equipate(_equipmentKatana, "In Hand");
            _survivor.Equipate(_equipmentKatana, "In Reserve");
            _survivor.Equipate(_equipmentKatana, "In Reserve");
            
            _survivor.ReceiveWound();
            
            _survivor.Equipate(_equipmentKatana, "In Reserve");
            
            Assert.AreEqual(4, _survivor.equipment.Count);
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
        public void BeAbleToDamageAZombie()
        {
            _survivor.DealDamage(_zombie);
            _zombie.Received(1).ReceiveDamage(_survivor);
        }
        
        [Test]
        public void BeAbleToGainExperience()
        {
            _survivor.experience = 4;
            _survivor.GainExperience(1);
            
            Assert.AreEqual(5, _survivor.experience);
        }
        
        [Test]
        public void LevelUpToYellowWhenHaveSixExperience()
        {
            _survivor.experience = 5;
            _survivor.GainExperience(1);
            
            Assert.AreEqual("Yellow", _survivor.level);
        }
        
        [Test]
        public void LevelUpToOrangeWhenHaveEighteenExperience()
        {
            _survivor.experience = 17;

            _survivor.GainExperience(1);
            
            Assert.AreEqual("Orange", _survivor.level);
        }
        [Test]
        public void LevelUpToRedWhenHaveFortyTwoExperience()
        {
            _survivor.experience = 41;

            _survivor.GainExperience(1);
            
            Assert.AreEqual("Red", _survivor.level);
        }

        [Test]
        public void NotifyToGameThatLevelUp()
        {
            _survivor.experience = 17;
            _survivor.GainExperience(1);

            _game.Received(1).ASurvivorLevelUp(_survivor);
        }

        [Test]
        public void StartWithLockedSkills()
        {
            _skillTree.UnlockedSkills().Returns(0);
            Assert.AreEqual(0, _survivor.UnlockedSkills());
        }

        [Test]
        public void UnlockSkillWhenLevelUpToYellow()
        {
            _survivor.experience = 6;
            _survivor.GainExperience(1);
            
            // _skillTree.Received(1).UnlockSkill();
        }
        
    }
}
