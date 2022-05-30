using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Classes
{
    public class Survivor : ISurvivorMechanics
    {
        private IGame gameManager;
        public string name;
        public int wounds;
        public bool isAlive;
        public int actions;
        public List<Equipment> equipament = new List<Equipment>(5);
        public float experience;
        public string level = "Blue";

        public Survivor(string survivorName, IGame iGameManager)
        {
            gameManager = iGameManager;
            name = survivorName;
            isAlive = true;
            actions = 3;
            wounds = 0;
        }

        public void ReceiveWound()
        {
            if (isAlive)
            { 
                wounds += 1;
                ReduceNumberOfPieces();
                gameManager.ASurvivorReceiveWound(this);
                if (wounds == 2)
                {
                    isAlive = false;
                    gameManager.ASurvivorDie(this);
                }
            }
        }


        private void ReduceNumberOfPieces()
        {
            if (equipament.Count == equipament.Capacity) {equipament.RemoveAt(equipament.Capacity -1);}
            equipament.Capacity -= 1;
        }

        public void Equipate(Equipment equipment, string typeOfEquipment)
        {
            if (CanNotEquipateNewEquipment(typeOfEquipment) ){ return; }

            equipment.equiped = typeOfEquipment;
            this.equipament.Add(equipment);

            gameManager.ASurvivorEquippedAWeapon(this, equipment, typeOfEquipment);
        }

        private bool CanNotEquipateNewEquipment(string typeOfEquipment)
        {
            return equipament.Count == equipament.Capacity || (typeOfEquipment == "In Hand" && EquipmentInHand() == 2);
        }


        public int EquipmentInHand()
        {
            int inHand = 0;
            foreach (Equipment item in equipament)
            {
                if (item.equiped == "In Hand")
                {
                    inHand += 1;
                }
            }

            return inHand;
        }

        public void DealDamage(IZombie zombie)
        {
            zombie.ReceiveDamage(this);
        }

        public void ReceiveExperience(float amount)
        {
            experience += amount;
            CheckLevel();
        }

        public bool CheckIfIsAlive()
        {
            return isAlive;
        }

        public string ReturnName()
        {
            return name;
        }

        public float CheckExperience()
        {
            return experience;
        }

        public string ReturnLevel()
        {
            return level;
        }

        private void CheckLevel()
        {
            switch (experience)
            {
                case 6:
                    level = "Yellow";
                    gameManager.ASurvivorLevelUp(this);
                    break;
                case 18:
                        level = "Orange";
                        gameManager.ASurvivorLevelUp(this);
                        break;
                case 42:
                        level = "Red";
                        gameManager.ASurvivorLevelUp(this);
                        break;
            }
        }
    }
}