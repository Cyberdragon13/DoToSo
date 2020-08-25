using System;
using System.Collections.Generic;
using System.Text;

namespace DoToSo
{
    public class UserInteraction
    {

        List<Player> playerList = new List<Player>();
        private string input;
        private bool TournamentStartet = false;
        private bool TournamentEndet = false;
        private bool ExitApplication = false;

        public void ExecuteTournamentPlan()
        {
            StartupMessage();
            PreGameComands PreComands = new PreGameComands();
            ApplicationShutDown ShutdownComand = new ApplicationShutDown();
            GameComands comands = new GameComands();

            do
            {
                input = Console.ReadLine();

                (TournamentStartet,playerList)= PreComands.Comand(TournamentStartet, playerList, input);

                (TournamentEndet, playerList) = comands.GameManagement(playerList, TournamentStartet, TournamentEndet, input);
                
                
                if(TournamentEndet == true)
                {

            
                }

                ExitApplication = ShutdownComand.AskForShutdown(input);
                
            } while (ExitApplication == false);
        }




        void StartupMessage()
        {
            Console.WriteLine("Dominion Tournament Software");
            Console.WriteLine("Enter a new playername:");
            return;
        }
    }    
}
