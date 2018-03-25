using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingChallenge_II
{
    public class Obstacle
    {
        private char index;  // Will be 'B' for brick cell.. S for Stones
        private Coordinate coordinate;
        private int damage;

        public Obstacle(char index,int x, int y, int damage) {
            coordinate = new Coordinate(x, y);
            this.damage = damage;
            this.index = index;
        }

        public Coordinate getCoordinates() { return this.coordinate; }

        public int getDamage() { return this.damage; }

        public void setDamage(int damage) { this.damage = damage; }
        public char getIndex() { return this.index; } 
        
    }
}
