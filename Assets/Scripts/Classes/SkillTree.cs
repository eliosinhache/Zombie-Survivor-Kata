using System;
using System.Collections.Generic;
using System.Linq;

namespace Classes
{
    public class SkillTree : ISkillTree
    {
        private List<ILevelSkills> _levelSkills = new List<ILevelSkills>();
        private List<Skill> _levelYellowSkills = new List<Skill>();
        private List<Skill> _levelOrangeSkills = new List<Skill>();
        private List<Skill> _levelRedSkills = new List<Skill>();
        
        public void AddNewSkill (ISkill skill)
        {
            foreach (ILevelSkills levelSkill in _levelSkills)
            {
                if (levelSkill.ReturnLevelSkills() == skill.ReturnLevelSkill())
                {
                    levelSkill.AddNewSkill(skill);
                }
            }
        }

        public int ReturnSkillsOfLevel(LevelEnum level)
        {
            int count = 0;
            foreach (ILevelSkills levelSkill in _levelSkills)
            {
                if (levelSkill.ReturnLevelSkills() == level)
                {
                    count += levelSkill.ReturnCountOfSkills();
                }
            }

            return count;
        }

        public void AddNewLevelSkills(ILevelSkills levelSkills)
        {
            _levelSkills.Add(levelSkills);
        }

        public List<ISkill> ListOfUnlockedSkills()
        {
            List<ISkill> unlockedSkills = new List<ISkill>();
            foreach (ILevelSkills levelSkills in _levelSkills)
            {
                foreach (ISkill skill in levelSkills.ReturnListOfUnlockedSkills())
                {
                    unlockedSkills.Add(skill);
                }
            }

            return unlockedSkills;
        }


        public int NumberUnlockedSkills ()
        {
            return _levelSkills.Sum(levelSkill => levelSkill.CountOfUnlockedSkills());
        }
        public int EnabledSkills(LevelEnum level)
        {
            return SearchCountOfSkillsPerLevel(level);
        }

        public int LockedSkills()
        {
            int count = CountOfLockedSkills();
            return count;
        }

        public void UnlockSkill(ISkill unlockSkill, int survivorExperience)
        {
            if (unlockSkill.MinExperienceNeeded() > survivorExperience) { return;}
            if (IsExperienceEnoughToUnlockNewSkill(survivorExperience)) { UnlockSkill(unlockSkill);}
        }

        public List<ISkill> AvaibleSkillsToUnlock(LevelEnum level)
        {
            List<ISkill> availableSkillsToUnlock = new List<ISkill>();

            foreach (ILevelSkills levelSkill in _levelSkills)  
            {
                if (levelSkill.ReturnLevelSkills() == level)
                {
                    availableSkillsToUnlock = levelSkill.ReturnListOfSkillsToUnlock();
                }
            }

            return availableSkillsToUnlock;
        }

        private bool IsExperienceEnoughToUnlockNewSkill(int experience)
        {
            if (experience >= 7 && experience < 18)
            {
                return NumberUnlockedSkills() < 1;
            }
            if (experience >= 18 && experience < 42)
            {
                return NumberUnlockedSkills() < 2;
            }

            if (experience >= 42 && experience < 61)
            {
                return NumberUnlockedSkills() < 3;
            }

            if (experience >= 61 && experience < 86)
            {
                return NumberUnlockedSkills() < 4;
            }
            if (experience >= 86 && experience < 129)
            {
                return NumberUnlockedSkills() < 5;
            }

            return NumberUnlockedSkills() < 6;
        }

        private void UnlockSkill(ISkill unlockSkill)
        {
            foreach (ILevelSkills levelSkill in _levelSkills)
            {
                if (levelSkill.ReturnLevelSkills() == unlockSkill.ReturnLevelSkill())
                {
                    levelSkill.UnlockSkill(unlockSkill);
                }
            }
        }

        private void SearchAndUnlockSkill(Skill unlockSkill, List<Skill> levelSkills)
        {
            foreach (Skill skill in levelSkills)
            {
                if (skill.description == unlockSkill.description)
                {
                    skill.isUnlock = true;
                }
            }
        }

        private int CountOfLockedSkills()
        {
            int count = 0;
            foreach (ILevelSkills levelSkills in _levelSkills)
            {
                count += levelSkills.CountOfLockedSkills();
            }
            return count;
        }

        private int SearchCountOfSkillsPerLevel(LevelEnum level)
        {
            int count = 0;
            switch (level)
            {
                case LevelEnum.Blue:
                    count = 0;
                    break;
                case LevelEnum.Yellow:
                    count += _levelYellowSkills.Count(item => !item.isUnlock);
                    break;
                case LevelEnum.Orange:
                    count += _levelOrangeSkills.Count(item => !item.isUnlock);
                    break;
                case LevelEnum.Red:
                    count += _levelRedSkills.Count(item =>  !item.isUnlock);
                    break;
            } 
            return count;
        }
    }
}