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
        [TestCase("Blue", 0)]
        [TestCase("Yellow", 1)]
        [TestCase("Orange", 2)]
        [TestCase("Red", 3)]
        public void HavePotentialSkills(string level, int skillCount)
        {
            Assert.AreEqual(skillCount, _skillTree.EnabledSkills(level));
        }

        [Test]
        public void ReturnSkillsLocked()
        {
            
            Assert.AreEqual(6, _skillTree.LockedSkills());
        }

        [Test]
        public void UnlockSkill()
        {
            Skill newSkillLevelYellow = new Skill();
            newSkillLevelYellow.description = "+1 Action";
            newSkillLevelYellow.lvlToUnlock = "Yellow";
            newSkillLevelYellow.minExperienceNeeded = 7;

            Skill newSkillLevelRed = new Skill();
            newSkillLevelRed.description = "Sniper";
            newSkillLevelRed.lvlToUnlock = "Red";
            newSkillLevelRed.minExperienceNeeded = 42;

            int experience = 45;
            
            _skillTree.UnlockSkill(newSkillLevelYellow, experience);
            _skillTree.UnlockSkill(newSkillLevelRed, experience);
            
            Assert.AreEqual(2, _skillTree.UnlockedSkills());
        }
        
        [Test]
        public void UnlockNewSkillWhenExperienceHigherThanTreeLevel()
        {
            Skill newSkillLevelYellow = new Skill();
            newSkillLevelYellow.description = "+1 Action";
            newSkillLevelYellow.lvlToUnlock = "Yellow";
            newSkillLevelYellow.minExperienceNeeded = 7;
            
            Skill newSkillLevelOrange = new Skill();
            newSkillLevelOrange.description = "+1 Die";
            newSkillLevelOrange.lvlToUnlock = "Orange";
            newSkillLevelOrange.minExperienceNeeded = 18;

            Skill newSkillLevelOrangeRestart = new Skill();
            newSkillLevelOrangeRestart.description = "+1 Free Move Action";
            newSkillLevelOrangeRestart.lvlToUnlock = "Orange";
            newSkillLevelOrangeRestart.minExperienceNeeded = 18;

            Skill newSkillLevelRed = new Skill();
            newSkillLevelRed.description = "Sniper";
            newSkillLevelRed.lvlToUnlock = "Red";
            newSkillLevelRed.minExperienceNeeded = 42;

            int experience = 60;
            
            _skillTree.UnlockSkill(newSkillLevelYellow, experience);
            _skillTree.UnlockSkill(newSkillLevelOrange, experience);
            _skillTree.UnlockSkill(newSkillLevelRed, experience);
            _skillTree.UnlockSkill(newSkillLevelOrangeRestart, experience);
            
            Assert.AreEqual(3, _skillTree.UnlockedSkills());
        }
        
        
    }
}
