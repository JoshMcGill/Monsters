using System;
using System.Collections.Generic;

namespace Monsters
{
    class BattleRoom : IBattleRoom
    {
        private Dictionary<string, Monster> _monsters =
            new Dictionary<string, Monster>();

        public void Register(Monster monster)
        {
            if (!_monsters.ContainsValue(monster))
            {
                _monsters[monster.name] = monster;
            }
        }

        public void Attack(Monster from, Monster to, Ability ability)
        {
            Monster monster = _monsters[to.name];
            float calculatedDamage = CalculateDamage(from, ability);

            if (monster != null)
            {
                monster.Receive(from.name, calculatedDamage);
            }
        }

        public float CalculateDamage(Monster attacker, Ability ability)
        {
            Random randomNumber = new Random();
            int criticalAttack = randomNumber.Next(1, 101);

            if (ability.critChance <= criticalAttack)
            {
                Console.WriteLine(attacker.name + " used " + ability.name + ", it was a critical hit!");
                return ability.damage * ability.critMultiplier;
            }

            Console.WriteLine(attacker.name + " used " + ability.name);
            return ability.damage;
        }
    }
}
