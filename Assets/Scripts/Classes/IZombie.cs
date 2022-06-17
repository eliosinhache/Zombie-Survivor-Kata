namespace Classes
{
    public interface IZombie
    {
        void ReceiveDamage(ISurvivor entity);
        void GiveExperienceToKiller(ISurvivor entity);
        LevelEnum ReturnLevel();
        string ReturnName();
    }
}