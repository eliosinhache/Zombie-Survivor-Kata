namespace Scenes.MainGame.MVP
{
    public interface IView
    {
        void SetSurvivorLevel(string level);
        void SetSurvivorExperience(float checkExperience);
        void SetZombieLevel(string returnLevel);
        void SetSurvivorLife(int lifes);
        void SetZombieLife(int i);
    }
}