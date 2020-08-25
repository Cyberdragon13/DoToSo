using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DoToSo
{
    class EnterMatchResults
    {

        public List<Player> AskForMatchresult(List<Player> playerList, List<Match> matches)
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

        private List<Player> AskForPlayerResults(List<Player> playerList, List<Match> matches, int matchNumber)
        {
            List<int> numberOfWinners = new List<int>(); ;

            ListMatchNumber(matches, matchNumber);
            string inputPlayerNumber = Console.ReadLine();

            if (inputPlayerNumber.Length % 2 == 1)
            {
                numberOfWinners = GetInputMatchresult(inputPlayerNumber, matchNumber, matches);
            }
            else
            {
                Console.WriteLine("Check your input");
            }

            if (numberOfWinners.Count != 0)
            {
                playerList = BookWins(playerList, matches, matchNumber, numberOfWinners);
                playerList = BookLooses(playerList, matches, matchNumber, numberOfWinners);
                matches[matchNumber].matchFinished = true;                
            }
            else
            {
                Console.WriteLine("Please enter only the corosponding number of the player");
            }
            return playerList;
        }

        private List<Player> BookWins(List<Player> playerList, List<Match> matches, int matchNumber, List<int> numberOfWinners)
        {
            for (int i = 0; i<numberOfWinners.Count; i++)
                {
                    string winnerName = matches[matchNumber].playerInMatch[numberOfWinners[i]];

                    for (int j = 0; j<playerList.Count; j++)
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
            return playerList;
        }

        private List<Player> BookLooses(List<Player> playerList, List<Match> matches, int matchNumber, List<int> numberOfWinners)
        {
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
            return playerList;
        }

        private void ListMatchNumber(List<Match> matches,int matchNumber)
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

        private List<int> GetInputMatchresult(string input, int matchNumber, List<Match> matches)
        {
            bool inputerror = false;
            int numberOfWinners = 0;
            int number = 0;

            List<int> winnerlist = new List<int>();

            for (int i = 0; i < (input.Length); i += 2)
            {
                (inputerror ,number) = CheckInputMatchresult(input, i, matches, matchNumber, winnerlist);
                if (inputerror == false)
                {
                    numberOfWinners++;
                    winnerlist.Add(number);                   
                }

                if (inputerror)
                {
                    winnerlist.Clear();
                    break;
                }
            }
         
            return winnerlist;
        }

        private (bool,int) CheckInputMatchresult(string input, int position, List<Match> matches, int matchNumber, List<int>winnerlist)
        {
            bool inputError = false;
            int number = 0;

            (inputError, number) = CheckInputMatchresultNumber(input, position, matches, matchNumber, winnerlist);
            inputError = CheckInputMatchresultLaedingPlace(input, position, inputError);

            return (inputError, number);
        }

        private (bool, int) CheckInputMatchresultNumber(string input, int position, List<Match> matches, int matchNumber, List<int> winnerlist)
        {
            bool inputError = false;
            int number = 0;

            if (int.TryParse(new string(input.ElementAt(position), 1), out number) )
            {
                number--;

                if (number >= 0 && number < matches[matchNumber].playerInMatch.Count && (winnerlist.Contains(number) == false))
                {
                   
                }
                else
                {
                    inputError = true;
                }
            }
            else
            {            
                inputError = true;
            }

            return (inputError, number);
        }

        private bool CheckInputMatchresultLaedingPlace(string input, int position, bool inputError)
        {
            if (input.Length > position+1)
            {
                if (input.ElementAt(position + 1) != ' ')
                {
                    inputError = true;
                }
            }
            return inputError;
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
