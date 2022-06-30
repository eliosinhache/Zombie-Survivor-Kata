using System.Collections;
using System.Collections.Generic;
using Classes;
using Classes.Level;
using Classes.Skill;
using NSubstitute;
using NSubstitute.Exceptions;
using NSubstitute.ReceivedExtensions;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class SkillTreeShould
    {
        private ISkillTree _skillTree;
        private ILevelSkills _levelSkills;
        private ISkill _skill01;
        private ISkill _skill02;
        private ILevelSkills _levelSkillsBlue ;        
        private ILevelSkills _levelSkillsYellow;
        private ILevelSkills _levelSkillsOrange;
        private ILevelSkills _levelSkillsRed;
        private ILevelUpRules _levelUpRules;
        
        [SetUp]
        public void SetUp()
        {
            _skill01 = Substitute.For<ISkill>();
            _skill02 = Substitute.For<ISkill>();
            _levelSkills = Substitute.For<ILevelSkills>();
            _levelUpRules = Substitute.For<ILevelUpRules>();
            
            _levelSkillsBlue = Substitute.For<ILevelSkills>();
            _levelSkillsYellow = Substitute.For<ILevelSkills>();
            _levelSkillsOrange = Substitute.For<ILevelSkills>();
            _levelSkillsRed = Substitute.For<ILevelSkills>();
            _levelSkillsBlue.GetSkillsLevel().Returns(LevelEnum.Blue);
            _levelSkillsYellow.GetSkillsLevel().Returns(LevelEnum.Yellow);
            _levelSkillsOrange.GetSkillsLevel().Returns(LevelEnum.Orange);
            _levelSkillsRed.GetSkillsLevel().Returns(LevelEnum.Red);
            
            _skillTree  = new SkillTree(_levelUpRules);
        }

        [Test]
        [TestCase(LevelEnum.Blue)]
        [TestCase(LevelEnum.Yellow)]
        [TestCase(LevelEnum.Orange)]
        [TestCase(LevelEnum.Red)]
        public void AddNewSkillToCorrectList(LevelEnum levelSkill)
        {
            FillSkillTreeWithAllLevelSkills();
            
            _skill01.GetLevelSkill().Returns(levelSkill);
            
            _skillTree.AddNewSkill(_skill01);
            ILevelSkills _levelSkillsExpected = RetrieveLevelSkill(levelSkill);
            _levelSkillsExpected.Received(1).AddNewSkill(Arg.Any<ISkill>());
        }

        private void FillSkillTreeWithAllLevelSkills()
        {
            _skillTree.AddNewLevelSkills(_levelSkillsBlue);
            _skillTree.AddNewLevelSkills(_levelSkillsYellow);
            _skillTree.AddNewLevelSkills(_levelSkillsOrange);
            _skillTree.AddNewLevelSkills(_levelSkillsRed);
        }

        private ILevelSkills RetrieveLevelSkill(LevelEnum levelSkill)
        {
            ILevelSkills varToReturn = Substitute.For<ILevelSkills>();
            switch (levelSkill)
            {
                case LevelEnum.Blue:
                    varToReturn = _levelSkillsBlue;
                    break;
                case LevelEnum.Yellow:
                    varToReturn = _levelSkillsYellow;
                    break;
                case LevelEnum.Orange:
                    varToReturn = _levelSkillsOrange;
                    break;
                case LevelEnum.Red:
                    varToReturn = _levelSkillsRed;
                    break;
            }

            return varToReturn;
        }

        [Test]
        public void StartWithOutSkillsUnlocked()
        {
            _levelSkills.CountOfUnlockedSkills().Returns(0);
            _skillTree.AddNewLevelSkills(_levelSkills);

            ILevelSkills levelSkills = Substitute.For<ILevelSkills>();
            levelSkills.CountOfUnlockedSkills().Returns(0);
            _skillTree.AddNewLevelSkills(levelSkills);
            
            Assert.AreEqual(0, _skillTree.NumberUnlockedSkills());
        }
        
        [Test]
        [TestCase(LevelEnum.Blue, 0)]
        [TestCase(LevelEnum.Yellow, 1)]
        [TestCase(LevelEnum.Orange, 2)]
        [TestCase(LevelEnum.Red, 3)]
        public void HavePotentialSkills(LevelEnum level, int skillCount)
        {
            FillSkillTreeWithAllLevelSkills();
            _skill01.GetLevelSkill().Returns(level);
            int skillToAdd = skillCount;
            while (skillToAdd > 0)
            {
                _skillTree.AddNewSkill(_skill01);
                skillToAdd--;
            }
            
            ILevelSkills _levelSkillsExpected = RetrieveLevelSkill(level);
            _levelSkillsExpected.Received(skillCount).AddNewSkill(Arg.Any<ISkill>());
        }

        [Test]
        public void ReturnSkillsLocked()
        {
            FillSkillTreeWithAllLevelSkills();
            _levelSkillsYellow.CountOfLockedSkills().Returns(1);
            _levelSkillsOrange.CountOfLockedSkills().Returns(2);
            _levelSkillsRed.CountOfLockedSkills().Returns(3);
            
            Assert.AreEqual(6, _skillTree.LockedSkills());
        }

        [Test]
        public void UnlockNewSkill()
        {
            _levelSkillsBlue.CountOfUnlockedSkills().Returns(0);
            _levelSkillsYellow.CountOfUnlockedSkills().Returns(0);
            _levelSkillsOrange.CountOfUnlockedSkills().Returns(0);
            _levelSkillsRed.CountOfUnlockedSkills().Returns(0);
            FillSkillTreeWithAllLevelSkills();
            _levelUpRules.CountOfSkillsAvailableToUnlock(Arg.Any<int>()).Returns(3);
             
            int experience = 46;

            _skill01.GetLevelSkill().Returns(LevelEnum.Yellow);
            _skill01.MinExperienceNeeded().Returns(7);
            _skill02.GetLevelSkill().Returns(LevelEnum.Red);
            _skill02.MinExperienceNeeded().Returns(42);
            
            _skillTree.UnlockSkill(_skill01, experience);
            _skillTree.UnlockSkill(_skill02, experience);

            _levelSkillsYellow.Received(1).UnlockSkill(Arg.Any<ISkill>());
            _levelSkillsRed.Received(1).UnlockSkill(Arg.Any<ISkill>());
        }
        
        [Test]
        public void UnlockNewSkillWhenExperienceHigherThanTreeLevel()
        {
            _skill01.MinExperienceNeeded().Returns(19);
            _skill01.GetLevelSkill().Returns(LevelEnum.Orange);
            _levelSkillsYellow.CountOfUnlockedSkills().Returns(1);
            _levelSkillsOrange.CountOfUnlockedSkills().Returns(1);
            _levelSkillsRed.CountOfUnlockedSkills().Returns(1);
            FillSkillTreeWithAllLevelSkills();
            
            int experience = 60;
            _levelUpRules.CountOfSkillsAvailableToUnlock(Arg.Any<int>()).Returns(4);
            
            _skillTree.UnlockSkill(_skill01, experience);
            
            _levelSkillsOrange.Received(1).UnlockSkill(Arg.Any<ISkill>());
        }

        [Test]
        public void ShowAvailableSkillToUnlock()
        {
            FillSkillTreeWithAllLevelSkills();
            _skill01.GetLevelSkill().Returns(LevelEnum.Yellow);
            List<ISkill> expectedList = new List<ISkill> {_skill01};
            _levelSkillsYellow.RetrieveListOfSkillsToUnlock().Returns(expectedList);
            
            _skillTree.AddNewSkill(_skill01);
            
            Assert.IsTrue(_skillTree.AvailableSkillsToUnlock(LevelEnum.Yellow).Count == 1);
        }

    }
}
