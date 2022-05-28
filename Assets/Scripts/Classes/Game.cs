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
        }

        public void ASurvivorDie()
        {
            var alives = 0;
            foreach (ISurvivorMechanics survivor in iPlayers)
            {
                if (survivor.CheckIfIsAlive()) {
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
            foreach (var survivor in iPlayers)
            {
                if (survivor.CheckExperience() <= maxExp) continue;
                maxExp = survivor.CheckExperience();
                level = survivor.ReturnLevel();
            }
        }
    }
}