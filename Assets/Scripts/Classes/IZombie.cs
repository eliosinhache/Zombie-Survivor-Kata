namespace Classes
{
    public interface IZombie
    {
        void ReceiveDamage(ISurvivor entity);
        void GiveExperienceToKiller(ISurvivor entity);
        LevelEnum ReturnLevel();
        string ReturnName();
        void DealDamage(ISurvivor survivor);
        void SetName(string name);

        bool IsAlive();
    }
}