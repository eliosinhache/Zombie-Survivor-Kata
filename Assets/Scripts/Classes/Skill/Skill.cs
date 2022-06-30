using Classes.Level;

namespace Classes.Skill
{
    public class Skill : ISkill
    {
        public readonly string description;
        private readonly LevelEnum _lvlToUnlock;
        private readonly int _minExperienceNeeded;
        public bool isUnlock;
        public Skill(string description, LevelEnum levelToUnlock, int minExperienceNeeded)
        {
            this.description = description;
            _lvlToUnlock = levelToUnlock;
            _minExperienceNeeded = minExperienceNeeded;
        }
        public LevelEnum GetLevelSkill()
        {
            return _lvlToUnlock;
        }

        public int MinExperienceNeeded()
        {
            return _minExperienceNeeded;
        }

        public void Unlock()
        {
            isUnlock = true;
        }

        public bool IsUnlock()
        {
            return isUnlock;
        }
    }
}