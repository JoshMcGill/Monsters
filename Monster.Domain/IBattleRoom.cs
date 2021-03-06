﻿using System;
namespace Monsters.Domain
{
    public interface IBattleRoom
    {
        void Register(Monster monster);
        void Attack(Monster from, Monster to, Ability ability);
        float CalculateDamage(Ability ability, bool criticalHit);
    }
}
