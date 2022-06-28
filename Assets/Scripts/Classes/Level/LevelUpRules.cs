namespace Classes.Level
{
    public class LevelUpRules : ILevelUpRules
    {
        public int CountOfSkillsAvailableToUnlock(int experience)
        {
            if (IsExperienceBelongYellow(experience))
            {
                return 1;
            }
            if (IsExperienceBelongOrange(experience))
            {
                return 2;
            }
            if (IsExperienceBelongRed(experience))
            {
                return  3;
            }
            if (experience >= 62 && experience < 129)
            {
                return  4;
            }
            if (experience >= 129)
            {
                return  5;
            }

            return 0;
        }

        private static bool IsExperienceBelongRed(int experience)
        {
            return experience >= 43 && experience < 62;
        }

        private static bool IsExperienceBelongOrange(int experience)
        {
            return experience >= 19 && experience < 43;
        }

        private static bool IsExperienceBelongYellow(int experience)
        {
            return experience >= 7 && experience < 19;
        }

        public LevelEnum LevelUp(LevelEnum actualLevel)
        {
            switch (actualLevel)
            {
                case LevelEnum.Blue:
                    return LevelEnum.Yellow;
                case LevelEnum.Yellow:
                    return LevelEnum.Orange;
                case LevelEnum.Orange:
                    return LevelEnum.Red;
                case LevelEnum.Red:
                    return LevelEnum.Blue;
                default:
                    return LevelEnum.Blue;
            }
        }

        public bool CanLevelUp(int experience, LevelEnum actualLevel)
        {
            switch (actualLevel)
            {
                case LevelEnum.Blue:
                    return IsExperienceBelongYellow(experience);
                case LevelEnum.Yellow:
                    return IsExperienceBelongOrange(experience);
                case LevelEnum.Orange:
                    return IsExperienceBelongRed(experience);
                default:
                    return true;
            }
        }
    }

    public interface ILevelUpRules
    {
        int CountOfSkillsAvailableToUnlock(int experience);
        LevelEnum LevelUp(LevelEnum actualLevel);
        bool CanLevelUp(int experience, LevelEnum actualLevel);
    }
}