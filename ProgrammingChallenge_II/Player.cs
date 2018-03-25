using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingChallenge_II
{
   public  class Player
    {

        private String player_index;  // Player index
        private Coordinate coordiates;  // To store the location of the palyer
        private int health;
        private int coins;
        private int points;
        private bool shot_state;   // True if player is shot otherwise false;
        private int direction;  // 0-North ,1-East, 2-South ,3-West
        private bool alive;
        public Player(String player_index, int x,int y,int direction, int health) {

            this.player_index = player_index;
            coordiates = new Coordinate(x,y);
            this.direction = direction;
            shot_state = false;
            this.health = health;
            alive = true;

        }
        public bool isAlive() {
            return alive;
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void updatePlayer(int x, int y, int direction,int shot, int health,int coins, int points) {

            coordiates.setCoordinates(x, y);
            this.direction = direction;
            this.health = health;
            if (shot == 0)
            {
                shot_state = false;
            }
            else {
                shot_state = true;
            }
            if (health == 0) {
                alive = false;
            }
            this.coins = coins;
            this.points = points; 


        }


        public String getPlayerIndex()
        {
            return player_index;
        }


        public Coordinate getCoordinates() { return coordiates; }

        public int getDirection() { return direction; }

        public int getHealth() { return health; }

        
    }
}
