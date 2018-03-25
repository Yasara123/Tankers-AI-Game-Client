using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ProgrammingChallenge_II
{
    class Program
    {
        static void Main(string[] args)
        {
      
            Communicator com = new Communicator();
            if (com.sendMessage_ToServer("JOIN#")) {
                 GameUpdater updater = new GameUpdater();
                 Thread update = new Thread(updater.update);
                 update.Start();
            }
          

           
            

        }
    }
}
