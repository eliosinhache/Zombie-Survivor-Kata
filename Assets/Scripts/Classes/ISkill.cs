namespace Classes
{
    public interface ISkill
    {
        LevelEnum ReturnLevelSkill();
        int MinExperienceNeeded();
        void Unlock();
        bool IsUnlock();
    }
}