using System;
namespace Monsters
{
    interface IBattleRoom
    {
        void Register(Monster monster);
        void Attack(Monster from, Monster to, Ability ability);
        float CalculateDamage(Monster attacker, Ability ability);
    }
}
