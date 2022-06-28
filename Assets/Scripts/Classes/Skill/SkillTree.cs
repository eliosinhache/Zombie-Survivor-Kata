using System.Collections.Generic;
using System.Linq;
using Classes.Level;

namespace Classes.Skill
{
    public class SkillTree : ISkillTree
    {
        private readonly List<ILevelSkills> _levelSkills = new List<ILevelSkills>();
        private readonly ILevelUpRules _levelUpRules;

        public SkillTree(ILevelUpRules levelUpRules)
        {
            _levelUpRules = levelUpRules;
        }
        public void AddNewSkill (ISkill skill)
        {
            foreach (var levelSkill in _levelSkills.Where(levelSkill => levelSkill.RetrieveLevelSkills() == skill.RetrieveLevelSkill()))
            {
                levelSkill.AddNewSkill(skill);
            }
        }

        public int RetrieveSkillsOfLevel(LevelEnum level)
        {
            var count = 0;
            foreach (ILevelSkills levelSkill in _levelSkills)
            {
                if (levelSkill.RetrieveLevelSkills() == level)
                {
                    count += levelSkill.RetrieveCountOfSkills();
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
            var unlockedSkills = new List<ISkill>();
            foreach (ILevelSkills levelSkills in _levelSkills)
            {
                unlockedSkills.AddRange(levelSkills.RetrieveListOfUnlockedSkills());
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
            var count = CountOfLockedSkills();
            return count;
        }

        public void UnlockSkill(ISkill unlockSkill, int survivorExperience)
        {
            if (unlockSkill.MinExperienceNeeded() > survivorExperience) { return;}
            if (IsExperienceEnoughToUnlockNewSkill(survivorExperience)) { UnlockSkill(unlockSkill);}
        }

        public List<ISkill> AvailableSkillsToUnlock(LevelEnum level)
        {
            List<ISkill> availableSkillsToUnlock = new List<ISkill>();

            foreach (var levelSkill in _levelSkills.Where(levelSkill => levelSkill.RetrieveLevelSkills() == level))
            {
                availableSkillsToUnlock = levelSkill.RetrieveListOfSkillsToUnlock();
            }

            return availableSkillsToUnlock;
        }

        private bool IsExperienceEnoughToUnlockNewSkill(int experience)
        {
            return NumberUnlockedSkills() < _levelUpRules.CountOfSkillsAvailableToUnlock(experience);
        }

        private void UnlockSkill(ISkill unlockSkill)
        {
            foreach (var levelSkill in _levelSkills.Where(levelSkill => levelSkill.RetrieveLevelSkills() == unlockSkill.RetrieveLevelSkill()))
            {
                levelSkill.UnlockSkill(unlockSkill);
            }
        }

        private void SearchAndUnlockSkill(Skill unlockSkill, List<Skill> levelSkills)
        {
            foreach (var skill in levelSkills.Where(skill => skill.description == unlockSkill.description))
            {
                skill.isUnlock = true;
            }
        }

        private int CountOfLockedSkills()
        {
            return _levelSkills.Sum(levelSkills => levelSkills.CountOfLockedSkills());
        }

        private int SearchCountOfSkillsPerLevel(LevelEnum level)
        {
            var count = 0;
            foreach (var levelSkills in _levelSkills.Where(levelSkills => levelSkills.RetrieveLevelSkills() == level))
            {
                count = levelSkills.RetrieveCountOfSkills();
            }
            return count;
        }
    }
}