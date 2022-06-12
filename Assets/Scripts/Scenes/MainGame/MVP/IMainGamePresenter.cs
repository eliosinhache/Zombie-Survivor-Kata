namespace Scenes.MainGame.MVP
{
    public interface IMainGamePresenter
    {
        void StartGame();
        void SetInfoSurvivor(SurvivorView survivorController);
        void SetInfoZombie(ZombieView zombieController);
        void CreateSurvivor(string nmeSurvivor);
    }
}