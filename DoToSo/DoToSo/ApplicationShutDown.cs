using System;
using System.Collections.Generic;
using System.Text;

namespace DoToSo
{
    class ApplicationShutDown
    {
        private bool ExitApplication = false;
        public bool AskForShutdown(string input)
        {
            if (input == "exit" || input == "close")
            {
                do
                {
                    Console.WriteLine("Are you sure you you want to close the Application [Y/N]");
                    input = Console.ReadLine();

                    if (input == "Y")
                    {
                        ExitApplication = true;
                    }
                    else if (input == "N")
                    {
                        Console.WriteLine("Continuing the Tournament");
                    }
                    else
                    {
                        Console.WriteLine("Insert a valid comand if you want to exit the application [Y/N]");
                    }

                } while (input != "Y" && input != "N");

            }

            return ExitApplication;
        }


    }
}
