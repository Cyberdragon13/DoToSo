using System;
using System.Collections.Generic;
using System.Text;

namespace DoToSo
{
    public class player
    {
        public player()
        {
            Id = new Guid();
            AlreadyPlayedOpponentIds = new List<Guid>();
            HadBye = false;
            HasBeenPairedUp = false;
        }

        //Properties
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Wins { get; set; }

        public int Ties { get; set; }

        public int Looses { get; set; }

        public List<Guid> AlreadyPlayedOpponentIds { get; set; }

        public bool HadBye { get; set; }

        public decimal Score => Wins + 0.5m * Ties;

        public bool HasBeenPairedUp { get; set; }
    }
}
