
namespace Classes
{
    public interface ISurvivorMechanics
    {
        void CreateSurvivor(string name, IGame iGame);
        void ReceiveExperience(float amount);
        bool CheckIfIsAlive();
        string ReturnName();
        void SendDeadMessageToGame();
        void SendLevelUpToGame();
        float CheckExperience();
        string ReturnLevel();
    }
}