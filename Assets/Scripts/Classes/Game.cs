using System;
using System.Collections.Generic;
using NSubstitute;
using UniRx;

namespace Classes
{
    public class Game : IGame
    {
        public List<ISurvivorMechanics> iPlayers = new List<ISurvivorMechanics>();
        public bool isFinish;
        public string level = "Blue";
        public List<string> history = new List<string>();


        private bool IsNameExist(ISurvivorMechanics player)
        {
            foreach (ISurvivorMechanics item in iPlayers)
            {
                if (item.ReturnName() == player.ReturnName())
                {
                    return true;
                }
            }
            return false;
        }

        public void AddSurvivor(ISurvivorMechanics survivor)
        {
            if (IsNameExist(survivor)) return;
            iPlayers.Add(survivor);
            AddNewPlayerHistory(survivor);
        }

        private void AddNewPlayerHistory(ISurvivorMechanics survivor)
        {
            RecordHistory("New player added: " + survivor.ReturnName());
        }

        public void ASurvivorDie(ISurvivorMechanics survivor)
        {
            RecordHistory("The survivor " + survivor.ReturnName() + " die");
            CheckIfAllSurvivorDie();
        }

        private void CheckIfAllSurvivorDie()
        {
            var alives = 0;
            foreach (ISurvivorMechanics survivor in iPlayers)
            {
                if (!survivor.CheckIfIsAlive()) continue;
                if (level != survivor.ReturnLevel())
                {
                    RecordGameLevelChanged();
                }
                alives += 1;
            }

            if (alives == 0)
            {
                isFinish = true;
                RecordHistory("Game Over: all survivor die");
            }
        }

        public void ASurvivorLevelUp(ISurvivorMechanics survivor)
        {
            RecordHistory(survivor.ReturnName() + " Level Up!");
            CheckMaxLevel();
        }

        private void CheckMaxLevel()
        {
            float maxExp = 0;
            foreach (ISurvivorMechanics survivor in iPlayers)
            {
                if (survivor.CheckExperience() <= maxExp) continue;
                maxExp = survivor.CheckExperience();
                if (level != survivor.ReturnLevel())
                {
                    level = survivor.ReturnLevel();
                    RecordGameLevelChanged();
                }
            }
        }

        private void RecordGameLevelChanged()
        {
            history.Add("Game level has change");
        }

        public void ASurvivorEquippedAWeapon(ISurvivorMechanics survivor, Equipment equipment, string typeOfEquipment)
        {
            AddPlayerEquipatedHistory(survivor, equipment, typeOfEquipment);
        }

        private void RecordHistory(string message)
        {
            history.Add(message);
        }

        public void ASurvivorReceiveWound(ISurvivorMechanics survivor)
        {
            RecordHistory(survivor.ReturnName() + " receive wound");
        }

        private void AddPlayerEquipatedHistory(ISurvivorMechanics survivor, Equipment equipment, string typeOfEquipment)
        {
            RecordHistory(survivor.ReturnName() + " equipped " + equipment.name + " like " + typeOfEquipment);
        }


        public string GameStartTime()
        {
            return "12:35";
        }

    }
}