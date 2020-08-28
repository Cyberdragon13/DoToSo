using System;
using System.Collections.Generic;
using System.Text;

namespace DoToSo
{
    public class Match
    {
        public List<string> PlayerInMatch { get; set; }
        public int MatchNumber { get; set; }
        public bool MatchFinished { get; set; }

        public void AddPlayerToMatch(string name)
        {
            PlayerInMatch.Add(name);
        }
        
    }
}
