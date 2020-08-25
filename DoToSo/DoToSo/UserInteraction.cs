using System;
using System.Collections.Generic;
using System.Text;

namespace DoToSo
{
    public class UserInteraction
    {

        private bool ExitApplication = false;

        public void ExecuteTournamentPlan()
        {
            StartupMessage();
            SettingUpPlayerlist PlayerListSetup = new SettingUpPlayerlist();
            ApplicationShutDown ShutdownComand = new ApplicationShutDown();
            GameComands comands = new GameComands();

    
            List <Player> playerList= PlayerListSetup.Command();

            playerList = comands.GameManagement(playerList);                       
        }


        void StartupMessage()
        {
            Console.WriteLine("Dominion Tournament Software");
            Console.WriteLine("Enter a new playername:");
            return;
        }
    }    
}
