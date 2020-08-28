using System;
using System.Collections.Generic;
using System.Text;

namespace DoToSo
{
    public class Insert
    {
        private int maxNameLenght = 50;
        public void InsertPlayer(string input, List<Player> playerList)
        {
            bool listPlayer = true;

            if (input.ToLower() == "list")
            {
                DisplayList PlayerList = new DisplayList();
                PlayerList.ListAllPlayers(playerList);
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

            if (input.ToLower() == "start")
            {
                listPlayer = false;
            }

            foreach (Player player in playerList)
            {
                if (player.Name == input)
                {
                    listPlayer = false;
                    Console.WriteLine("Spieler mit diesem Namen wurde bereits hinzugefügt");
                    Console.WriteLine("");
                }
            }

            if (listPlayer == true)
            {
                playerList.Add(new Player() { Name = input, Wins = 0, Ties = 0, Looses = 0 });
            }          
        }
    }
}
