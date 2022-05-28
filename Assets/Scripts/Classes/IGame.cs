namespace Classes
{
    public interface IGame
    {
        void AddSurvivor(ISurvivorMechanics survivor);
        void ASurvivorDie();
        void ASurvivorLevelUp();
    }
}