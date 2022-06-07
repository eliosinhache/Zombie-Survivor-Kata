using System;
using System.Collections.Generic;

namespace Classes
{
    public class SkillTree : ISkillTree
    {
        class Nodo
        {
            public Skill skill;
            public Nodo left, right;
        }

        Nodo raiz;

        public SkillTree()
        {
            raiz = null;
            Skill skill = new Skill();
            skill.description = "+1 Action";
            skill.lvlToUnlock = "Yellow";
            skill.minExperienceNeeded = 7;
            AddNewSkill(skill);
            
            Skill skill2 = new Skill();
            skill2.description = "+1 Die";
            skill2.lvlToUnlock = "Orange";
            AddNewSkill(skill2);
            
            Skill skill3 = new Skill();
            skill3.description = "+1 Die";
            skill3.lvlToUnlock = "Orange";
            skill3.description = "+1 Free Move Action";
            AddNewSkill(skill3);

            Skill skill4 = new Skill();
            skill4.description = "Hoard";
            skill4.lvlToUnlock = "Red";
            skill4.minExperienceNeeded = 42;
            AddNewSkill(skill4);
            
            Skill skill5 = new Skill();
            skill5.description = "Tough";
            skill5.lvlToUnlock = "Red";
            skill5.minExperienceNeeded = 42;
            AddNewSkill(skill5);
            
            Skill skill6 = new Skill();
            skill6.description = "Sniper";
            skill6.lvlToUnlock = "Red";
            skill6.minExperienceNeeded = 42;
            AddNewSkill(skill6);
        }

        public void AddNewSkill (Skill newSkill)
        {
            Nodo skill;
            skill = new Nodo ();
            skill.skill = newSkill;
            skill.left = null;
            skill.right = null;
            if (raiz == null)
                raiz = skill;
            else
            {
                Nodo anterior = null, reco;
                reco = raiz;
                while (reco != null)
                {
                    anterior = reco;
                    if (newSkill.minExperienceNeeded < reco.skill.minExperienceNeeded)
                        reco = reco.left;
                    else
                        reco = reco.right;
                }
                if (newSkill.minExperienceNeeded < anterior.skill.minExperienceNeeded)
                    anterior.left = skill;
                else
                    anterior.right = skill;
            }
        }
        
        private int ImprimirEntre (Nodo reco, int count)
        {
            if (reco != null)
            {
                if (reco.skill.isUnlock) {count++;}
                count += ImprimirEntre (reco.left, count);
                count += ImprimirEntre (reco.right, count);
            }

            return count;
        }

        public int ImprimirEntre (int count)
        {
            count += ImprimirEntre (raiz, count);
            Console.WriteLine();
            return count;
        }

        public int UnlockedSkills()
        {
            int count = 0;
            count += ImprimirEntre(count);
            return count;
        }

        public int avaibleSkills(string lvl)
        {
            int count = 0;
            count += SearchCantOfSkillsPerLevel(count, lvl);
            return count;
        }

        public int LockedSkills()
        {
            int count = CantOfLockedSkills();
            return count;
        }

        private int CantOfLockedSkills()
        {
            return CantOfLockedSkills(0, raiz);
        }
        private int CantOfLockedSkills(int count, Nodo recc)
        {
            if (recc != null)
            {
                if (!recc.skill.isUnlock)
                {
                    count++;
                }

                count = CantOfLockedSkills(count, recc.left);
                count = CantOfLockedSkills(count, recc.right);
            }

            return count;
        }

        private int SearchCantOfSkillsPerLevel(int count, string lvl)
        {
            count += SearchCantOfSkillsPerLevel(count, raiz, lvl);
            return count;
        }

        private int SearchCantOfSkillsPerLevel(int count, Nodo recc, string lvl)
        {
            if (recc != null)
            {
                if (recc.skill.lvlToUnlock == lvl) { count++; }
                count = SearchCantOfSkillsPerLevel(count, recc.left, lvl);
                count = SearchCantOfSkillsPerLevel(count, recc.right, lvl);
            }
            return count;
        }
    }
}