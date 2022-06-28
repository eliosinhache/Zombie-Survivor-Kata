using System.Collections;
using System.Collections.Generic;
using Classes;
using Classes.Level;
using Classes.Skill;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class SkillShould
    {
        private ISkillTree _skillTree = Substitute.For<ISkillTree>();
        // private List<ILevelSkills> _skillTree = Substitute.For<List<ILevelSkills>>();
        private ISkill _skill;
        [Test]
        public void AddToCorrectLevelListOnSkillTree()
        {
            _skill = new Skill("+1 damage", LevelEnum.Orange, 24);
            _skillTree.AddNewSkill(_skill);

            
        }
    }
}
