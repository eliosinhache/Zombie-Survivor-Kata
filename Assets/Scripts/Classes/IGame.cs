namespace Classes
{
    public interface IGame
    {
        void AddSurvivor(ISurvivorMechanics survivor);
        void ASurvivorDie(ISurvivorMechanics survivor);
        void ASurvivorLevelUp(ISurvivorMechanics survivor);
        void ASurvivorEquippedAWeapon(ISurvivorMechanics survivor, Equipment equipment, string typeOfEquipment);
        void ASurvivorReceiveWound(ISurvivorMechanics survivor);
    }
}