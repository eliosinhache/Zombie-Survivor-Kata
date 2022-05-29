namespace Classes
{
    public interface IGame
    {
        void AddSurvivor(ISurvivorMechanics survivor);
        void ASurvivorDie(ISurvivorMechanics survivor);
        void ASurvivorLevelUp();
        void ASurvivorEquipatedAWeapon(ISurvivorMechanics survivor, Equipament equipament, string typeOfEquipament);
        void ASurvivorReceiveWound(ISurvivorMechanics survivor);
    }
}