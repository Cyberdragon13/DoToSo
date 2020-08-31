using System;
using System.Collections.Generic;
using System.Text;

namespace DoToSo
{
    public class UserInteraction
    {
        public void ExecuteTournamentPlan()
        {
            StartupMessage();
            SetUpPlayers PlayerListSetup = new SetUpPlayers();
            GameCommands commands = new GameCommands();
    
            List<Player> playerList = PlayerListSetup.Insert();

            commands.GameManagement(playerList);                       
        }


        private void StartupMessage()
        {
            Console.WriteLine("Dominion Tournament Software");
            Console.WriteLine("Enter a new playername:");
        }
    }    
}
