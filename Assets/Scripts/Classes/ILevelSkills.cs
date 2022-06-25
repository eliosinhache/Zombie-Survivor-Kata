using System.Collections.Generic;

namespace Classes
{
    public interface ILevelSkills
    {
        LevelEnum ReturnLevelSkills();
        int ReturnCountOfSkills();
        void AddNewSkill(ISkill skill);
        void UnlockSkill(ISkill unlockSkill);
        int CountOfUnlockedSkills();
        int CountOfLockedSkills();
        IEnumerable<ISkill> ReturnListOfUnlockedSkills();
        List<ISkill> ReturnListOfSkillsToUnlock();
    }
}