using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DoToSo
{
    public class Match
    {
        public Match()
        {
            PlayersInMatch = new List<player>();
        }

        public Match(player player1, player player2)
        {
            PlayersInMatch = new List<player>();
            PlayersInMatch.Add(player1);
            PlayersInMatch.Add(player2);
        }

        public bool IsForbidden => PlayersInMatch
                .SelectMany(players => players.AlreadyPlayedOpponentIds)
                .Intersect(PlayersInMatch.Select(player => player.Id))
                .Any();

        public List<player> PlayersInMatch { get; set; }

        public List<string> playerInMatch = new List<string>();
        public int matchNumber;
        public bool matchFinished;
    }
}
