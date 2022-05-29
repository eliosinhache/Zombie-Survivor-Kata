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
            history.Add("New player added: " + survivor.ReturnName());
        }

        public void ASurvivorDie(ISurvivorMechanics survivor)
        {
            RecordASurvivorDie(survivor);
            CheckIfAllSurvivorDie();
        }

        private void RecordASurvivorDie(ISurvivorMechanics survivor)
        {
            history.Add("The survivor " + survivor.ReturnName() + " die");
        }

        private void CheckIfAllSurvivorDie()
        {
            var alives = 0;
            foreach (ISurvivorMechanics survivor in iPlayers)
            {
                if (survivor.CheckIfIsAlive())
                {
                    alives += 1;
                }
            }

            if (alives == 0)
            {
                isFinish = true;
            }
        }

        public void ASurvivorLevelUp()
        {
            float maxExp = 0;
            foreach (ISurvivorMechanics survivor in iPlayers)
            {
                if (survivor.CheckExperience() <= maxExp) continue;
                maxExp = survivor.CheckExperience();
                level = survivor.ReturnLevel();
            }
        }

        public void ASurvivorEquipatedAWeapon(ISurvivorMechanics survivor, Equipament equipament, string typeOfEquipament)
        {
            AddPlayerEquipatedHistory(survivor, equipament, typeOfEquipament);
        }

        public void ASurvivorReceiveWound(ISurvivorMechanics survivor)
        {
            history.Add(survivor.ReturnName() + " receive wound");
        }

        private void AddPlayerEquipatedHistory(ISurvivorMechanics survivor, Equipament equipament, string typeOfEquipament)
        {
            history.Add(survivor.ReturnName() + " equipped " + equipament.name + " like " + typeOfEquipament);
        }


        public string GameStartTime()
        {
            return "12:35";
        }

    }
}