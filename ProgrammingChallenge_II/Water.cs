using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingChallenge_II
{
    public class Water
    {
        private char index;  // will be 'W' for Water Cell
        private Coordinate coordinates;
        private int damage;

        public Water( char index,int x,int y) {
            coordinates = new Coordinate(x, y);
            this.index = index;

        }

        public void setDamage(int damage) { this.damage = damage; }



    }
}
