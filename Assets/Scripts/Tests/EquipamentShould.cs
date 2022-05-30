using System.Collections;
using System.Collections.Generic;
using Classes;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class EquipamentShould
    {
        [Test]
        public void HaveName()
        {
            Equipment equipment = new Equipment("Baseball bat");
            
            Assert.AreEqual("Baseball bat", equipment.name);
        }
        
    }
}
