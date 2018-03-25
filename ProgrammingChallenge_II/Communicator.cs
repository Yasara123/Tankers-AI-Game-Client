using ProgrammingChallenge_II;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Configuration.Assemblies;
using System.Runtime.CompilerServices;




namespace ProgrammingChallenge_II
{
    public class Communicator{

        private TcpClient tcpClient;
        private Stream stm;
        private ASCIIEncoding ascii;
        
        public Communicator() {
            tcpClient = new TcpClient();
            ascii = new ASCIIEncoding();
        }
        
        public bool sendMessage_ToServer(String message) {
            bool state = false;
            try
            {
                tcpClient = new TcpClient();
                tcpClient.Connect(IPAddress.Parse("127.0.0.1"), 6000);
               // tcpClient.Connect(IPAddress.Parse("10.8.106.49"), 6000);               
                if (tcpClient.Connected)
                {
                    stm = tcpClient.GetStream();
                    stm.Flush();
                    byte[] buffer = ascii.GetBytes(message);
                    stm.Write(buffer, 0, buffer.Length);
                    stm.Close();
                    tcpClient.Close();
                    state = true;
                }
                else
                {
                    Console.WriteLine("Server is unreachable.");
                    state = false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Message Sending Failed. \n" + e.StackTrace);

            }
            finally {
                tcpClient.Close();
            }

            return state;
        }

         



    }
}
