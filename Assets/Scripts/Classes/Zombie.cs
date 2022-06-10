namespace Classes
{
    public class Zombie : IZombie
    {
        private string level = "Blue";
        public void ReceiveDamage(ISurvivor entity)
        {
            GiveExperienceToKiller(entity);
        }

        public void GiveExperienceToKiller(ISurvivor entity)
        {
            entity.GainExperience(1);
        }

        public string ReturnLevel()
        {
            return level;
        }
    }
}