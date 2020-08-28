using System;
using System.Collections.Generic;
using System.Text;

namespace DoToSo
{
    public class SettingUpPlayerlist
    {
        private string UserInput;
        public List<Player> Command()
        {
            List<Player> playerList = new List<Player>();

            do
            {
                UserInput = Console.ReadLine();
                Insert InsertClass = new Insert();
                InsertClass.InsertPlayer(UserInput, playerList);
                
                if (UserInput.ToLower() != "start")
                {   
                    Console.WriteLine("enter next Player, 'list' to list all enterd players or 'start' to generate a match");
                }            
                    
            } while (UserInput.ToLower() != "start");
            return playerList;
        }
    }
}
