using System;
using System.Collections.Generic;
using System.Text;

namespace DoToSo
{
    class PreGameComands
    {
        public (bool, List<Player>) Comand(bool TournamentStartet, List<Player> playerList, string input)
        {
            if (TournamentStartet == false)
            {
                
                Insert InsertClass = new Insert();

                playerList = InsertClass.InsertPlayer(input, playerList);

               
                if (input == "start")
                {
                    TournamentStartet = true;
                }
                else
                {
                    Console.WriteLine("enter next Player, 'list' to list all enterd players or 'start' to generate a match");
                }
            }
            return (TournamentStartet, playerList);
        }
    }
}
