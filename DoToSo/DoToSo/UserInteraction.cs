using System;
using System.Collections.Generic;
using System.Text;

namespace DoToSo
{
    public class UserInteraction
    {

        List<player> playerList = new List<player>();
        private string input;
        private bool TournamentStartet = false;
        private bool TournamentEndet = false;
        private bool ExitApplication = false;

        public void ExecuteTournamentPlan()
        {
            StartupMessage();
            PreGameComands PreComands = new PreGameComands();
            ApplicationShutDown ShutdownComand = new ApplicationShutDown();
            

            do
            {
                input = Console.ReadLine();

                (TournamentStartet,playerList)= PreComands.Comand(TournamentStartet, playerList, input);


                
                if (TournamentEndet == false)
                {

                }
                else
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
