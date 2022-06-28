using System.Collections.Generic;

namespace Classes
{
    public interface ILevelSkills
    {
        LevelEnum RetrieveLevelSkills();
        int RetrieveCountOfSkills();
        void AddNewSkill(ISkill skill);
        void UnlockSkill(ISkill unlockSkill);
        int CountOfUnlockedSkills();
        int CountOfLockedSkills();
        IEnumerable<ISkill> RetrieveListOfUnlockedSkills();
        List<ISkill> RetrieveListOfSkillsToUnlock();
    }
}