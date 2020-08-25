using System;
using System.Collections.Generic;
using System.Text;

namespace DoToSo
{
    class SettingUpPlayerlist
    {
        public List<Player> Command()
        {
            List<Player> playerList = new List<Player>();
            string input;
            do
            {               
                    input = Console.ReadLine();
                    Insert InsertClass = new Insert();
                    playerList = InsertClass.InsertPlayer(input, playerList);

                    if (input.ToLower() != "start")
                    {
                        Console.WriteLine("enter next Player, 'list' to list all enterd players or 'start' to generate a match");
                    }            
                    
            } while (input.ToLower() != "start");
            return playerList;
        }
    }
}
