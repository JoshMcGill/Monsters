using System;
using System.Collections.Generic;

namespace Monsters
{
    class BattleRoom : IBattleRoom
    {
        public event EventHandler<AttackEventArgs> AttackCompleted;

        private Dictionary<string, Monster> _monsters =
            new Dictionary<string, Monster>();
        
        protected virtual void OnAttackCompleted(AttackEventArgs attack)
        {
            if (AttackCompleted != null)
            {
                AttackCompleted(this, attack);
            }
        }

        public void Register(Monster monster)
        {
            if (!_monsters.ContainsValue(monster))
            {
                _monsters[monster.name] = monster;
            }
        }

        public void Attack(Monster from, Monster to, Ability ability)
        {
            // Create a variable of monster to attack
            Monster monster = _monsters[to.name];

            // Create AttackEventArgs and fill it
            AttackEventArgs attack = new AttackEventArgs
            {
                From = from,
                Ability = ability,
                CriticalHit = CriticalHitCheck(ability)
            };
            attack.Damage = CalculateDamage(attack.Ability, attack.CriticalHit);

            // If the monster exists, then attack
            if (monster != null)
            {
                monster.Receive(from.name, attack.Damage);
                OnAttackCompleted(attack);
            }
        }

        public bool CriticalHitCheck(Ability ability)
        {
            Random randomNumber = new Random();
            int criticalAttack = randomNumber.Next(1, 101);

            if (ability.critChance <= criticalAttack)
            {
                return true;
            }

            return false;
        }

        public float CalculateDamage(Ability ability, bool criticalHit)
        {
            if (criticalHit)
            {
                return ability.damage * ability.critMultiplier;
            }

            return ability.damage;
        }
    }
}
