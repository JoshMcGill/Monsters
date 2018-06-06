using System;
using System.Collections.Generic;

namespace Monsters
{
    class Monster
    {
        // Class vars
        public string name;
        public string type;
        public float health = 150;
        public List<Ability> abilities = new List<Ability>();

        public Monster(string _name)
        {
            name = _name;
            type = "normal";
            AddAbilities("Quick Attack", 25, 25, 1.5f);
            AddAbilities("Punch", 20, 50, 2f);
        }

        // Class Methods
        // Receives attack from enemy
        public void Receive(string from, float damage)
        {
            if ((health - damage) <= 0)
            {
                health = 0;
            }
            else
            {
                health -= damage;
            }
        }

        private void AddAbilities(string _name, int _damage, int _critChance, float _critMultiplier)
        {
            Ability ability = new Ability(_name, _damage, "normal", _critChance, _critMultiplier);
            abilities.Add(ability);
        }
    }
}
