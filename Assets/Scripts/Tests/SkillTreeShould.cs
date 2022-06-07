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
        [Test]
        public void StartWithOutSkillsUnlocked()
        {
            ISkillTree skillTree = new SkillTree();
            
            Assert.AreEqual(0, skillTree.UnlockedSkills());
        }
        
        [Test]
        [TestCase("Yellow", 1)]
        [TestCase("Orange", 2)]
        [TestCase("Red", 3)]
        public void HavePotentialSkills(string lvl, int skillCount)
        {
            ISkillTree skillTree = new SkillTree();
            
            Assert.AreEqual(skillCount, skillTree.avaibleSkills(lvl));
        }

        [Test]
        public void ReturnSkillsLocked()
        {
            ISkillTree skillTree = new SkillTree();
            
            Assert.AreEqual(6, skillTree.LockedSkills());
        }
    }
}
