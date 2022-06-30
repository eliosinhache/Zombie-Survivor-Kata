namespace Classes.Level
{
    public interface ILevelUpRules
    {
        int CountOfSkillsAvailableToUnlock(int experience);
        LevelEnum LevelUp(LevelEnum actualLevel);
        bool CanLevelUp(int experience, LevelEnum actualLevel);
    }
}