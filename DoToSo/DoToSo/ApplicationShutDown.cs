using System;
using System.Collections.Generic;
using System.Text;

namespace DoToSo
{
    class ApplicationShutDown
    {
        private bool ExitApplication = false;
        public bool AskForShutdown(string userInteraction)
        {
            if (userInteraction == "exit" || userInteraction == "close")
            {
                do
                {
                    Console.WriteLine("Are you sure you you want to close the Application [Y/N]");
                    userInteraction = Console.ReadLine();

                    if (userInteraction == "Y")
                    {
                        ExitApplication = true;
                    }
                    else if (userInteraction == "N")
                    {
                        Console.WriteLine("Continuing the Tournament");
                    }
                    else
                    {
                        Console.WriteLine("Insert a valid comand if you want to exit the application [Y/N]");
                    }

                } while (userInteraction != "Y" && userInteraction != "N");

            }

            return ExitApplication;
        }


    }
}
