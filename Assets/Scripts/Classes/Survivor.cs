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
        public List<Equipament> equipament = new List<Equipament>(5);
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
                if (wounds == 2)
                {
                    isAlive = false;
                    SendDeadMessageToGame();
                }
            }
        }


        private void ReduceNumberOfPieces()
        {
            if (equipament.Count == equipament.Capacity) {equipament.RemoveAt(equipament.Capacity -1);}
            equipament.Capacity -= 1;
        }

        public void Equipate(Equipament equipament, string typeOfEquipament)
        {
            if (CanNotEquipateNewEquipament(typeOfEquipament) ){ return; }

            equipament.equiped = typeOfEquipament;
            this.equipament.Add(equipament);
        }

        private bool CanNotEquipateNewEquipament(string typeOfEquipament)
        {
            return equipament.Count == equipament.Capacity || (typeOfEquipament == "In Hand" && EquipamentInHand() == 2);
        }


        public int EquipamentInHand()
        {
            int inHand = 0;
            foreach (Equipament item in equipament)
            {
                if (item.equiped == "In Hand")
                {
                    inHand += 1;
                }
            }

            return inHand;
        }

        public void DealDamage(Zombie zombie)
        {
            zombie.ReceiveDamage(this);
        }

        public void CreateSurvivor(string name)
        {
            name = name;
            isAlive = true;
            actions = 3;
            wounds = 0;
        }

        public void CreateSurvivor(string name, IGame iGame)
        {
            gameManager = iGame;
            this.name = name;
            isAlive = true;
            actions = 3;
            wounds = 0;
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

        public void SendDeadMessageToGame()
        {
            gameManager.ASurvivorDie();
        }

        public void SendLevelUpToGame()
        {
            gameManager.ASurvivorLevelUp();
        }

        public float CheckExperience()
        {
            throw new NotImplementedException();
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
                    SendLevelUpToGame();
                    break;
                case 18:
                        level = "Orange";
                        SendLevelUpToGame();
                        break;
                case 42:
                        level = "Red";
                        SendLevelUpToGame();
                        break;
            }
        }
    }
}