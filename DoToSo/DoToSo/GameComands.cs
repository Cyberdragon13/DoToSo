using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DoToSo
{


    class GameComands
    {
        private int preferedMatchsize = 0;

        DisplayList DisplayList = new DisplayList();
        EnterMatchResults matchResults = new EnterMatchResults();
        SwissSystem Pairing = new SwissSystem();

        public (bool TournamentEndet, List<player>) GameManagement(List<player> playerList, bool TournamentStartet, bool TournamentEndet, string input)
        {
            if (TournamentStartet == true && TournamentEndet == false)
            {
                GetPreferedMatchsize();

                List<Match> matches = Pairing.GeneratePairing(playerList, preferedMatchsize);              

                playerList = matchResults.AskForMatchresult(playerList, matches);

                DisplayList.ListAllPlayers(playerList);
            }

            return (TournamentEndet, playerList);
        }


        private void GetPreferedMatchsize()
        {
            bool validMatchsize = false;

            if (preferedMatchsize == 0)
            {
                Console.WriteLine("How many players do you want to have per match");
                do
                {
                    string inputpreferedMatchsize = Console.ReadLine();
                    int test = 0;
                    if (int.TryParse(inputpreferedMatchsize, out test))
                    {
                        preferedMatchsize = Int16.Parse(inputpreferedMatchsize);
                    }
                    else
                    {
                        preferedMatchsize = 0;
                    }

                    if (preferedMatchsize >= 2 && preferedMatchsize <= 4)
                    {
                        validMatchsize = true;
                    }
                    else
                    {
                        Console.WriteLine("Enter a valid playernumber for the match");
                        Console.WriteLine("The matchsize has to lie between 2 and 4 players");
                    }
                } while (validMatchsize == false);
            }
            return;
        }
       
    }
}
