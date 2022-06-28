using Classes.Level;

namespace Classes.Skill
{
    public interface ISkill
    {
        LevelEnum RetrieveLevelSkill();
        int MinExperienceNeeded();
        void Unlock();
        bool IsUnlock();
    }
}