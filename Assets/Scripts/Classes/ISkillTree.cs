using System.Collections.Generic;

namespace Classes
{
    public interface ISkillTree
    {
        int NumberUnlockedSkills();
        int EnabledSkills(LevelEnum level);
        int LockedSkills();
        void UnlockSkill(ISkill skillToUnlock, int survivorExperience);
        List<ISkill> AvailableSkillsToUnlock(LevelEnum level);
        void AddNewSkill(ISkill skill);
        int RetrieveSkillsOfLevel(LevelEnum orange);
        void AddNewLevelSkills(ILevelSkills levelSkills);
        List<ISkill> ListOfUnlockedSkills();
    }
}