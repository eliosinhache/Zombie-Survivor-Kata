using System.Collections;
using System.Collections.Generic;
using Classes;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class SkillTreeShould
    {
        private ISkillTree _skillTree;
            
        [SetUp]
        public void SetUp()
        {
            _skillTree  = new SkillTree();
        }
        
        [Test]
        public void StartWithOutSkillsUnlocked()
        {
            
            Assert.AreEqual(0, _skillTree.UnlockedSkills());
        }
        
        [Test]
        [TestCase(LevelEnum.Blue, 0)]
        [TestCase(LevelEnum.Yellow, 1)]
        [TestCase(LevelEnum.Orange, 2)]
        [TestCase(LevelEnum.Red, 3)]
        public void HavePotentialSkills(LevelEnum level, int skillCount)
        {
            Assert.AreEqual(skillCount, _skillTree.EnabledSkills(level));
        }

        [Test]
        public void ReturnSkillsLocked()
        {
            
            Assert.AreEqual(6, _skillTree.LockedSkills());
        }

        [Test]
        public void UnlockNewSkill()
        {
            Skill newSkillLevelYellow = new Skill();
            newSkillLevelYellow.description = "+1 Action";
            newSkillLevelYellow.lvlToUnlock = LevelEnum.Yellow;
            newSkillLevelYellow.minExperienceNeeded = 7;

            Skill newSkillLevelRed = new Skill();
            newSkillLevelRed.description = "Sniper";
            newSkillLevelRed.lvlToUnlock = LevelEnum.Red;
            newSkillLevelRed.minExperienceNeeded = 42;

            int experience = 46;
            
            _skillTree.UnlockSkill(newSkillLevelYellow, experience);
            _skillTree.UnlockSkill(newSkillLevelRed, experience);
            
            Assert.AreEqual(2, _skillTree.UnlockedSkills());
        }
        
        [Test]
        public void UnlockNewSkillWhenExperienceHigherThanTreeLevel()
        {
            Skill newSkillLevelYellow = new Skill();
            newSkillLevelYellow.description = "+1 Action";
            newSkillLevelYellow.lvlToUnlock = LevelEnum.Yellow;
            newSkillLevelYellow.minExperienceNeeded = 7;
            
            Skill newSkillLevelOrange = new Skill();
            newSkillLevelOrange.description = "+1 Die";
            newSkillLevelOrange.lvlToUnlock = LevelEnum.Orange;
            newSkillLevelOrange.minExperienceNeeded = 18;

            Skill newSkillLevelOrangeRestart = new Skill();
            newSkillLevelOrangeRestart.description = "+1 Free Move Action";
            newSkillLevelOrangeRestart.lvlToUnlock = LevelEnum.Orange;
            newSkillLevelOrangeRestart.minExperienceNeeded = 18;

            Skill newSkillLevelRed = new Skill();
            newSkillLevelRed.description = "Sniper";
            newSkillLevelRed.lvlToUnlock = LevelEnum.Orange;
            newSkillLevelRed.minExperienceNeeded = 42;

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

            Skill skill4 = new Skill();
            skill4.description = "Hoard";
            skill4.lvlToUnlock = LevelEnum.Red;
            skill4.minExperienceNeeded = 42;
            
            Skill skill5 = new Skill();
            skill5.description = "Tough";
            skill5.lvlToUnlock = LevelEnum.Red;
            skill5.minExperienceNeeded = 42;
            
            Skill skill6 = new Skill();
            skill6.description = "Sniper";
            skill6.lvlToUnlock = LevelEnum.Red;
            skill6.minExperienceNeeded = 42;
            
            
            Assert.IsTrue(_skillTree.AvaibleSkillsToUnlock(LevelEnum.Red).Exists(item => item.description == skill4.description));
            Assert.IsTrue(_skillTree.AvaibleSkillsToUnlock(LevelEnum.Red).Exists(item => item.description == skill5.description));
            Assert.IsTrue(_skillTree.AvaibleSkillsToUnlock(LevelEnum.Red).Exists(item => item.description == skill5.description));
        }

        private void UnlockFirstTowSkills()
        {
            Skill newSkillLevelYellow = new Skill();
            newSkillLevelYellow.description = "+1 Action";
            newSkillLevelYellow.lvlToUnlock = LevelEnum.Yellow;
            newSkillLevelYellow.minExperienceNeeded = 7;

            Skill newSkillLevelOrange = new Skill();
            newSkillLevelOrange.description = "+1 Die";
            newSkillLevelOrange.lvlToUnlock = LevelEnum.Orange;
            newSkillLevelOrange.minExperienceNeeded = 18;
            int experience = 43;

            _skillTree.UnlockSkill(newSkillLevelYellow, experience);
            _skillTree.UnlockSkill(newSkillLevelOrange, experience);
        }
    }
}
