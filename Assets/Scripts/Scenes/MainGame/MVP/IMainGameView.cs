using System.Collections.Generic;
using Classes;

namespace Scenes.MainGame.MVP
{
    public interface IMainGameView
    {
        List<SurvivorCharacterView> ReturnSurvivorViews();
        List<ZombieCharacterView> ReturnZombieViews();
        void AddSurvivor(string name);
        void AddZombie(string zombieName);
        void FillSelectedSurvivor(string returnName, string returnLevel);
        void AddSurvivorGUI(string returnName);
        void AddZombieGUI(string name);
        void FillSelectedZombie(string characterNameValue, string toString);
        void WriteLog(string log);
        void ShowHistoryOnDebug(List<string> histoyry);
        // void SuccessfullyEquippedInReserve(string equipment);
        // void SuccessfullyEquippedInHand(string equipment);
        EquipmentView[] ReturnEquipmentViewList();
    }
}