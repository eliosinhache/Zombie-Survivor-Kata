using System.Collections;
using System.Linq;
using Classes;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class GameShould
    {
        private Game _game;
        private ISurvivorMechanics _survivor01;
        private ISurvivorMechanics _survivor02;
        
        [SetUp]
        public void Setup()
        {
             _game = new Game();
             _survivor01 = Substitute.For<ISurvivorMechanics>() ;
             _survivor02 = Substitute.For<ISurvivorMechanics>() ;
             // _survivor01 = new Survivor("Juan", _game);
             // _survivor02 = new Survivor("Pedro", _game);
             // _survivor01.CreateSurvivor("Juan", _game);
             // _survivor02.CreateSurvivor("Pedro", _game);
        }
        
        [Test]
        public void StarWithoutSurvivor()
        {
            
            Assert.AreEqual(0, _game.iPlayers.Count);
        }
        
        [Test]
        public void HavePlayers()
        {
            _game.AddSurvivor(_survivor01);
            
            Assert.Positive(_game.iPlayers.Count);
        }
        
        [Test]
        public void DontHaveSurvivorsWithSameNAme()
        {
            _game.AddSurvivor(_survivor01);
            _game.AddSurvivor(_survivor01);
            
            Assert.AreEqual(1, _game.iPlayers.Count);
        }
        
        [Test]
        public void FinishWhenAllSurvivorDie()
        {
            _survivor01.CheckIfIsAlive().Returns(false);
            _game.AddSurvivor(_survivor01);
            
            _game.ASurvivorDie(_survivor01);
            
            Assert.IsTrue(_game.isFinish);
        }
        [Test]
        public void IsNotFinishWhenOneSurvivorAlive()
        {
            _survivor01.CheckIfIsAlive().Returns(true);
            _game.AddSurvivor(_survivor01);
            
            Assert.IsFalse(_game.isFinish);
        }
        [Test]
        public void StartWithBlueLevel()
        {
            Assert.AreEqual("Blue", _game.level);
        }

        [Test]
        public void BeTheSameLevelAsTheHighestSurvivor()
        {
            _survivor01.ReturnName().Returns("Juan");
            _survivor02.ReturnName().Returns("Pedro");
            _survivor01.CheckExperience().Returns(19);
            _survivor02.CheckExperience().Returns(56);
            _survivor01.ReturnLevel().Returns("Orange");
            _survivor02.ReturnLevel().Returns("Red");
            _game.AddSurvivor(_survivor01);
            _game.AddSurvivor(_survivor02);
            
            _game.ASurvivorLevelUp(_survivor01);
            
            Assert.AreEqual("Red", _game.level);
        }

        [Test]
        public void RecordTheGameStartTime()
        {
            
            Assert.AreEqual("12:35", _game.GameStartTime());
        }

        [Test]
        public void RecordWhenAPlayerJoinTheGame()
        {
            _game.AddSurvivor(_survivor01);
            Assert.IsTrue(_game.history.Contains("New player added: " + _survivor01.ReturnName()));
        }

        [Test]
        public void RecordWhenAPlayerAcquiresAPieceOfEquipament()
        {
            Equipment equipment = new Equipment("SomeWeapon");
            _game.AddSurvivor(_survivor01);
            
            
            _game.ASurvivorEquippedAWeapon(_survivor01, equipment, "In Reserve");
            
            Assert.IsTrue(_game.history.Contains(_survivor01.ReturnName() + " equipped " + equipment.name + " like In Reserve"));
        }
        
        [Test]
        public void RecordWhenAPlayerIsWounded()
        {
            _game.ASurvivorReceiveWound(_survivor01);
            
            Assert.IsTrue(_game.history.Contains(_survivor01.ReturnName() + " receive wound"));
        }
        
        [Test]
        public void RecordWhenASurvivorDie()
        {
            _game.ASurvivorDie(_survivor01);
            
            Assert.IsTrue(_game.history.Contains("The survivor " + _survivor01.ReturnName() + " die"));
        }

        [Test]
        public void RecordWhenASurvivorLevelUp()
        {
            _game.ASurvivorLevelUp(_survivor01);
            
            Assert.IsTrue(_game.history.Contains(_survivor01.ReturnName() + " Level Up!"));
        }

        [Test]
        public void RecordGameLevelChangeWhenASurvivorLevelUp()
        {
            _survivor01.CheckExperience().Returns(19);
            _survivor01.ReturnLevel().Returns("Orange");
            _game.AddSurvivor(_survivor01);
            _game.ASurvivorLevelUp(_survivor01);
            
            Assert.IsTrue(_game.history.Contains("Game level has change"));
        }
        [Test]
        public void RecordGameLevelChangeWhenASurvivorDie()
        {
            _survivor01.CheckExperience().Returns(19);
            _survivor01.ReturnLevel().Returns("Orange");
            _survivor01.CheckIfIsAlive().Returns(true);
            _survivor02.CheckExperience().Returns(29);
            _survivor02.ReturnLevel().Returns("Red");
            _survivor02.CheckIfIsAlive().Returns(false);
            _game.AddSurvivor(_survivor01);
            _game.AddSurvivor(_survivor02);
            
            _game.ASurvivorDie(_survivor02);
            
            Assert.IsTrue(_game.history.Contains("Game level has change"));
        }
        [Test]
        public void RecordGameOverWhenAllSurvivorDie()
        {
            _survivor01.CheckExperience().Returns(19);
            _survivor01.ReturnLevel().Returns("Orange");
            _survivor01.CheckIfIsAlive().Returns(false);
            _survivor02.CheckExperience().Returns(29);
            _survivor02.ReturnLevel().Returns("Red");
            _survivor02.CheckIfIsAlive().Returns(false);
            _game.AddSurvivor(_survivor01);
            _game.AddSurvivor(_survivor02);
            
            _game.ASurvivorDie(_survivor02);
            _game.ASurvivorDie(_survivor01);
            
            Assert.IsTrue(_game.history.Contains("Game Over: all survivor die"));
        }
    }
}
