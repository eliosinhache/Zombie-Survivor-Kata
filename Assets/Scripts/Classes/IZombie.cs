namespace Classes
{
    public interface IZombie
    {
        void ReceiveDamage(ISurvivorMechanics entity);
        void GiveExperienceToSurvivor(ISurvivorMechanics entity);
    }
}