using System.Collections;
using Classes;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class GameShould
    {
        private Game _game;
        private Survivor _survivor01;
        private Survivor _survivor02;
        
        [SetUp]
        public void Setup()
        {
             _game = new Game();
             _survivor01 = new Survivor("Juan");
             _survivor02 = new Survivor("Pedro");
        }
        
        [Test]
        public void StarWithoutSurvivor()
        {
            
            Assert.AreEqual(0, _game.players.Count);
        }
        
        [Test]
        public void HavePlayers()
        {
            Survivor _survivor01 = new Survivor("Juan");
            _game.AddPlayer(_survivor01);
            
            Assert.Positive(_game.players.Count);
        }
        
        [Test]
        public void DontHaveSurvivorsWithSameNAme()
        {
            _game.AddPlayer(_survivor01);
            _game.AddPlayer(_survivor01);
            
            Assert.AreEqual(1, _game.players.Count);
        }
        
        [Test]
        public void FinishWhenAllSurvivorDie()
        {
            _game.AddPlayer(_survivor01);
            
            _survivor01.ReceiveWound();
            _survivor01.ReceiveWound();
            
            Assert.IsTrue(_game.isFinish);
        }
        [Test]
        public void IsNotFinishWhenOneSurvivorAlive()
        {
            _game.AddPlayer(_survivor01);
            
            _survivor01.ReceiveWound();
            
            Assert.IsFalse(_game.isFinish);
        }

    }
}
