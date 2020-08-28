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
            PlayersInMatch = new List<Player>();
        }

        public Match(Player player1, Player player2)
        {
            PlayersInMatch = new List<Player>();
            PlayersInMatch.Add(player1);
            PlayersInMatch.Add(player2);
        }

        public bool IsForbidden => PlayersInMatch
                .SelectMany(players => players.AlreadyPlayedOpponentIds)
                .Intersect(PlayersInMatch.Select(player => player.Id))
                .Any();

        public List<Player> PlayersInMatch { get; set; }
        public int MatchNumber { get; set; }
        public bool MatchFinished { get; set; }

        public void AddPlayerToMatch(Player player)
        {
            PlayersInMatch.Add(player);
        }
    }
}
