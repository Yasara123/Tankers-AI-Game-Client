using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.CompilerServices;

namespace ProgrammingChallenge_II
{
   public class Item
    {
        private String index; // L for life pack and C for coin pile
        private bool alive; // True indicates that life pack is available false indicate that the life pack is vanished.
        private bool taken; // true if someone took the life pack.
        private Coordinate coordinate;
        private int time_to_live;
        private DateTime start;
        private Thread timer;
        private int value;  // Zero for the Life Packs;


        public Item(String index, int x, int y, int ttl,int value) {

            start=DateTime.Now;
            this.index = index;
            alive = true;
            taken = false;
            this.value = value;
            coordinate = new Coordinate(x, y);
            this.time_to_live = ttl;
            timer = new Thread(this.Timer);
            timer.Start();
            

        }


        public Coordinate getCoordinates() {
            return this.coordinate;
        } 

        public bool isAlive() {
            return this.alive;
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        private void Timer() {
            
            bool state = false;
            DateTime current = DateTime.Now;
            TimeSpan temp = (current - start);
            
            while (!state) {

                current = DateTime.Now;
                temp = current - start;
                if (Convert.ToInt32(temp.TotalMilliseconds) >= this.time_to_live) {
                    state = true;

                }

                if (taken == true) {
                    break;
                }
                
            }
           
            this.alive = false;
          
            
        }

        public bool Taken {

            get { return taken; }
            set { taken = value; }
        
        }
        
        


    }
}
