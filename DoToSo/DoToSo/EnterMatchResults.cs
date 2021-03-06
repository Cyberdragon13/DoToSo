﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DoToSo
{
    public class EnterMatchResults
    {
        public List<Player> AskForMatchresult(List<Player> playerList, List<Match> matches)
        {
            bool roundFinished;
            do
            {
                ShowPairings(matches);
                Console.WriteLine();
                Console.WriteLine("Enter the match number of which you want to enter the result");

                string inputMatchResult = Console.ReadLine();

                if (int.TryParse(inputMatchResult, out int test))
                {
                    int InputMatchInt = Int16.Parse(inputMatchResult) - 1;

                    if (InputMatchInt >= 0 && InputMatchInt < matches.Count)
                    {
                        if (matches[InputMatchInt].MatchFinished == false)
                        {
                            playerList = AskForMatchResult(playerList, matches, InputMatchInt);
                        }
                        else
                        {
                            Console.WriteLine("Please enter an other match number");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Please enter a valid number");
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

        private List<Player> AskForMatchResult(List<Player> playerList, List<Match> matches, int matchNumber)
        {
            List<int> numberOfWinners = new List<int>(); ;

            ListPlayerNumbers(matches, matchNumber);
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
                playerList = BookLosses(playerList, matches, matchNumber, numberOfWinners);
                matches[matchNumber].MatchFinished = true;
            }
            else
            {
                Console.WriteLine("Please enter only the corresponding number of the player");
            }
            return playerList;
        }

        private List<Player> BookWins(List<Player> playerList, List<Match> matches, int matchNumber, List<int> numberOfWinners)
        {
            for (int i = 0; i < numberOfWinners.Count; i++)
            {
                Player winner = matches[matchNumber].PlayersInMatch[numberOfWinners[i]];

                for (int j = 0; j < playerList.Count; j++)
                {
                    if (playerList[j].Id == winner.Id)
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

        private List<Player> BookLosses(List<Player> playerList, List<Match> matches, int matchNumber, List<int> numberOfWinners)
        {
            for (int i = 0; i < matches[matchNumber].PlayersInMatch.Count; i++)
            {
                if (numberOfWinners.Contains(i) == false)
                {
                    Player loser = matches[matchNumber].PlayersInMatch[i];
                    for (int j = 0; j < playerList.Count; j++)
                    {
                        if (playerList[j].Id == loser.Id)
                        {
                            playerList[j].Loses++;
                            break;
                        }
                    }
                }
            }
            return playerList;
        }

        private void ListPlayerNumbers(List<Match> matches, int matchNumber)
        {
            int playernumber = 1;
            Console.WriteLine("Who has won? Multiple players posible. Seperate Multiple inputs with spaces");

            foreach (Player player in matches[matchNumber].PlayersInMatch)
            {
                Console.WriteLine(playernumber + ": " + player.Name);
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
                (inputerror, number) = CheckInputMatchresult(input, i, matches, matchNumber, winnerlist);
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

        private (bool, int) CheckInputMatchresult(string input, int position, List<Match> matches, int matchNumber, List<int> winnerlist)
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

            if (int.TryParse(new string(input.ElementAt(position), 1), out number))
            {
                number--;

                if (number >= 0 && number < matches[matchNumber].PlayersInMatch.Count && (winnerlist.Contains(number) == false))
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
            if (input.Length > position + 1)
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
                if (match.MatchFinished == false)
                {
                    Console.WriteLine(new string('_', 74));
                    Console.WriteLine("Match Number:" + matchnumber);
                    Console.WriteLine();

                    foreach (Player player in match.PlayersInMatch)
                    {
                        Console.WriteLine(player.Name);
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
                if (matches[i].MatchFinished == false)
                {
                    roundFinished = false;
                }
            }
            return roundFinished;
        }
    }
}
