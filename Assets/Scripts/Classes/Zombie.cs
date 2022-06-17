namespace Classes
{
    public class Zombie : IZombie
    {
        private LevelEnum level = LevelEnum.Blue;
        private string name = "zombieDefault";
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

        public string ReturnName()
        {
            return name;
        }
    }
}