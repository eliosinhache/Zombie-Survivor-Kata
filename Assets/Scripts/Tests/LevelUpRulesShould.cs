using System.Collections;
using System.Collections.Generic;
using Classes;
using Classes.Level;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class LevelUpRulesShould
    {
        private LevelUpRules _levelUpRules = new LevelUpRules();
        
        [Test]
        [TestCase(7, 1)]
        [TestCase(19, 2)]
        [TestCase(43, 3)]
        [TestCase(62, 4)]
        [TestCase(129, 5)]
        public void ShowCountOfSkillAvailableToUnlock(int experience, int countOfSkillsAvailable)
        {
            Assert.AreEqual(countOfSkillsAvailable, _levelUpRules.CountOfSkillsAvailableToUnlock(experience));
        }

        [Test]
        [TestCase(LevelEnum.Blue, LevelEnum.Yellow)]
        [TestCase(LevelEnum.Yellow, LevelEnum.Orange)]
        [TestCase(LevelEnum.Orange, LevelEnum.Red)]
        [TestCase(LevelEnum.Red, LevelEnum.Blue)]
        public void RetrieveNextLevelToLevelUp(LevelEnum actualLevel, LevelEnum nextLevel)
        {
            
            Assert.AreEqual(nextLevel, _levelUpRules.LevelUp(actualLevel));
        }

        [Test]
        [TestCase(7, LevelEnum.Blue)]
        [TestCase(19, LevelEnum.Yellow)]
        [TestCase(43, LevelEnum.Orange)]
        public void VerifyIfCanLevelUp(int experience, LevelEnum actualLevel)
        {
            Assert.IsTrue(_levelUpRules.CanLevelUp(experience, actualLevel));
        }
    }
}
