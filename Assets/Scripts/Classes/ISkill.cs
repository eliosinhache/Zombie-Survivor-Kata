namespace Classes
{
    public interface ISkill
    {
        LevelEnum RetrieveLevelSkill();
        int MinExperienceNeeded();
        void Unlock();
        bool IsUnlock();
    }
}