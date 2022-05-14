namespace Classes
{
    public class Zombie
    {
        public void ReceiveDamage(ISurvivorMechanics entity)
        {
            entity.ReceiveExperience(1);
        }

    }
}