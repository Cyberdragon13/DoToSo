using System;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace DoToSo
{
    class Program
    {
        const int maxNameLenght = 50;
        static void Main(string[] args)
        {
            startupMessage();

            List<player> playerList = new List<player>();
            string input;

            do
            {

                input = getInputFromConsole();




                playerList = insertPlayer(input, playerList);



                Console.WriteLine("enter next Player, 'list' to list all enterd players or start to generate a match");



            } while (input != "exit");
        }




        public class player
        {
            //Properties
            public string Name { get; set; }
            public int Wins { get; set; }
            public int Ties { get; set; }
            public int Looses { get; set; }
        }


        static void startupMessage()
        {
            Console.WriteLine("Dominion Tournament Software");
            Console.WriteLine("Enter a new playername:");
            return;
        }


        static string getInputFromConsole()
        {
            string input;

            input = Console.ReadLine();

            return input;
        }


        static List<player> insertPlayer(string input, List<player> playerList)
        {
            bool listPlayer = true;

            if (input == "list")
            {
                listAllPlayers(playerList);
                listPlayer = false;
                Console.WriteLine("");
            }

            if (String.IsNullOrWhiteSpace(input) == true)
            {
                Console.WriteLine("Enter a valid playername");
                Console.WriteLine("");
                listPlayer = false;
            }


            if (input.Length > maxNameLenght)
            {
                Console.WriteLine("Player Name has to be" + maxNameLenght + "caracters or less");
                Console.WriteLine("");
                listPlayer = false;
            }


            foreach (player checkDouble in playerList)
            {
                if (checkDouble.Name == input)
                {
                    listPlayer = false;
                    Console.WriteLine("Spieler mit diesem Namen wurde bereits hinzugefügt");
                    Console.WriteLine("");
                }
            }

            if (listPlayer == true)
            {
                playerList.Add(new player() { Name = input, Wins = 0, Ties = 0, Looses = 0 });
            }

            return playerList;
        }


        static string generateFillerSpaces(int numberOfWhithspaces)
        {
            string fillerSpaces = "";
            for (int i = 0; i < numberOfWhithspaces; i++)
            {
                fillerSpaces = fillerSpaces + " ";
            }
            return fillerSpaces;
        }




        static void drawListHeader()
        {
            int spaceLeft = maxNameLenght - 2;
            string fillerSpaces = generateFillerSpaces(spaceLeft);

            Console.WriteLine("Name" + fillerSpaces + "Wins    Ties    Losses");
            Console.WriteLine("__________________________________________________________________________");
        }




        

   
        static int numberLenght(int number)
        {
            int size = 0;

            while (number > 1)
            {
                size++;
            }

            return size;
        }

        static void listAllPlayers(List<player> playerList)
        {

            drawListHeader();
            int counter = 0;

            foreach (player player in playerList)
            {
                int nameLenght = player.Name.Length;
                int spaceLeft = maxNameLenght - nameLenght;

                string fil1 = generateFillerSpaces(spaceLeft);

               
                string fil2 = generateFillerSpaces(4 - numberLenght(player.Wins));
                string fil3 = generateFillerSpaces(4 - numberLenght(player.Ties));
                string fil4 = generateFillerSpaces(4 - numberLenght(player.Looses));

                Console.WriteLine(player.Name + fil1 + "| "+ fil2 + player.Wins + " | "+ fil3 + player.Ties + " | " + fil4  + player.Looses );

                counter++;
                if (counter > 2)
                {
                    counter = 0;
                    Console.WriteLine("__________________________________________________________________________");
                }

            }
        }

       
    }
}


