using Classes;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using NUnit.Framework;

namespace Tests
{
    public class SurvivorShould
    {
        private Survivor _survivor;
        private ISkillTree _skillTree ;
        private Equipment _equipmentBaseballBat;
        private Equipment _equipmentKatana;
        private IGame _game ;
        private IZombie _zombie;
        private ILevelUpRules _levelUpRules;
        [SetUp]
        public void SetUp()
        {
            _zombie = Substitute.For<IZombie>();
            _levelUpRules = Substitute.For<ILevelUpRules>();
            _game = Substitute.For<IGame>();
            _skillTree = Substitute.For<ISkillTree>();
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
        public void HaveTwoEquipmentInHand()
        {
            _survivor.Equip(_equipmentBaseballBat, "In Reserve");
            _survivor.Equip(_equipmentKatana, "In Hand");
            _survivor.Equip(_equipmentKatana, "In Hand");
            _survivor.Equip(_equipmentKatana, "In Hand");
            
            
            Assert.AreEqual(2, _survivor.EquipmentInHand());
        }
        
        [Test]
        public void HaveMaxOfEquipmentInReserve()
        {
            _survivor.Equip(_equipmentBaseballBat, "In Reserve");
            _survivor.Equip(_equipmentKatana, "In Reserve");
            _survivor.Equip(_equipmentKatana, "In Hand");
            _survivor.Equip(_equipmentKatana, "In Reserve");
            _survivor.Equip(_equipmentKatana, "In Hand");
            _survivor.Equip(_equipmentKatana, "In Reserve");
            _survivor.Equip(_equipmentKatana, "In Hand");
            
            Assert.AreEqual(5, _survivor.equipment.Count);
        }
        
        [Test]
        public void HaveOneLessPiecesWhenReceiveWound()
        {
            _survivor.Equip(_equipmentBaseballBat, "In Reserve");
            _survivor.Equip(_equipmentKatana, "In Reserve");
            _survivor.Equip(_equipmentKatana, "In Hand");
            _survivor.Equip(_equipmentKatana, "In Reserve");
            _survivor.Equip(_equipmentKatana, "In Reserve");
            
            _survivor.ReceiveWound();
            
            _survivor.Equip(_equipmentKatana, "In Reserve");
            
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
            Assert.AreEqual(LevelEnum.Blue, _survivor.level);
        }
        
        [Test]
        public void BeAbleToDamageAZombie()
        {
            _survivor.DealDamage(_zombie);
            _zombie.Received(1).ReceiveDamage(_survivor);
        }

        [Test]
        public void StartWithLife()
        {
            Assert.AreEqual(2, _survivor.RetrieveLifes());
        }
        
        [Test]
        public void BeAbleToGainExperience()
        {
            _survivor.experience = 4;
            _survivor.GainExperience(1);
            
            Assert.AreEqual(5, _survivor.experience);
        }
        
        [Test]
        [TestCase(6, LevelEnum.Blue, LevelEnum.Yellow)]
        [TestCase(18, LevelEnum.Yellow, LevelEnum.Orange)]
        [TestCase(42, LevelEnum.Orange, LevelEnum.Red)]
        public void LevelUpWhenHaveEnoughExperience(int experience, LevelEnum actualLevel, LevelEnum nextLevel)
        {
            _survivor.experience = experience;
            _survivor.level = actualLevel;
            _levelUpRules.CanLevelUp(Arg.Any<int>(), Arg.Any<LevelEnum>()).Returns(true);
            _survivor.GainExperience(1);
            
            Assert.AreEqual(nextLevel, _survivor.level);
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
            _skillTree.NumberUnlockedSkills().Returns(0);
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
