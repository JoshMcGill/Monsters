﻿using System;
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
        private BattleRoom _battleroom;

        public Monster(string _name)
        {
            name = _name;
            type = "normal";
            AddAbilities("Quick Attack", 25, 25, 1.5f);
            AddAbilities("Punch", 20, 50, 2f);
        }

        // Class Methods
        public BattleRoom BattleRoom
        {
            set { _battleroom = value; }
            get { return _battleroom; }
        }

        // Sends attack to given monster
        public void Send(string to, float damage)
        {
            _battleroom.Attack(name, to, damage);
        }

        // Receives attack from enemy
        public virtual void Receive(string from, float damage)
        {
            Console.WriteLine("{0} dealt {2} damage to {1}!",
              from, name, damage);

            if ((health - damage) <= 0)
            {
                health = 0;
                Console.WriteLine("");
                Console.WriteLine("{0} has fainted!", name);
                Console.WriteLine("");
                Console.WriteLine("GAME OVER!");
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