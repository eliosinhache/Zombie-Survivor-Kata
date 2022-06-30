using Classes.Level;

namespace Classes.Skill
{
    public interface ISkill
    {
        LevelEnum GetLevelSkill();
        int MinExperienceNeeded();
        void Unlock();
        bool IsUnlock();
    }
}