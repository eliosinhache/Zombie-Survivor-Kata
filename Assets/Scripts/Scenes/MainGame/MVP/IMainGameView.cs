using System.Collections.Generic;

namespace Scenes.MainGame.MVP
{
    public interface IMainGameView
    {
        List<SurvivorView> ReturnSurvivorViews();
        List<ZombieView> ReturnZombieViews();
        void AddSurvivor();
    }
}