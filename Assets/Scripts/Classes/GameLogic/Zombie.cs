using Classes.Level;

namespace Classes.Character
{
    public class Zombie : IZombie
    {
        private LevelEnum level = LevelEnum.Blue;
        private string name;
        private bool isAlive=true;
        public void ReceiveDamage(ISurvivor entity)
        {
            if (!isAlive) return;
            isAlive = false;
            GiveExperienceToKiller(entity);
        }

        public void GiveExperienceToKiller(ISurvivor entity)
        {
            entity.GainExperience(1);
        }

        public LevelEnum RetrieveLevel()
        {
            return level;
        }

        public string RetrieveName()
        {
            return name;
        }

        public void DealDamage(ISurvivor survivor)
        {
            survivor.ReceiveWound();
        }

        public void SetName(string zombieName)
        {
            name = zombieName;
        }

        public bool IsAlive()
        {
            return isAlive;
        }
    }
}