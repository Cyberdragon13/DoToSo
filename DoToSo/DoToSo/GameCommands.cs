using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DoToSo
{
    public class GameCommands
    {
        DisplayList DisplayList = new DisplayList();
        EnterMatchResults matchResults = new EnterMatchResults();
        Pairing Pairing = new Pairing();

        public List<Player> GameManagement(List<Player> playerList)
        {
            int preferedMatchsize = GetPreferedMatchsize();
            do
            {
                Console.WriteLine(preferedMatchsize);
                List<Match> matches = Pairing.GeneratePairing(playerList, preferedMatchsize);

                playerList = matchResults.AskForMatchresult(playerList, matches);

                DisplayList.ListAllPlayers(playerList);

            } while (true);
                
            return playerList;
        }


        private int GetPreferedMatchsize()
        {
            int preferedMatchsize;
            Console.WriteLine("How many players do you want to have per match");            
            do
            {
                string inputPreferedMatchsize = Console.ReadLine();
                int.TryParse(inputPreferedMatchsize, out preferedMatchsize);
                if (preferedMatchsize < 2 && preferedMatchsize > 4)
                {
                    Console.WriteLine("Enter a valid playernumber for the match");
                    Console.WriteLine("The matchsize has to lie between 2 and 4 players");
                }
            } while (preferedMatchsize < 2 && preferedMatchsize > 4);
            return preferedMatchsize;
        }
    }       
}
