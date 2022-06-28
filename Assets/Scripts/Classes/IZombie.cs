namespace Classes
{
    public interface IZombie
    {
        void ReceiveDamage(ISurvivor entity);
        void GiveExperienceToKiller(ISurvivor entity);
        LevelEnum RetrieveLevel();
        string RetrieveName();
        void DealDamage(ISurvivor survivor);
        void SetName(string name);

        bool IsAlive();
    }
}