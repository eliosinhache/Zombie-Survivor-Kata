using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Classes
{
    public class Survivor : ISurvivor
    {
        private IGame _game;
        private ISkillTree _skillTree;
        private ILevelUpRules _levelUpRules = new LevelUpRules();
        public string name;
        public int wounds;
        public bool isAlive;
        public int actions;
        public List<Equipment> equipment = new List<Equipment>(5);
        public float experience;
        public LevelEnum level = LevelEnum.Blue;

        public Survivor(string survivorName, IGame Game, ISkillTree skillTree)
        {
            _game = Game;
            _skillTree = skillTree;
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
                _game.ASurvivorReceiveWound(this);
                if (wounds == 2)
                {
                    isAlive = false;
                    _game.ASurvivorDie(this);
                }
            }
        }


        private void ReduceNumberOfPieces()
        {
            if (equipment.Count == equipment.Capacity) {equipment.RemoveAt(equipment.Capacity -1);}
            equipment.Capacity -= 1;
        }

        public void Equip(Equipment equipment, string typeOfEquipment)
        {
            if (CanNotEquipateNewEquipment(typeOfEquipment) ){ return; }

            equipment.equiped = typeOfEquipment;
            this.equipment.Add(equipment);

            _game.ASurvivorEquippedAWeapon(this, equipment, typeOfEquipment);
        }

        private bool CanNotEquipateNewEquipment(string typeOfEquipment)
        {
            return equipment.Count == equipment.Capacity || (typeOfEquipment == "In Hand" && EquipmentInHand() == 2);
        }


        public int EquipmentInHand()
        {
            int inHand = 0;
            foreach (Equipment item in equipment)
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

        public int RetrieveLifes()
        {
            return 2 - wounds;
        }

        public List<Equipment> ReturnAllEquipment()
        {
            return equipment;
        }

        public void GainExperience(float amount)
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

        public LevelEnum ReturnLevel()
        {
            return level;
        }

        private void CheckLevel()
        {
            if (!_levelUpRules.CanLevelUp(Mathf.RoundToInt(experience), level)) return;
            level = _levelUpRules.LevelUp(level);
            _game.ASurvivorLevelUp(this);
        }

        public int UnlockedSkills()
        {
            return _skillTree.NumberUnlockedSkills();
        }
    }
}