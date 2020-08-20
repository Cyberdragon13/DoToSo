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
        private bool firstRound = true;
        List<Match> matches = new List<Match>();
        DisplayList DisplayList = new DisplayList();

        public (bool TournamentEndet, List<player>) GameManagement(List<player> playerList, bool TournamentStartet, bool TournamentEndet, string input)
        {
            if (TournamentStartet == true && TournamentEndet == false)
            {
                GetPreferedMatchsize();

                matches = GeneratePairing(playerList);              

                playerList = AskForMatchresult(playerList, matches);

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

        private List<Match> GeneratePairing(List<player> playerList)
        {
            List<Match> matches = new List<Match>();

            if (firstRound == true)
            {
                matches = GenerateRandomPairing(playerList);
            }
            else
            {
                matches = GenerateSwissPairing(playerList);
            }

            firstRound = false;
            return matches;
        }

        private List<Match> GenerateRandomPairing(List<player> playerList)
        {
            List<int> matching = RandomNumberList(playerList.Count);
            List<Match> matches = SplitIntoMatches(playerList, matching);

            return matches;
        }

        private List<Match> GenerateSwissPairing(List<player> playerList)
        {
            List<Match> matches = new List<Match>();
            return matches;
            //mm
        }

        private List<Match> SplitIntoMatches(List<player> playerList, List<int> matching)
        {
            List<Match> matches = new List<Match>();
            int PlayerInMatch = 0;
            int matchnumber = 0;
            int Playersleft = playerList.Count;

            for (int i = 0; i < matching.Count; i++)
            {
                if (PlayerInMatch == 0)
                {
                    matches.Add(new Match { playerInMatch = new List<string>(), matchNumber = matchnumber, matchFinished = false });
                }

                matches[matchnumber].playerInMatch.Add(playerList[matching.IndexOf(i)].Name);

                PlayerInMatch++;
                if ((Playersleft % preferedMatchsize) == 0)
                {
                    if (PlayerInMatch == preferedMatchsize)
                    {
                        matchnumber++;
                        PlayerInMatch = 0;
                        Playersleft -= preferedMatchsize;
                    }
                }
                else
                {
                    switch (preferedMatchsize)
                    {
                        case 2:
                            if (PlayerInMatch == 3)
                            {
                                matchnumber++;
                                PlayerInMatch = 0;
                                Playersleft -= 3;
                            }
                            break;
                        case 3:
                            if (PlayerInMatch == 4)
                            {
                                matchnumber++;
                                PlayerInMatch = 0;
                                Playersleft -= 4;
                            }
                            break;
                        default:
                            if (PlayerInMatch == 3)
                            {
                                matchnumber++;
                                PlayerInMatch = 0;
                                Playersleft -= 3;
                            }
                            break;
                    }
                }

            }
            return matches;
        }

        private List<int> RandomNumberList(int ListSize)
        {
            var rand = new Random();
            List<int> matching = new List<int>();

            for (int i = 0; i < ListSize; i++)
            {
                int j = rand.Next(i);
                matching.Insert(j, i);
            }

            return matching;
        }

        private void ShowPairings(List<Match> matches)
        {
            int matchnumber = 1;
            Console.WriteLine("Current active matches are betwen:");
            foreach (Match match in matches)
            {             
                if (match.matchFinished == false)                
                {
                    Console.WriteLine(new string('_', 74));
                    Console.WriteLine("Match Number:" + matchnumber);
                    Console.WriteLine();

                    foreach (string name in match.playerInMatch)
                    {
                        Console.WriteLine(name);
                    }
                }
                matchnumber++;
            }
        }

        private List<player> AskForMatchresult(List<player> playerList, List<Match> matches)
        {
            bool roundFinished = false;
            do
            {
                ShowPairings(matches);
                Console.WriteLine();
                Console.WriteLine("Enter the matchnumber of which you want to enter the result");

                string inputMatchResult = Console.ReadLine();

                int test = 0;
                if (int.TryParse(inputMatchResult, out test))
                {
                    int InputMatchInt = Int16.Parse(inputMatchResult) - 1;

                    if (InputMatchInt >= 0 && InputMatchInt < matches.Count)
                    {
                        if (matches[InputMatchInt].matchFinished == false)
                        {                           
                            playerList = AskForPlayerResults(playerList, matches, InputMatchInt);
                        }
                        else
                        {
                            Console.WriteLine("Please enter an other match number");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Pleas enter a valid number");
                    }
                }
                else
                {
                    Console.WriteLine("Please enter an existing match number");
                }
                roundFinished = CheckedIfRoundFinished(matches);

            } while (roundFinished == false);

            return playerList;
        }

        private List<player> AskForPlayerResults(List<player> playerList, List<Match> matches, int matchNumber)
        {
            List<int> numberOfWinners = new List<int>(); ;
            
            ListMatchNumber(matchNumber);
            string inputPlayerNumber = Console.ReadLine();
             
            if (inputPlayerNumber.Length % 2 == 1)
            {                   
                numberOfWinners = CheckInputMatchresult(inputPlayerNumber, matchNumber, matches);
            }
            else
            {
                Console.WriteLine("Check your input");
            }

            if (numberOfWinners.Count == 0)
            {
                Console.WriteLine("Please enter only the corosponding number of the player");
            }
            else
            {                                    
                for (int i = 0; i < numberOfWinners.Count; i++)
                {
                    string winnerName = matches[matchNumber].playerInMatch[numberOfWinners[i]];
                     
                    for (int j = 0; j < playerList.Count; j++)
                    {
                        if (playerList[j].Name == winnerName)
                        {
                            if (numberOfWinners.Count == 1)
                            {
                                playerList[j].Wins++;
                            }
                            else
                            {
                                playerList[j].Ties++;
                            }
                            break;
                        }
                    }
                }
                   

                for (int i = 0; i < matches[matchNumber].playerInMatch.Count; i++)
                {
                    if (numberOfWinners.Contains(i) == false)
                    {
                        string looserName = matches[matchNumber].playerInMatch[i];
                        for (int j = 0; j < playerList.Count; j++)
                        {                                    
                            if (playerList[j].Name == looserName)
                            {
                                playerList[j].Looses++;
                                break;
                            }
                        }
                    }                         
                }
                matches[matchNumber].matchFinished = true;
            }
                
            return playerList;
        }


        private void ListMatchNumber(int matchNumber)
        {
            int playernumber = 1;
            Console.WriteLine("Who has won? Multiple players posible. Seperate Multiple inputs with spaces");
            foreach (string playerName in matches[matchNumber].playerInMatch)
            {
                Console.WriteLine(playernumber + ": " + playerName);
                playernumber++;
            }
            return;        
        }


        private List<int> CheckInputMatchresult(string input, int matchNumber, List<Match> matches)
        {
            bool inputerror = false;
            int numberOfWinners = 0;
            int number = 0;

            List<int> winnerlist = new List<int>();

            for (int i = 0; i<(input.Length); i+=2)
            {
                if (int.TryParse(new string(input.ElementAt(i), 1), out number) == true)            
                {
                    inputerror = false;
                    number--;
                    if (number >= 0 && number < matches[matchNumber].playerInMatch.Count && winnerlist.Contains(number) == false )
                    {
                        numberOfWinners++;
                        winnerlist.Add(number);
                    }
                    else
                    {
                        inputerror = true;
                    }
                }
                else
                {
                    inputerror = true;
                }                                    
            }

            if (inputerror)
            {
                winnerlist.Clear();
            }

            return winnerlist;
        }


        private bool CheckedIfRoundFinished(List<Match> matches)
            {
                bool roundFinished = true;
                for (int i = 0; i < matches.Count; i++)
                {
                    if (matches[i].matchFinished == false)
                    {
                        roundFinished = false;
                    }
                }
                return roundFinished;
            }
        
    }
}
