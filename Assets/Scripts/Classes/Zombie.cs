namespace Classes
{
    public class Zombie : IZombie
    {
        private LevelEnum level = LevelEnum.Blue;
        public void ReceiveDamage(ISurvivor entity)
        {
            GiveExperienceToKiller(entity);
        }

        public void GiveExperienceToKiller(ISurvivor entity)
        {
            entity.GainExperience(1);
        }

        public LevelEnum ReturnLevel()
        {
            return level;
        }
    }
}