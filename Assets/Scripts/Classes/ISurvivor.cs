
namespace Classes
{
    public interface ISurvivor
    {
        void GainExperience(float amount);
        bool CheckIfIsAlive();
        string ReturnName();
        void ReceiveWound();
        float CheckExperience();
        LevelEnum ReturnLevel();
        void Equipate(Equipment equipment, string typeOfEquipment);
    }
}