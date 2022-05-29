namespace Classes
{
    public class Zombie : IZombie
    {
        public void ReceiveDamage(ISurvivorMechanics entity)
        {
            entity.ReceiveExperience(1);
        }

    }
}