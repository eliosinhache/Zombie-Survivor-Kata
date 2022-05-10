using System.Collections;
using System.Collections.Generic;
using Classes;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.XR.WSA;

namespace Tests
{
    public class SurvivorShould
    {
        private Survivor _survivor;
        private Equipament _equipamentBaseballBat;
        private Equipament _equipamentKatana;
        [SetUp]
        public void SetUp()
        {
            _survivor = new Survivor("Juan");
            _equipamentBaseballBat = new Equipament("Baseball bat");
            _equipamentKatana = new Equipament("Katana");
        }
        
        [Test]
        public void HasAName()
        {
            Assert.AreEqual("Juan", _survivor.name);
        }
        
        [Test]
        public void StartWithNoWounds()
        {
            Assert.AreEqual(0, _survivor.wounds);
        }
        
        [Test]
        public void DieWithTwoWounds()
        {
            _survivor.ReceiveWound();
            _survivor.ReceiveWound();
            
            Assert.IsFalse(_survivor.isAlive);
        }
        
        [Test]
        public void HaveThreeActionsPerTurn()
        {
            Assert.AreEqual(3, _survivor.actions);
        }
        
        [Test]
        public void HaveTwoEquipamentInHand()
        {
            _survivor.Equipate(_equipamentBaseballBat, "In Reserve");
            _survivor.Equipate(_equipamentKatana, "In Hand");
            _survivor.Equipate(_equipamentKatana, "In Hand");
            _survivor.Equipate(_equipamentKatana, "In Hand");
            
            
            Assert.AreEqual(2, _survivor.EquipamentInHand());
        }
        
        [Test]
        public void Have5EquipamentInReserve()
        {
            _survivor.Equipate(_equipamentBaseballBat, "In Reserve");
            _survivor.Equipate(_equipamentKatana, "In Reserve");
            _survivor.Equipate(_equipamentKatana, "In Hand");
            _survivor.Equipate(_equipamentKatana, "In Reserve");
            _survivor.Equipate(_equipamentKatana, "In Hand");
            _survivor.Equipate(_equipamentKatana, "In Reserve");
            _survivor.Equipate(_equipamentKatana, "In Hand");
            
            Assert.AreEqual(5, _survivor.equipament.Count);
        }
        
        [Test]
        public void HaveOneLessPiecesWhenReceiveWound()
        {
            _survivor.Equipate(_equipamentBaseballBat, "In Reserve");
            _survivor.Equipate(_equipamentKatana, "In Reserve");
            _survivor.Equipate(_equipamentKatana, "In Hand");
            _survivor.Equipate(_equipamentKatana, "In Reserve");
            _survivor.Equipate(_equipamentKatana, "In Reserve");
            
            _survivor.ReceiveWound();
            
            _survivor.Equipate(_equipamentKatana, "In Reserve");
            
            Assert.AreEqual(4, _survivor.equipament.Count);
        }
        
        
    }
}
