namespace Classes
{
    public interface IGame
    {
        void AddSurvivor(ISurvivor survivor);
        void ASurvivorDie(ISurvivor survivor);
        void ASurvivorLevelUp(ISurvivor survivor);
        void ASurvivorEquippedAWeapon(ISurvivor survivor, Equipment equipment, string typeOfEquipment);
        void ASurvivorReceiveWound(ISurvivor survivor);
    }
}