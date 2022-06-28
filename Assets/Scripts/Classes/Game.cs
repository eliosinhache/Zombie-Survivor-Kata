using System;
using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using UniRx;

namespace Classes
{
    public class Game : IGame
    {
        private readonly List<ISurvivor> _survivors = new List<ISurvivor>();
        private readonly List<IZombie> _zombies = new List<IZombie>();
        public readonly List<string> history = new List<string>();
        public bool isFinish;
        public LevelEnum level = LevelEnum.Blue;


        private bool IsNameExist(ISurvivor player)
        {
            return _survivors.Any(item => item.ReturnName() == player.ReturnName());
        }

        public void AddSurvivor(ISurvivor survivor)
        {
            if (IsNameExist(survivor)) return;
            _survivors.Add(survivor);
            AddNewPlayerHistory(survivor);
            
        }

        private void AddNewPlayerHistory(ISurvivor survivor)
        {
            RecordHistory("New player added: " + survivor.ReturnName());
        }

        public void ASurvivorDie(ISurvivor survivor)
        {
            RecordHistory("The survivor " + survivor.ReturnName() + " die");
            CheckIfAllSurvivorDie();
        }

        private void CheckIfAllSurvivorDie()
        {
            var alive = 0;
            foreach (var survivor in _survivors.Where(survivor => survivor.CheckIfIsAlive()))
            {
                if (level != survivor.ReturnLevel())
                {
                    RecordGameLevelChanged();
                }
                alive += 1;
            }

            if (alive != 0) return;
            isFinish = true;
            RecordHistory("Game Over: all survivor die");
        }

        public void ASurvivorLevelUp(ISurvivor survivor)
        {
            RecordHistory(survivor.ReturnName() + " Level Up!");
            CheckMaxLevel();
        }

        private void CheckMaxLevel()
        {
            float maxExp = 0;
            foreach (var survivor in _survivors.Where(survivor => !(survivor.CheckExperience() <= maxExp)))
            {
                maxExp = survivor.CheckExperience();
                if (level == survivor.ReturnLevel()) continue;
                level = survivor.ReturnLevel();
                RecordGameLevelChanged();
            }
        }

        private void RecordGameLevelChanged()
        {
            history.Add("Game level has change");
        }

        public void ASurvivorEquippedAWeapon(ISurvivor survivor, Equipment equipment, string typeOfEquipment)
        {
            AddPlayerEquipatedHistory(survivor, equipment, typeOfEquipment);
        }

        private void RecordHistory(string message)
        {
            history.Add(message);
        }

        public void ASurvivorReceiveWound(ISurvivor survivor)
        {
            RecordHistory(survivor.ReturnName() + " received wound");
        }

        private void AddPlayerEquipatedHistory(ISurvivor survivor, Equipment equipment, string typeOfEquipment)
        {
            RecordHistory(survivor.ReturnName() + " equipped " + equipment.name + " like " + typeOfEquipment);
        }


        public string GameStartTime()
        {
            return "12:35";
        }

        public List<ISurvivor> RetrieveAllSurvivors()
        {
            return _survivors;
        }

        public List<IZombie> RetrieveAllZombies()
        {
            return _zombies;
        }

        public List<string> RetrieveCompleteHistory()
        {
            return history;
        }

        public void AddZombie(IZombie zombie)
        {
            _zombies.Add(zombie);
        }

        public List<ISurvivor> ListOfSurvivors()
        {
            return _survivors;
        }
    }
}