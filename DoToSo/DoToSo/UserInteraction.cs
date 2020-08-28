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
            SettingUpPlayerlist PlayerListSetup = new SettingUpPlayerlist();
            GameCommands comands = new GameCommands();
    
            List<Player> playerList = PlayerListSetup.Command();

            comands.GameManagement(playerList);                       
        }


        private void StartupMessage()
        {
            Console.WriteLine("Dominion Tournament Software");
            Console.WriteLine("Enter a new playername:");
        }
    }    
}
