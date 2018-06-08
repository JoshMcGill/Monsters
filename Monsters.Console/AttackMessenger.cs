using System;
namespace Monsters.Domain
{
    public class AttackMessenger
    {
        public void OnAttackCompleted(object source, AttackEventArgs attack)
        {
            if (attack.IsCriticalHit)
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
