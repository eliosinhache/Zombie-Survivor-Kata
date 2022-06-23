using System.Collections;
using System.Collections.Generic;
using Classes;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class SkillTreeShould
    {
        private ISkillTree _skillTree;
        private ILevelSkills _levelSkills;
        private ISkill _skill01 = Substitute.For<ISkill>();
        private ISkill _skill02 = Substitute.For<ISkill>();
        [SetUp]
        public void SetUp()
        {
            _skillTree  = new SkillTree();
        }

        [Test]
        [TestCase(LevelEnum.Blue)]
        [TestCase(LevelEnum.Yellow)]
        [TestCase(LevelEnum.Orange)]
        [TestCase(LevelEnum.Red)]
        public void AddNewSkillToCorrectList(LevelEnum levelSkill)
        {
            _skillTree = new SkillTree();
            AddNewLevelSkillsToSkillsTree(LevelEnum.Blue);
            AddNewLevelSkillsToSkillsTree(LevelEnum.Yellow);
            AddNewLevelSkillsToSkillsTree(LevelEnum.Orange);
            AddNewLevelSkillsToSkillsTree(LevelEnum.Red);
            
            _skill01.ReturnLevelSkill().Returns(levelSkill);
            _skillTree.AddNewSkill(_skill01);
            
            Assert.AreEqual(1, _skillTree.ReturnSkillsOfLevel(levelSkill));
        }

        private void AddNewLevelSkillsToSkillsTree(LevelEnum levelSkill)
        {
            _levelSkills = new LevelSkills(levelSkill);
            _skillTree.AddNewLevelSkills(_levelSkills);
        }

        [Test]
        public void StartWithOutSkillsUnlocked()
        {
            _levelSkills = Substitute.For<ILevelSkills>();
            _levelSkills.CountOfUnlockedSkills().Returns(0);
            _skillTree.AddNewLevelSkills(_levelSkills);

            ILevelSkills levelSkills = Substitute.For<ILevelSkills>();
            levelSkills.CountOfUnlockedSkills().Returns(0);
            _skillTree.AddNewLevelSkills(levelSkills);
            
            Assert.AreEqual(0, _skillTree.UnlockedSkills());
        }
        
        [Test]
        [TestCase(LevelEnum.Blue, 0)]
        [TestCase(LevelEnum.Yellow, 1)]
        [TestCase(LevelEnum.Orange, 2)]
        [TestCase(LevelEnum.Red, 3)]
        public void HavePotentialSkills(LevelEnum level, int skillCount)
        {
            _skillTree = new SkillTree();
            AddNewLevelSkillsToSkillsTree(level);
            _skill01.ReturnLevelSkill().Returns(level);
            while (skillCount > 0)
            {
                _skillTree.AddNewSkill(_skill01);
                skillCount--;
            }
            
            
            Assert.AreEqual(skillCount, _skillTree.EnabledSkills(level));
        }

        [Test]
        public void ReturnSkillsLocked()
        {
            ILevelSkills levelSkillsSubstituteYellow = Substitute.For<ILevelSkills>();
            levelSkillsSubstituteYellow.CountOfUnlockedSkills().Returns(2);
            levelSkillsSubstituteYellow.ReturnLevelSkills().Returns(LevelEnum.Yellow);
            _skill01.IsUnlock().Returns(false);
            _skill01.ReturnLevelSkill().Returns(LevelEnum.Yellow);
            AddNewLevelSkillsToSkillsTree(LevelEnum.Yellow);
            
            _skillTree.AddNewLevelSkills(levelSkillsSubstituteYellow);
            _skillTree.AddNewSkill(_skill01);
            _skillTree.AddNewSkill(_skill01);
            
            Assert.AreEqual(2, _skillTree.LockedSkills());
        }

        [Test]
        public void UnlockNewSkill()
        {
             ILevelSkills _levelSkillsSubstituteYellow = Substitute.For<ILevelSkills>();
             ILevelSkills _levelSkillsSubstituteRed = Substitute.For<ILevelSkills>();
             _levelSkillsSubstituteYellow.CountOfUnlockedSkills().Returns(0);
             _levelSkillsSubstituteRed.CountOfUnlockedSkills().Returns(0);
             _levelSkillsSubstituteYellow.ReturnLevelSkills().Returns(LevelEnum.Yellow);
             _levelSkillsSubstituteRed.ReturnLevelSkills().Returns(LevelEnum.Red);
             _skillTree.AddNewLevelSkills(_levelSkillsSubstituteYellow);
             _skillTree.AddNewLevelSkills(_levelSkillsSubstituteRed);
             
            int experience = 46;

            _skill01.ReturnLevelSkill().Returns(LevelEnum.Yellow);
            _skill01.MinExperienceNeeded().Returns(7);
            _skill02.ReturnLevelSkill().Returns(LevelEnum.Red);
            _skill02.MinExperienceNeeded().Returns(42);
            
            _skillTree.UnlockSkill(_skill01, experience);
            _skillTree.UnlockSkill(_skill02, experience);

            Assert.AreEqual(2, _skillTree.ListOfUnlockedSkills().Count);
        }
        
        [Test]
        public void UnlockNewSkillWhenExperienceHigherThanTreeLevel()
        {
            Skill newSkillLevelYellow = new Skill("+1 Action", LevelEnum.Yellow, 7);
            Skill newSkillLevelOrange = new Skill("+1 Die", LevelEnum.Orange, 18);
            Skill newSkillLevelOrangeRestart = new Skill("+1 Free Move Action", LevelEnum.Orange, 18);
            Skill newSkillLevelRed = new Skill("Sniper", LevelEnum.Orange, 42);
            
            int experience = 60;
            
            _skillTree.UnlockSkill(newSkillLevelYellow, experience);
            _skillTree.UnlockSkill(newSkillLevelOrange, experience);
            _skillTree.UnlockSkill(newSkillLevelRed, experience);
            _skillTree.UnlockSkill(newSkillLevelOrangeRestart, experience);
            
            Assert.AreEqual(3, _skillTree.UnlockedSkills());
        }

        [Test]
        public void ShowAvailableSkillToUnlock()
        {
            UnlockFirstTowSkills();

            Skill skill4 = new Skill("Hoard", LevelEnum.Red, 42);
            Skill skill5 = new Skill("Tough", LevelEnum.Red, 42);
            Skill skill6 = new Skill("Sniper", LevelEnum.Red, 42);
            
            
            Assert.IsTrue(_skillTree.AvaibleSkillsToUnlock(LevelEnum.Red).Exists(item => item.description == skill4.description));
            Assert.IsTrue(_skillTree.AvaibleSkillsToUnlock(LevelEnum.Red).Exists(item => item.description == skill5.description));
            Assert.IsTrue(_skillTree.AvaibleSkillsToUnlock(LevelEnum.Red).Exists(item => item.description == skill5.description));
        }

        private void UnlockFirstTowSkills()
        {
            Skill newSkillLevelYellow = new Skill("+1 Action", LevelEnum.Yellow, 7);
            Skill newSkillLevelOrange = new Skill("+1 Die", LevelEnum.Orange, 18);
            
            int experience = 43;

            _skillTree.UnlockSkill(newSkillLevelYellow, experience);
            _skillTree.UnlockSkill(newSkillLevelOrange, experience);
        }
    }
}
