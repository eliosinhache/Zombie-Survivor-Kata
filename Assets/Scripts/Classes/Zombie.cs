namespace Classes
{
    public class Zombie : IZombie
    {
        public void ReceiveDamage(ISurvivor entity)
        {
            GiveExperienceToKiller(entity);
        }

        public void GiveExperienceToKiller(ISurvivor entity)
        {
            entity.GainExperience(1);
        }
    }
}