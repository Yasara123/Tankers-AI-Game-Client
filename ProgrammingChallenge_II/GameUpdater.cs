using ProgrammingChallenge_II;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

using System.Threading;
using System.Runtime.CompilerServices;

namespace ProgrammingChallenge_II
{
    public class GameUpdater
    { 

        private TcpListener tcpListener;
        private Socket connection;
        private NetworkStream stm;
        private ASCIIEncoding ascii;
        private Communicator com;


        private Map map;

        private String initialString;
        private String startString;
        private String gameString;
        private String coinPileString;
        private String medipackString;
        private Thread stear;
        private Thread Attack;
        Player_Solution solution;
        private int switcher;

        public GameUpdater(){
            ascii = new ASCIIEncoding();
            tcpListener = new TcpListener(IPAddress.Parse("127.0.0.1"),7000);
            //tcpListener = new TcpListener(IPAddress.Parse("10.10.4.90"), 7000);
            tcpListener.Start();
            stear = new Thread(this.Stear);
            Attack = new Thread(this.Attack_Thread);
            switcher = 0;
                                   
        }
       [MethodImpl(MethodImplOptions.Synchronized)]
        public void update() {
            
            
            int s = 0;  // s=1  initial message Recived
            // s=2  start message Recived..
                  

            while (true)
            {
                connection = tcpListener.AcceptSocket();
                if (connection.Connected)
                {
                    stm = new NetworkStream(connection);
                    byte[] buffer = new byte[5000];
                    int k;
                    try
                    {
                        k = stm.Read(buffer, 0, buffer.Length);
                    }
                    catch (IOException e) {
                        Console.WriteLine("Remote Host  Closed ");
                        continue;
                    }
                            
                    String message = ascii.GetString(buffer, 0, k);
                    char value;
                    if (!message.Equals(""))
                    {
                        value = message.ToCharArray()[0];
                    }
                    else {
                        value = 'U';
                     }

                    if (message.Equals("CELL_OCCUPIED#")) {

                        Console.WriteLine("CELL OCCUPIED....");
                        com = new Communicator();
                        com.sendMessage_ToServer("SHOOT#");
                        continue;

                    }

                    switch (value) {

                        case 'I': this.initialString = message;
                                  s++; 
                                  break;

                        case 'S': this.startString = message;
                                  s++;
                                  if (s == 2) {
                                      map = new Map(initialString, startString);
                                      solution= new Player_Solution(map);
                                  }
                                  break;

                        case 'G': this.gameString = message;
                                  map.MapUpdate(message);
                                  if (switcher <= 10)
                                  {
                                      solution.stear();
                                      switcher++;
                                      Console.WriteLine("Stear MODE");
                                  }
                                  else if (switcher > 10 && switcher <= 20)
                                  {
                                      bool shoot = solution.attack();
                                      switcher++;
                                      Console.WriteLine("Attack MODE");
                                      if (shoot == false)
                                      {
                                          switcher = 1010;
                                          Console.WriteLine("SWITCHED STEAR MODE");
                                      }

                                  }
                                  else
                                  {
                                      switcher = 0;
                                  }
                                 break;

                        case 'C': this.coinPileString = message;
                                  map.ItemUpdate(message);
                                  break;

                        case 'L': this.medipackString = message;
                                  map.ItemUpdate(message);
                                  break;

                        default: Console.WriteLine("Unknown Message.");
                                  break;   
                                                   
                    }
                 
                    Console.WriteLine(message);
        
                
                
                }
                else {
                    Console.WriteLine("Internet fail.. ");
                    continue;
                }
                     
            
            }
        
        }


  
        private void Stear() {
             while (true) {

                solution.stear();
                Thread.Sleep(1500);
              }
       
        }

        
        private void Attack_Thread() {

            while (true) {

                solution.attack();
                Thread.Sleep(500);
            }
        
        }
       


      
     
    }
}
