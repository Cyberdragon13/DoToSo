using System;
using System.Collections.Generic;
using System.Text;

namespace DoToSo
{
    public class Pairing
    {      
        private bool firstRound = true;
        public List<Match> GeneratePairing(List<Player> playerList, int preferedMatchsize)
        {
            List<Match> matches = new List<Match>();

            if (firstRound == true)
            {
                matches = GenerateRandomPairing(playerList, preferedMatchsize);
            }
            else
            {
                matches = GenerateSwissPairing(playerList, preferedMatchsize);
            }

            firstRound = false;
            return matches;
        }

        private List<Match> GenerateRandomPairing(List<Player> playerList, int preferedMatchsize)
        {
            List<int> matching = RandomNumberList(playerList.Count);
            List<Match> matches = SplitIntoMatches(playerList, matching, preferedMatchsize);

            return matches;
        }

        private List<Match> GenerateSwissPairing(List<Player> playerList, int preferedMatchsize)
        {
            List<Match> matches = new List<Match>();
            return matches;
            //mm
        }

        private List<Match> SplitIntoMatches(List<Player> playerList, List<int> matching, int preferedMatchsize)
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
    }
}
