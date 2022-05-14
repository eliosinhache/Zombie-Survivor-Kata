using System;
using System.Collections.Generic;
using UniRx;

namespace Classes
{
    public class Game
    {
        public List<Survivor> players = new List<Survivor>();
        public bool isFinish = false;
        public ReactiveProperty<bool> playerRX = new ReactiveProperty<bool>();
        public List<ReactiveProperty<bool>> survivorsList = new List<ReactiveProperty<bool>>();
        public string level = "Blue";

        private void VerifySurvivors()
        {
            var deadSurvivors = 0;
            // foreach (ReactiveProperty<Survivor> survivor in survivorsList)
            // {
            //     if (!survivor.Value.isAlive)
            //     {
            //         deadSurvivors += 1;
            //     }
            // }
            foreach (Survivor survivor in players)
            {
                if (!survivor.isAlive)
                {
                    deadSurvivors += 1;
                }
            }

            if (deadSurvivors == survivorsList.Count)
            {
                isFinish = true;
            }
        }

        public void AddPlayer(Survivor player)
        {
            if (IsNameExist(player)) return;
            players.Add(player);
            playerRX.Value = player.isAlive;
            survivorsList.Add(playerRX);
            playerRX.Subscribe(x =>VerifySurvivors()) ;
            // InitiateReactivity();
            // playerRX.Subscribe(survivor => VerifySurvivors(survivor));
        }

        // private void InitiateReactivity()
        // {
        //     foreach (ReactiveProperty<Survivor> item in survivorsList)
        //     {
        //         item.Subscribe(player => VerifySurvivors());
        //     }
        // }

        private bool IsNameExist(Survivor player)
        {
            foreach (Survivor item in players)
            {
                if (item.name == player.name)
                {
                    return true;
                }
            }
            return false;
        }
    }
}