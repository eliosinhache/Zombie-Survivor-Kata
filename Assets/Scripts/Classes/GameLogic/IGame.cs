using System.Collections.Generic;
using Classes.Character;

namespace Classes.GameLogic
{
    public interface IGame
    {
        void AddSurvivor(ISurvivor survivor);
        void ASurvivorDie(ISurvivor survivor);
        void ASurvivorLevelUp(ISurvivor survivor);
        void ASurvivorEquippedAWeapon(ISurvivor survivor, Equipment equipment, string typeOfEquipment);
        void ASurvivorReceiveWound(ISurvivor survivor);
        List<ISurvivor> RetrieveAllSurvivors();
        void AddZombie(IZombie zombie);
        List<IZombie> RetrieveAllZombies();
        List<string> RetrieveCompleteHistory();
    }
}