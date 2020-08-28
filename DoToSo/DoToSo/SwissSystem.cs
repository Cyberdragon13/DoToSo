using DoToSo.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DoToSo
{
    public class SwissSystem
    {
        public ShuffleListBc<Player> ShuffleListBc {get; set;}
        public Random Random { get; set; }

        public SwissSystem()
        {
            ShuffleListBc = new ShuffleListBc<Player>();
            Random = new Random();
        }

        public List<Match> GeneratePairing(List<Player> playerList, int preferedMatchsize)
        {
            List<Match> matches = new List<Match>();

            switch(preferedMatchsize)
            {
                case 2:
                    matches.AddRange(GeneratePairingsForTwoPlayerMatches(playerList));
                    break;
                default:
                    throw new BusinessException("The chosen matchsize is not legal");
            }

            return matches;
        }

        private List<Match> GeneratePairingsForTwoPlayerMatches(List<Player> allPlayers)
        {
            //need to have a new list, since we remove a player at assignment of bye from the list, but it should not be removed from the original list
            List<Player> players = new List<Player>(allPlayers);
            List<Match> generatedMatches = new List<Match>();

            if(allPlayers.Count % 2 == 1)
            {
                AssignBye(players);
            }

            Dictionary<decimal, List<Player>> playersByDescendingScore = GroupPlayersByDescendingScore(players);

            foreach(decimal score in playersByDescendingScore.Keys)
            {
                // quadratic or even cubic runtime in this loop will be ok 
                // since possible scores will probably never go over 100 and amount of players not over 5000 (this is already a really high estimation)

                List<Player> playerGroupToPair = playersByDescendingScore[score];

                if(playerGroupToPair.Count % 2 == 1)
                {
                    PairUpAPlayer(playersByDescendingScore, playerGroupToPair);
                }

                generatedMatches.AddRange(PairPlayers(playerGroupToPair));
            }

            return generatedMatches;
        }

        private List<Match> PairPlayers(List<Player> playersToPair)
        {
            ShuffleListBc.FisherYatesShuffle(playersToPair, Random);

            return RotateBeforeSplit(playersToPair, 0);
        }

        private List<Match> RotateWithinList(List<Player> playersToRotate, List<Player> otherPlayers, int rotationStartingIndex)
        {
            for(int i = rotationStartingIndex; i < playersToRotate.Count; i++)
            {
                List<Match> matches = CreateMatches(playersToRotate, otherPlayers);
                if (HavePlayersNotBeenPairedBefore(matches))
                {
                    return matches;
                }

                Player player1 = playersToRotate[rotationStartingIndex];
                Player player2 = playersToRotate[i];
                Swap(ref player1, ref player2);
                RotateWithinList(playersToRotate, otherPlayers, rotationStartingIndex + 1);
            }

            return null;
        }

        private List<Match> RotateBeforeSplit(List<Player> players, int rotationStartIndex)
        {
            for(int i = rotationStartIndex; i < players.Count; i++)
            {
                int halfAmountOfPlayers = players.Count / 2;
                List<Player> firstHalf = players.Take(halfAmountOfPlayers).ToList();
                List<Player> secondHalf = players.Skip(halfAmountOfPlayers).Take(halfAmountOfPlayers).ToList();

                List<Match> matches = RotateWithinList(firstHalf, secondHalf, 0);

                if (matches != null)
                {
                    return matches;
                }

                Player player1 = players[rotationStartIndex];
                Player player2 = players[i];
                Swap(ref player1, ref player2);
                RotateBeforeSplit(players, rotationStartIndex + 1);
            }

            return null;
        }

        private void Swap(ref Player player1, ref Player player2)
        {
            if (player1 == player2)
            {
                return;
            }

            Player temp = player1;
            player1 = player2;
            player2 = temp;
        }

        private bool HavePlayersNotBeenPairedBefore(List<Match> matches)
        {
            return matches.Any(match => match.IsForbidden == true) == false;
        }

        private List<Match> CreateMatches(List<Player> firstHalfOfPlayers, List<Player> secondHalfOfPlayers)
        {
            List<Match> matches = new List<Match>();

            for(int i = 0; i < firstHalfOfPlayers.Count; i++)
            {
                matches.Add(new Match(firstHalfOfPlayers[i], secondHalfOfPlayers[i]));
            }

            return matches;
        }

        private void PairUpAPlayer(Dictionary<decimal, List<Player>> playersByDescendingScore, List<Player> playerGroupToPair)
        {
            decimal score = playerGroupToPair.First().Score;
            decimal nextScore = playersByDescendingScore.Keys.First(value => value < score);
            Player pairedUpPlayer = playersByDescendingScore[nextScore].First(player => player.HasBeenPairedUp == false);
            pairedUpPlayer.HasBeenPairedUp = true;
            playerGroupToPair.Add(pairedUpPlayer);
        }

        private Dictionary<decimal, List<Player>> GroupPlayersByDescendingScore(List<Player> players)
        {
            return players
                .GroupBy(player => player.Score)
                .OrderByDescending(group => group.Key)
                .ToDictionary(group => group.Key, group => group.ToList());
        }

        private void AssignBye(List<Player> players)
        {
            Player playerWithBye = players.OrderBy(player => player.Score).First(player => player.HadBye == false);
            playerWithBye.Wins += 1;
            players.Remove(playerWithBye);
        }
    }
}
