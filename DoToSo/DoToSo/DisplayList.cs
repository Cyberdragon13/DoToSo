using System;
using System.Collections.Generic;
using System.Text;

namespace DoToSo
{
    class DisplayList
    {
        private const int maxNameLenght = 50;

        public void ListAllPlayers(List<Player> playerList)
        {

            DrawListHeader();
            int ListLenghtSinceLastLine = 0;

            foreach (Player player in playerList)
            {
                if (ListLenghtSinceLastLine > 2)
                {
                    ListLenghtSinceLastLine = 0;
                    Console.WriteLine(new String('_', 74));
                }

                int spaceLeft = maxNameLenght - player.Name.Length;
                
                string fil1 = new string(' ', spaceLeft);

                string fil2 = new string(' ', 4 - NumberLenght(player.Wins));
                string fil3 = new string(' ', 4 - NumberLenght(player.Ties));
                string fil4 = new string(' ', 4 - NumberLenght(player.Looses));

                Console.WriteLine(player.Name + fil1 + "| " + fil2 + player.Wins + " | " + fil3 + player.Ties + " | " + fil4 + player.Looses);

                ListLenghtSinceLastLine++;               
            }
        }




        void DrawListHeader()
        {
            int spaceLeft = maxNameLenght - 2;
            string fillerSpaces = new String(' ', spaceLeft);

            Console.WriteLine("Name" + fillerSpaces + "Wins    Ties    Losses");
            Console.WriteLine("__________________________________________________________________________");
        }

        int NumberLenght(int number)
        {
            int size = 0;
            while (number > 1)
            {
                size++;
            }
            return size;
        }

    }
}
