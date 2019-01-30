using System;

namespace Create_Task__Number_Guessing_RPG
{
    class Program
    {
        static void Main(string[] args)
        {
            guessingGame();
            Console.ReadKey();
        }

        static void guessingGame(){
            Random numberGen = new Random();
            int rightNumber;
            string[] actions = {"attack", "defend"};
            double playerHP = 100;
            double compHP = 100;
            string playerAction;
            int playerGuess;
            string compAction;
            int compGuess;
            int turnLimit = 20;

            while(true){
                Console.WriteLine($"You now have {playerHP} Health Points.");
                Console.WriteLine($"Your opponent now has {compHP} Health Points");
                rightNumber = numberGen.Next(0, 10);
                Console.Write("Attack or Defend? ");
                playerAction = Console.ReadLine();
                if(playerAction == "att" || playerAction == "attck" || playerAction == "attk" || playerAction == "a"){
                    playerAction = "attack";
                }else if(playerAction == "d" || playerAction == "def" || playerAction == "defen"){
                    playerAction = "defend";
                }
                Console.Write("Enter a number between 0 and 9: ");
                playerGuess = Convert.ToInt32(Console.ReadLine());
                compAction = actions[numberGen.Next(0, 2)];
                compGuess = numberGen.Next(0,10);
                if(compAction == "attack"){
                    Console.WriteLine("Your opponent attacked");
                }else {
                    Console.WriteLine("Your opponent decided to defend");
                }
                if (Math.Abs(rightNumber - playerGuess) > Math.Abs(rightNumber - compGuess)){
                    playerHP -= criticalChance(calculateDamage(compAction, playerAction, Math.Abs(rightNumber - compGuess), Math.Abs(rightNumber - playerGuess)));
                }else if (Math.Abs(rightNumber - playerGuess) < Math.Abs(rightNumber - compGuess)){
                    compHP -= criticalChance(calculateDamage(playerAction, compAction, Math.Abs(rightNumber - playerGuess), Math.Abs(rightNumber - compGuess)));
                }else{
                    Console.WriteLine("No Effect!");
                }
                turnLimit--;
                Console.WriteLine($"{turnLimit} turns left");

                if(playerHP <= 0){
                    break;
                }else if(compHP <= 0){
                    break;
                }else if(turnLimit == 0){
                    break;
                }else{
                    continue;
                }
            }
            if(playerHP > compHP){
                Console.WriteLine("You Win!");
                Console.WriteLine("Must've gotten lucky");
            }else {
                Console.WriteLine("You Lose! You're a bad guesser");
            }
        }
        static double calculateDamage(string winnerAction, string loserAction, int winDistance, int lostDistance){
            double damage = 0;
            if (winnerAction == "attack" && loserAction == winnerAction){
                damage = 10;
            }else if (winnerAction == "defend" && loserAction == "attack"){
                if(winDistance == 0){
                    Console.WriteLine("Perfect Parry!");
                    damage = 10;
                }else if (winDistance <= 3 && winDistance > 0){
                    Console.WriteLine("Parry!");
                    damage = 5;
                }else if (winDistance <= 7 && winDistance > 3){
                    Console.WriteLine("Parry!");
                    damage = 2.5;
                }else{
                    Console.WriteLine("Blocked");
                }
            }else if (winnerAction == "attack" && loserAction == "defend"){
                if (lostDistance == 1){
                    damage = .5;
                }else if (lostDistance <= 4 && lostDistance > 1){
                    damage = 2.5;
                }else if(lostDistance <= 8 && lostDistance > 4){
                    damage = 5;
                }else {
                    damage = 8.5;
                }
            }else {
                damage = 0;
            }
            return damage;
        }
        static double criticalChance(double damage){
            Random numberGen = new Random();
            int chance = numberGen.Next(0, 51);
            if(damage > 0){
                if(chance >= 40){
                    Console.WriteLine("Critical!");
                    damage *= 2.5;
                }
            }
            return damage;
        }
    }
}