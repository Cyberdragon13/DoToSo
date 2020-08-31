using System;
using System.Collections.Generic;
using System.Text;

namespace DoToSo
{
    public class SetUpPlayers
    {
        private string UserInput;
        public List<Player> Insert()
        {
            List<Player> playerList = new List<Player>();

            do
            {
                UserInput = Console.ReadLine();
                Insert InsertClass = new Insert();
                InsertClass.InsertPlayer(UserInput, playerList);
                
                if (UserInput.ToLower() != "start")
                {   
                    Console.WriteLine("enter next Player, 'list' to list all entered players or 'start' to generate a match");
                }            
                    
            } while (UserInput.ToLower() != "start");

            return playerList;
        }
    }
}
