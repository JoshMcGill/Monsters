using System;
namespace Monsters.Domain
{
    public class AttackEventArgs: EventArgs
    {
        public Monster From { get; set; }
        public Ability Ability { get; set; }
        public bool IsCriticalHit { get; set; }
        public float Damage { get; set; }
    }
}
