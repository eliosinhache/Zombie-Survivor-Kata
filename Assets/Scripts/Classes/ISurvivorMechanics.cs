
namespace Classes
{
    public interface ISurvivorMechanics
    {
        void ReceiveExperience(float amount);
        bool CheckIfIsAlive();
        string ReturnName();
        void ReceiveWound();
        float CheckExperience();
        string ReturnLevel();
        void Equipate(Equipment equipment, string typeOfEquipment);
    }
}