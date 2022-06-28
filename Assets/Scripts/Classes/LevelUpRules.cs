namespace Classes
{
    public class LevelUpRules : ILevelUpRules
    {
        public int CountOfSkillsAvailableToUnlock(int experience)
        {
            if (experience >= 7 && experience < 19)
            {
                return 1;
            }
            if (experience >= 19 && experience < 43)
            {
                return 2;
            }
            if (experience >= 43 && experience < 62)
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
    }

    public interface ILevelUpRules
    {
        int CountOfSkillsAvailableToUnlock(int experience);
    }
}