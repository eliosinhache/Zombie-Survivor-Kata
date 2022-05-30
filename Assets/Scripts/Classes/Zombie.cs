namespace Classes
{
    public class Zombie : IZombie
    {
        public void ReceiveDamage(ISurvivorMechanics entity)
        {
            GiveExperienceToSurvivor(entity);
        }

        public void GiveExperienceToSurvivor(ISurvivorMechanics entity)
        {
            entity.ReceiveExperience(1);
        }
    }
}