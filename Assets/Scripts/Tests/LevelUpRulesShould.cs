﻿using System.Collections;
using System.Collections.Generic;
using Classes;
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
    }
}
