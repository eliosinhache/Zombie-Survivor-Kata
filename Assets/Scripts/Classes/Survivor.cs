using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Classes
{
    public class Survivor : ISurvivorMechanics
    {
        public string name;
        public int wounds;
        public bool isAlive;
        public int actions;
        public List<Equipament> equipament = new List<Equipament>(5);
        public float experience;
        public string level = "Blue";

        public Survivor(string survivorName)
        {
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
            if (this.equipament.Count == this.equipament.Capacity || (typeOfEquipament == "In Hand" && EquipamentInHand() == 2) ){ return; }

            equipament.equiped = typeOfEquipament;
            this.equipament.Add(equipament);
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

        public void ReceiveExperience(float amount)
        {
            experience += amount;
            if (experience == 6)
            {
                level = "Yellow";
            }

            if (experience == 18)
            {
                level = "Orange";
            }
            if (experience == 42)
            {
                level = "Red";
            }
        }
    }
}