namespace Classes
{
    public class Skill : ISkill
    {
        public string description;
        public LevelEnum lvlToUnlock;
        private int _minExperienceNeeded;
        public bool isUnlock;
        public Skill(string description, LevelEnum levelToUnlock, int minExperienceNeeded)
        {
            this.description = description;
            lvlToUnlock = levelToUnlock;
            _minExperienceNeeded = minExperienceNeeded;
        }
        public LevelEnum ReturnLevelSkill()
        {
            return lvlToUnlock;
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