using System.Collections.Generic;

namespace Classes
{
    public interface ISkillTree
    {
        int UnlockedSkills();
        int EnabledSkills(LevelEnum level);
        int LockedSkills();
        void UnlockSkill(Skill skillToUnlock, int experience);
        List<Skill> AvaibleSkillsToUnlock(LevelEnum level);
    }
}