using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingChallenge_II
{
    public class Coordinate
    {
        private int x;
        private int y;
        
        public Coordinate(int x, int y) {

            this.x = x;
            this.y = y;
        }

                
        public void setCoordinates(int x, int y) {
            this.x = x;
            this.y = y;
        }

        public int getXCoordinate() {
            return x;
        }

        public int getYCoordinate() {
            return y;
        }

        public String getCoordinatesAsAString() {
            return x + "," + y;

        }

    }
}
