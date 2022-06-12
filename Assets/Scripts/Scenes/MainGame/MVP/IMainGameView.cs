using System.Collections.Generic;

namespace Scenes.MainGame.MVP
{
    public interface IMainGameView
    {
        List<SurvivorCharacterView> ReturnSurvivorViews();
        List<ZombieCharacterView> ReturnZombieViews();
        void AddSurvivor();
        void AddZombie();
    }
}