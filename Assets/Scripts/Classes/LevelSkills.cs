using System;
using System.Collections.Generic;
using System.Linq;

namespace Classes
{
    public class LevelSkills : ILevelSkills
    {
        private List<ISkill> _skills = new List<ISkill>();
        private readonly LevelEnum _level;

        public LevelSkills(LevelEnum levelListOf)
        {
            _level = levelListOf;
        }
        
        public LevelEnum RetrieveLevelSkills()
        {
            return _level;
        }

        public int RetrieveCountOfSkills()
        {
            return _skills.Count;
        }

        public void AddNewSkill(ISkill skill)
        {
            _skills.Add(skill);
        }

        public void UnlockSkill(ISkill unlockSkill)
        {
            foreach (ISkill skill in _skills)
            {
                if (skill == unlockSkill)
                {
                    skill.Unlock();
                }
            }
        }

        public int CountOfUnlockedSkills()
        {
            return _skills.Count(x => x.IsUnlock());
        }

        public int CountOfLockedSkills()
        {
            return _skills.Count(x => !x.IsUnlock());
        }

        public IEnumerable<ISkill> RetrieveListOfUnlockedSkills()
        {
            return _skills.FindAll(x => x.IsUnlock());
        }

        public List<ISkill> RetrieveListOfSkillsToUnlock()
        {
            List<ISkill> SkillsToUnlock = new List<ISkill>();
            foreach (ISkill skill in _skills)
            {
                if (!skill.IsUnlock()) { SkillsToUnlock.Add(skill);}
            }

            return SkillsToUnlock;
        }
    }
}