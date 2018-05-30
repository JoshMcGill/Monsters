using System;
using System.Collections.Generic;

namespace Monsters
{
    // Game Logic
    class MainClass
    {
        public static void Main(string[] args)
        {
            // Create the battle room
            BattleRoom battleRoom = new BattleRoom();

            // Create monsters
            Monster enemy = new Monster("Ratata");
            Monster player = CreateMonster();

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
            Console.WriteLine("TEST ROLL = " + starterRoll);
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

        // GENERAL METHODS
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

        public static Monster CreateMonster()
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
