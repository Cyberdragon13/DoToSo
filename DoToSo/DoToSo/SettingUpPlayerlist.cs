using System;
using System.Collections.Generic;
using System.Text;

namespace DoToSo
{
    class SettingUpPlayerlist
    {
        private string UserInput;
        public List<Player> Command()
        {
            List<Player> playerList = new List<Player>();

            do
            {               
                    string input = Console.ReadLine();
                    Insert InsertClass = new Insert();
                    playerList = InsertClass.InsertPlayer(input, playerList);

                    if (input.ToLower() != "start")
                    {
                        Console.WriteLine("enter next Player, 'list' to list all enterd players or 'start' to generate a match");
                    }            
                    
            } while (UserInput.ToLower() != "start");
            return playerList;
        }
    }
}
