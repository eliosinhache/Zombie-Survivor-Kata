using System.Collections.Generic;
using Classes.Level;

namespace Classes.Skill
{
    public interface ISkillTree
    {
        int NumberUnlockedSkills();
        int EnabledSkills(LevelEnum level);
        int LockedSkills();
        void UnlockSkill(ISkill skillToUnlock, int survivorExperience);
        List<ISkill> AvailableSkillsToUnlock(LevelEnum level);
        void AddNewSkill(ISkill skill);
        int CountSkillsOfLevel(LevelEnum orange);
        void AddNewLevelSkills(ILevelSkills levelSkills);
        List<ISkill> ListOfUnlockedSkills();
    }
}