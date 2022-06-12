namespace Scenes.MainGame.MVP
{
    public interface IMainGamePresenter
    {
        void StartGame();
        void SetInfoSurvivor(SurvivorCharacterView survivorCharacterController);
        void SetInfoZombie(ZombieCharacterView zombieCharacterController);
        void CreateSurvivor(string nmeSurvivor);
    }
}