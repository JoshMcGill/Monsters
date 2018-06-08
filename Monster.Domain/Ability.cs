using System;
namespace Monsters.Domain
{
    public class Ability
    {
        // Class Vars
        public string name;
        public int damage;
        public string type;
        public int critChance = 25;
        public float critMultiplier = 1.50f;

        // Class Constructors
        public Ability(string _name, int _damage)
        {
            name = _name;
            damage = _damage;
            type = "normal";
        }

        public Ability(string _name, int _damage, string _type)
        {
            name = _name;
            damage = _damage;
            type = _type;
        }

        public Ability(string _name, int _damage, string _type, int _critChance)
        {
            name = _name;
            damage = _damage;
            type = _type;
            critChance = _critChance;
        }

        public Ability(string _name, int _damage, string _type, int _critChance, float _critMultiplier)
        {
            name = _name;
            damage = _damage;
            type = _type;
            critChance = _critChance;
            critMultiplier = _critMultiplier;
        }
    }
}
