using System;
namespace Monsters
{
    class AttackMessenger
    {
        public void OnAttackCompleted(object source, AttackEventArgs attack)
        {
            if (attack.CriticalHit)
            {   
                Console.WriteLine("{0} used {1} for {2} damage! Critical hit!", attack.From.name, attack.Ability.name, attack.Damage);
            }
            else
            {
                Console.WriteLine("{0} used {1} for {2} damage!", attack.From.name, attack.Ability.name, attack.Damage);
            }
        }
    }
}
