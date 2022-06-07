namespace Classes
{
    public interface ISkillTree
    {
        int UnlockedSkills();
        int avaibleSkills(string lvl);
        int LockedSkills();
    }
}