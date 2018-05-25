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

    // Monster class
    class Monster
    {
        // Class vars
        public string name;
        public string type;
        public float health = 150;
        public List<Ability> abilities = new List<Ability>();
        private BattleRoom _battleroom;


        // Class constructors
        public Monster () 
        {
            name = "Ditto";
            type = "normal";
            AddAbilities("Quick Attack", 30, 25, 1.5f);
            AddAbilities("Punch", 20, 50, 2f);
        }

        public Monster (string _name)
        {
            name = _name;
            type = "normal";
            AddAbilities("Quick Attack", 25, 25, 1.5f);
            AddAbilities("Punch", 20, 50, 2f);
        }


        // Class Methods
        public BattleRoom BattleRoom
        {
            set { _battleroom = value; }
            get { return _battleroom; }
        }

        // Sends attack to given monster
        public void Send(string to, float damage)
        {
            _battleroom.Attack(name, to, damage);
        }

        // Receives attack from enemy
        public virtual void Receive(string from, float damage)
        {
            Console.WriteLine("{0} dealt {2} damage to {1}!",
              from, name, damage);

            if ((health - damage) <=  0) 
            {
                health = 0;
                Console.WriteLine("");
                Console.WriteLine("{0} has fainted!", name);
                Console.WriteLine("");
                Console.WriteLine("GAME OVER!");
            }
            else 
            {
                health -= damage;
            }
        }

        private void AddAbilities(string _name, int _damage, int _critChance, float _critMultiplier)
        {
            Ability ability = new Ability(_name, _damage, "normal", _critChance, _critMultiplier);
            abilities.Add(ability);
        }
    }

    // Ability class
    class Ability {
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

    // Game Logic
    class MainClass
    {
        public static void Main(string[] args)
        {
            // Create the battle room
            BattleRoom battleRoom = new BattleRoom();

            // Create monsters
            Monster enemy = new Monster("Ratata");
            Monster player = CreatePokemon();

            // Register monsters to chat room
            battleRoom.Register(enemy);
            battleRoom.Register(player);

            // Start game
            Console.WriteLine("Name: " + player.name);
            Console.WriteLine("Health: " + player.health);

            Console.WriteLine(" ");
            Console.WriteLine("Time for battle! Meet your opponent!");
            Console.WriteLine(" ");

            Console.WriteLine("Name: " + enemy.name);
            Console.WriteLine("Health: " + enemy.health);

            Console.WriteLine(" ");
            Console.WriteLine("------BATTLE------");
            Console.WriteLine(" ");

            // Start Battle
            Random roll = new Random();
            int starterRoll = roll.Next(0, 2);
            Monster attacker;
            Monster reciever;

            // Randomly Decide who attacks first
            if (starterRoll == 0)
            {
                attacker = player;
                reciever = enemy;
            }
            else
            {
                attacker = enemy;
                reciever = player;
            }

            // Keep battle going until one of the players reaches 0 health
            while (enemy.health > 0 && player.health > 0) {
                WriteStatus(player, enemy, attacker);

                if (attacker == player)
                {
                    Attack(attacker, reciever);

                    attacker = enemy;
                    reciever = player;
                }
                else {
                    EnemyAttack(attacker, reciever);

                    attacker = player;
                    reciever = enemy;
                }

                Console.WriteLine("");
                Console.WriteLine("--------END TURN--------");
                Console.WriteLine("");
            }

        }

        public static void Attack(Monster attacker, Monster reciever)
        {
            Console.WriteLine("What ability would you like to use?");

            foreach (var ability in attacker.abilities)
            {
                Console.WriteLine(ability.name);
            }

            Console.WriteLine("");

            bool abilityChosen = false;

            while (!abilityChosen)
            {
                Console.WriteLine("Type the name of the ability you would like to use:");
                string userInput = Console.ReadLine();

                foreach (var ability in attacker.abilities)
                {
                    if (ability.name == userInput)
                    {
                        abilityChosen = true;
                        attacker.Send(reciever.name, CalculateDamage(attacker, ability));
                    }
                }
            }

            Console.WriteLine("");
            Console.WriteLine("Press anything to continue");
            Console.ReadLine();
        }

        public static void EnemyAttack(Monster attacker, Monster reciever)
        {
            Random roll = new Random();
            int abilityRoll = roll.Next(0, attacker.abilities.Count);

            attacker.Send(reciever.name, CalculateDamage(attacker, attacker.abilities[abilityRoll]));
        }

        public static float CalculateDamage(Monster attacker, Ability ability)
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

        public static Monster CreatePokemon()
        {
            Console.WriteLine("Choose a name for your Monster!");
            string input = Console.ReadLine();
            return new Monster(input);
        }

        public static void WriteStatus(Monster player, Monster enemy, Monster attacker)
        {
            Console.WriteLine(player.name + " health: " + player.health);
            Console.WriteLine(enemy.name + " health: " + enemy.health);
            Console.WriteLine("");

            if (attacker == player)
            {
                Console.WriteLine("It is your turn:");
            }
            else
            {
                Console.WriteLine("It is " + attacker.name + "'s turn:");
            }

        }
    }
}
