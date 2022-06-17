using System.Collections.Generic;
using Classes;

namespace Scenes.MainGame.MVP
{
    public interface IMainGameView
    {
        List<SurvivorCharacterView> ReturnSurvivorViews();
        List<ZombieCharacterView> ReturnZombieViews();
        void AddSurvivor(string name);
        void AddZombie();
        void FillSelectedSurvivor(string returnName, string returnLevel);
        void AddSurvivorGUI(string returnName);
    }
}