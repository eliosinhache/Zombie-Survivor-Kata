namespace Classes
{
    public interface ISkillTree
    {
        int UnlockedSkills();
        int EnabledSkills(string lvl);
        int LockedSkills();
        void UnlockSkill(Skill oneMoreAction, int experience);
    }
}