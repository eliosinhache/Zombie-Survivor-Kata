using System;
using System.Collections.Generic;
using System.Linq;

namespace Classes
{
    public class SkillTree : ISkillTree
    {
        class Nodo
        {
            public Skill skill;
            public Nodo next;
        }

        Nodo raiz;
        private Nodo _explorer;
        private List<Nodo> _levelYellowSkills = new List<Nodo>();
        private List<Nodo> _levelOrangeSkills = new List<Nodo>();
        private List<Nodo> _levelRedSkills = new List<Nodo>();

        public SkillTree()
        {
            raiz = null;
            Skill skill = new Skill();
            skill.description = "+1 Action";
            skill.lvlToUnlock = LevelEnum.Yellow;
            skill.minExperienceNeeded = 7;
            AddNewSkill(skill, LevelEnum.Yellow);
            
            Skill skill2 = new Skill();
            skill2.description = "+1 Die";
            skill2.lvlToUnlock = LevelEnum.Orange;
            skill2.minExperienceNeeded = 18;
            AddNewSkill(skill2, LevelEnum.Orange);
            
            Skill skill3 = new Skill();
            skill3.description = "+1 Free Move Action";
            skill3.lvlToUnlock = LevelEnum.Orange;
            skill3.minExperienceNeeded = 18;
            AddNewSkill(skill3, LevelEnum.Orange);

            Skill skill4 = new Skill();
            skill4.description = "Hoard";
            skill4.lvlToUnlock = LevelEnum.Red;
            skill4.minExperienceNeeded = 42;
            AddNewSkill(skill4, LevelEnum.Red);
            
            Skill skill5 = new Skill();
            skill5.description = "Tough";
            skill5.lvlToUnlock = LevelEnum.Red;
            skill5.minExperienceNeeded = 42;
            AddNewSkill(skill5, LevelEnum.Red);
            
            Skill skill6 = new Skill();
            skill6.description = "Sniper";
            skill6.lvlToUnlock = LevelEnum.Red;
            skill6.minExperienceNeeded = 42;
            AddNewSkill(skill6, LevelEnum.Red);

            _explorer = null;
        }

        public void AddNewSkill (Skill skill, LevelEnum level)
        {
            Nodo newSkill;
            newSkill = new Nodo ();
            newSkill.skill = skill;
            newSkill.next = null;
            switch (level)
            {
                case LevelEnum.Yellow:
                    _levelYellowSkills.Add(newSkill);
                    break;
                case LevelEnum.Orange:
                    if (_levelOrangeSkills.Count == 0) { PointToFirstNodo(newSkill, _levelYellowSkills); } 
                    _levelOrangeSkills.Add(newSkill);
                    break;
                case LevelEnum.Red:
                    if (_levelRedSkills.Count == 0) { PointToFirstNodo(newSkill, _levelOrangeSkills); } 
                    _levelRedSkills.Add(newSkill);
                    break;
            }
        }

        private void PointToFirstNodo(Nodo newSkill, List<Nodo> previousLevelSkills)
        {
            foreach (Nodo previousSkill in previousLevelSkills)
            {
                previousSkill.next = newSkill;
            }
        }

        public int UnlockedSkills ()
        {
            int count = 0;
            count += _levelYellowSkills.Count(item => item.skill.isUnlock);
            count += _levelOrangeSkills.Count(item => item.skill.isUnlock);
            count += _levelRedSkills.Count(item => item.skill.isUnlock);

            // count = UnlockedSkillsRecursive (raiz, count);
            // Console.WriteLine();
            return count;
        }

        // public int UnlockedSkills()
        // {
        //     int count = 0;
        //     count = UnlockedSkills(count);
        //     return count;
        // }

        public int EnabledSkills(LevelEnum level)
        {
            return SearchCountOfSkillsPerLevel(level);
        }

        public int LockedSkills()
        {
            int count = CountOfLockedSkills();
            return count;
        }

        public void UnlockSkill(Skill unlockSkill, int experience)
        {
            if (unlockSkill.minExperienceNeeded > experience) { return;}
            if (IsExperienceEnoughToUnlockNewSkill(experience)) { UnlockSkill(unlockSkill);}
        }

        public List<Skill> AvaibleSkillsToUnlock(LevelEnum level)
        {
            List<Skill> availableSkillsToUnlock = new List<Skill>();
            switch (level)
            {
                case LevelEnum.Yellow:
                    foreach (Nodo nodo in _levelYellowSkills)
                    {
                        if (!nodo.skill.isUnlock) {availableSkillsToUnlock.Add(nodo.skill);}
                    }
                    break;
                case LevelEnum.Orange:
                    foreach (Nodo nodo in _levelOrangeSkills)
                    {
                        if (!nodo.skill.isUnlock) { availableSkillsToUnlock.Add(nodo.skill);}
                    }
                    break;
                case LevelEnum.Red:
                    foreach (Nodo nodo in _levelRedSkills)
                    {
                        if (!nodo.skill.isUnlock) {availableSkillsToUnlock.Add(nodo.skill);}
                    }
                    break;
            }

            return availableSkillsToUnlock;
        }

        private bool IsExperienceEnoughToUnlockNewSkill(int experience)
        {
            if (experience >= 7 && experience < 18)
            {
                return UnlockedSkills() < 1;
            }
            if (experience >= 18 && experience < 42)
            {
                return UnlockedSkills() < 2;
            }
            if (experience >= 42 && experience < 61) {return UnlockedSkills() < 3;}

            if (experience >= 61 && experience < 86)
            {
                return UnlockedSkills() < 4;
            }
            if (experience >= 86 && experience < 129)
            {
                return UnlockedSkills() < 5;
            }

            return UnlockedSkills() < 6;
        }

        private void UnlockSkill(Skill unlockSkill)
        {
            switch (unlockSkill.lvlToUnlock)
            {
                case LevelEnum.Yellow:
                    SearchAndUnlockSkill(unlockSkill, _levelYellowSkills);
                    break;
                case LevelEnum.Orange:
                    SearchAndUnlockSkill(unlockSkill, _levelOrangeSkills);
                    break;
                case LevelEnum.Red:
                    SearchAndUnlockSkill(unlockSkill, _levelRedSkills);
                    break;
            }
        }

        private void SearchAndUnlockSkill(Skill unlockSkill, List<Nodo> levelSkills)
        {
            foreach (Nodo nodo in levelSkills)
            {
                if (nodo.skill.description == unlockSkill.description)
                {
                    nodo.skill.isUnlock = true;
                }
            }
        }

        private int CountOfLockedSkills()
        {
            int count = 0;
            count += _levelYellowSkills.Count(item => !item.skill.isUnlock);
            count += _levelOrangeSkills.Count(item => !item.skill.isUnlock);
            count += _levelRedSkills.Count(item => !item.skill.isUnlock);
            return count;
        }

        private int SearchCountOfSkillsPerLevel(LevelEnum level)
        {
            int count = 0;
            switch (level)
            {
                case LevelEnum.Blue:
                    count = 0;
                    break;
                case LevelEnum.Yellow:
                    count += _levelYellowSkills.Count(item => item.skill != null && !item.skill.isUnlock);
                    break;
                case LevelEnum.Orange:
                    count += _levelOrangeSkills.Count(item => item.skill != null && !item.skill.isUnlock);
                    break;
                case LevelEnum.Red:
                    count += _levelRedSkills.Count(item => item.skill != null && !item.skill.isUnlock);
                    break;
            } 
            return count;
        }
    }
}