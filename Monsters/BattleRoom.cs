using System;
using System.Collections.Generic;

namespace Monsters
{
    // Mediator for monsters
    abstract class AbstractBattleRoom
    {
        public abstract void Register(Monster monster);
        public abstract void Attack(string from, string to, float damage);
    }

    class BattleRoom : AbstractBattleRoom
    {
        private Dictionary<string, Monster> _monsters =
            new Dictionary<string, Monster>();

        public override void Register(Monster monster)
        {
            if (!_monsters.ContainsValue(monster))
            {
                _monsters[monster.name] = monster;
            }

            monster.BattleRoom = this;
        }

        public override void Attack(string from, string to, float damage)
        {
            Monster monster = _monsters[to];

            if (monster != null)
            {
                monster.Receive(from, damage);
            }
        }
    }
}
