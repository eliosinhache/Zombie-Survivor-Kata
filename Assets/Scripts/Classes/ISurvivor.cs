﻿
using System.Collections.Generic;

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
        void Equip(Equipment equipment, string typeOfEquipment);
        void DealDamage(IZombie zombie);
        int RetrieveLifes();
        List<Equipment> ReturnAllEquipment();
    }
}