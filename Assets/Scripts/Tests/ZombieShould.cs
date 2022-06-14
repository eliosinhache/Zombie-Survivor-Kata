using System.Collections;
using System.Collections.Generic;
using Classes;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class ZombieShould
    {
        private IZombie zombie = new Zombie();
        private ISurvivor survivor = Substitute.For<ISurvivor>();

        [Test]
        public void GiveExperienceToKiller()
        {
            zombie.ReceiveDamage(survivor);
            
            survivor.Received(1).GainExperience(1);
        }

        [Test]
        public void HaveLevel()
        {
            Assert.AreEqual(LevelEnum.Blue, zombie.ReturnLevel());
        }
    }
}
