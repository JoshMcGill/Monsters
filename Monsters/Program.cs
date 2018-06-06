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

            // Create Messenger and subscribe to the attack
            AttackMessenger attackMessenger = new AttackMessenger();
            battleRoom.AttackCompleted += attackMessenger.OnAttackCompleted;

            // Create enemy
            Monster enemy = new Monster("Ratata");

            // Create players monster
            Console.WriteLine("Choose a name for your Monster!");
            string input = Console.ReadLine();
            Monster player = new Monster(input);

            // Register monsters to battleRoom
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
            string deadMonsterName;


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
                    // Pick Ability
                    Console.WriteLine("What ability would you like to use?");
                    PrintAbilities(attacker);
                    string userInput = Console.ReadLine();

                    Ability chosenAbility = PickAbility(attacker, userInput);

                    while (chosenAbility == null)
                    {
                        Console.WriteLine("The ability you chose is not valid, please try again.");
                        userInput = Console.ReadLine();

                        chosenAbility = PickAbility(attacker, userInput);
                    }

                    // Attack
                    battleRoom.Attack(attacker, reciever, chosenAbility);

                    // Change turn
                    attacker = enemy;
                    reciever = player;

                    // Allow player to acknowledge what happened
                    Console.WriteLine("");
                    Console.WriteLine("Press anything to continue");
                    Console.ReadLine();
                }
                else {
                    // Randomly pick ability
                    int abilityRoll = roll.Next(0, attacker.abilities.Count);

                    // Attack
                    battleRoom.Attack(attacker, reciever, attacker.abilities[abilityRoll]);

                    // Change Turn
                    attacker = player;
                    reciever = enemy;
                }

                Console.WriteLine("");
                Console.WriteLine("--------END TURN--------");
                Console.WriteLine("");
            }

            if (attacker.health == 0) {
                deadMonsterName = attacker.name;
            }
            else {
                deadMonsterName = reciever.name;
            }

            Console.WriteLine("");
            Console.WriteLine("{0} has fainted!", deadMonsterName);
            Console.WriteLine("");
            Console.WriteLine("GAME OVER!");

        }

        // GENERAL METHODS
        // Allows player to choose the ability to attack with
        public static Ability PickAbility(Monster attacker, string move)
        {
            foreach (var ability in attacker.abilities)
            {
                if (ability.name == move)
                {
                    return ability;
                }
            }

            return null;
        }

        // Print a list of all abilities from a monster to the console
        public static void PrintAbilities(Monster monster)
        {
            foreach (var ability in monster.abilities)
            {
                Console.WriteLine(ability.name);
            }

            Console.WriteLine("");
        }

        // Write the status of the match to the console
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
