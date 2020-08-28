using System;
using System.Collections.Generic;
using System.Text;

namespace DoToSo
{
    public class Pairing
    {
        public List<Match> GeneratePairing(List<Player> playerList, int preferedMatchsize)
        {
            List<int> matching = RandomNumberList(playerList.Count);
            List<Match> matches = SplitIntoMatches(playerList, matching, preferedMatchsize);
            return matches;
        }


        private List<Match> SplitIntoMatches(List<Player> playerList, List<int> matching, int matchsize)
        {
            List<Match> matches = new List<Match>();
            int PlayerInMatch = 0;
            int matchnumber = 0;
            int PlayersUnmatched = playerList.Count;

            AssigneWildcards(PlayersUnmatched, matchsize, matching, playerList);

            for (int i = 0; i < matching.Count; i++)
            {
                if (PlayerInMatch == 0)
                {
                    AddMatch(matches, matchnumber);
                }

                matches[matchnumber].AddPlayerToMatch(playerList[matching.IndexOf(i)].Name);
                PlayerInMatch++;

                if (PlayerInMatch == matchsize)
                {
                    matchnumber++;
                    PlayerInMatch = 0;
                    PlayersUnmatched -= matchsize;
                }
            }
            return matches;
        }
  
        private void AssigneWildcards(int PlayersUnmatched, int matchsize, List<int> matching, List<Player> playerList)
        {
            int WorstScore = playerList[0].Wins + playerList[0].Ties + playerList[0].Looses;

            Console.WriteLine(PlayersUnmatched % matchsize + " wildcard(s) assigned");

            for (int i = 0; i < (PlayersUnmatched % matchsize); i++)
            {
                int j = 0;
                while (playerList[matching.IndexOf(j)].WildcardUsed && WorstScore != playerList[matching.IndexOf(j)].Looses)
                {
                    if (j == playerList.Count-1)
                    {
                        WorstScore--;
                        j = 0;
                    }                                            
                }
                PlayersUnmatched--;
                Console.WriteLine(playerList[matching.IndexOf(j)].Name + " has drawn a wildcard");
                playerList[matching.IndexOf(j)].WildcardUsed = true;
                playerList[matching.IndexOf(j)].Wins++;
                               
                for (int k = 0; k < matching.Count; k++)
                {
                    if (matching[k] > j)
                    {
                        matching[k]--;
                    }
                }
                matching.Remove(j);
            }                
        }

        private List<int> RandomNumberList(int ListSize)
        {
            var rand = new Random();
            List<int> matching = new List<int>();

            for (int i = 0; i < ListSize; i++)
            {
                int j = rand.Next(i+1);
                matching.Insert(j, i);
            }
            return matching;
        }

        public void AddMatch(List<Match> matches, int matchnumber)
        {
            matches.Add(new Match { PlayerInMatch = new List<string>(), MatchNumber = matchnumber, MatchFinished = false });
        }
    }
}
