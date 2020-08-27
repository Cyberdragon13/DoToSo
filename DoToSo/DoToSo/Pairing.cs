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

            for (int i = 0; i < matching.Count; i++)
            {

                if ((PlayersUnmatched % matchsize) == 0)
                {
                    if (PlayerInMatch == 0)
                    {
                        AddMatch(matches,matchnumber);                        
                    }
                   
                    matches[matchnumber].PlayerInMatch.Add(playerList[matching.IndexOf(i)].Name);
                    PlayerInMatch++;

                    if (PlayerInMatch == matchsize)
                    {                     
                        matchnumber++;
                        PlayerInMatch = 0;
                        PlayersUnmatched -= matchsize;
                    }
                }
                else
                {
                    PlayersUnmatched--;
                    Console.WriteLine(playerList[matching.IndexOf(i)].Name + " has drawn a wildcard");
                    playerList[i].WildcardUsed = true;
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

        public void AddMatch(List<Match> matches, int matchnumber)
        {
            matches.Add(new Match { PlayerInMatch = new List<string>(), MatchNumber = matchnumber, MatchFinished = false });
        }
    }
}
