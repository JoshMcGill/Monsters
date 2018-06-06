using System;
namespace Monsters
{
    class AttackEventArgs: EventArgs
    {
        public Monster From { get; set; }
        public Ability Ability { get; set; }
        public bool CriticalHit { get; set; }
        public float Damage { get; set; }
    }
}
