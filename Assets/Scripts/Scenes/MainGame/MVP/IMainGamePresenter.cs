using System;
using Classes;

namespace Scenes.MainGame.MVP
{
    public interface IMainGamePresenter
    {
        void StartGame();
        void SetInfoSurvivor(SurvivorCharacterView survivorCharacterController);
        void SetInfoZombie(ZombieCharacterView zombieCharacterController);
        void CreateSurvivor(string nmeSurvivor);
        void CreateZombie();
        void ASurvivorWasSelected(String survivor);
        void AZombieWasSelected(string zombieName);
        void SurvivorAttackAZombie();
        void SubscribeNewZombieToData(ZombieCharacterView getComponent);
        void SubscribeNewSurvivorToData(SurvivorCharacterView getComponent);
        void ZombieAttackASurvivor();
        void ReadHistory();
        void EquipateInReserve(string equipment);
        void EquipateInHand(string equipment);
    }
}