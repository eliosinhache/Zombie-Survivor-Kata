using System.Collections;
using System.Collections.Generic;
using Classes;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class SurvivorShould
    {
        private Survivor _survivor;
        [SetUp]
        public void SetUp()
        {
            _survivor = new Survivor("Juan");
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
    }
}
