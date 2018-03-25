using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProgrammingChallenge_II
{
  public  class Map
    {
        private int mapSize;
        private int numberOfBrickCells;
        private int numbberOfStoneCells;
        private int numebrOfWataterCells;
        private int numberOfPlayers;
        private int indexOfHomePlayer;

        private Obstacle[] brick_cell;
        private Obstacle[] stone_cell;
        private Water[] water_cell;
        private Player[] player;
        private List<Item> coinPiles;
        private List<Item> lifePacks;


        private String initialMessage;

        private String [,] map;
       

        public int getNumberOfAliveCoinPilies() {

            int ans = 0;
            for (int i = 0; i < coinPiles.Count; i++) {
                if (coinPiles.ElementAt(i).isAlive()) {
                    ans++;
                }
            
            }

            return ans;
        }

        public int getNumberOfAliveLifePacks() {

            int ans = 0;
            for (int i = 0; i < this.lifePacks.Count; i++) {
                if (lifePacks.ElementAt(i).isAlive()) {
                    ans++;
                }
            }
            return ans;
        }

        public List<Item> getCoinPiles() {
            return this.coinPiles;
        }
        public List<Item> getLifepacks() {
            return this.lifePacks;
        }
        public int getIndexOfHomePlayer() {

            return this.indexOfHomePlayer;       
        }

        public String getCell(int x, int y) {
            return map[x, y];
        }

        public Player getPlayerByIndex(int index) {
            return player[index];
        }

        public int getMapSize() {
            return this.mapSize;
        }

        public Player[] getAllPlayers() {
            return player;
        }


       
        public  Map(String initialMessage,String startString) {

            this.mapSize = 20;
            map = new String[20, 20];
            this.initialMessage = initialMessage;

            this.numbberOfStoneCells = 0;
            this.numberOfBrickCells = 0;
            this.numebrOfWataterCells = 0;
            this.numberOfPlayers = 0;

            this.coinPiles = new List<Item>();
            this.lifePacks = new List<Item>();

            for (int i = 0; i < mapSize; i++) {
                for (int j = 0; j < mapSize; j++) {

                    map[i, j] = "Empty";
                }
            
            }


            if (this.processInitialString(initialMessage) & this.processStartString(startString))
            {
                Console.WriteLine("Map Initialization Sucessfull");
              // Thread display = new Thread(this.displayMap);
             //   display.Start();
            }
            else {
                Console.WriteLine("Error Occured While initializing the Map");
            }

        
        }
       
        private bool processInitialString(String str) {

            if (str.IndexOf("#") != str.Length - 1) {
                return false;
            }
            str = str.Substring(0, str.Length - 1);

            String[] StrSplit = str.Split(':');
            String[] bricks;
            String[] stones;
            String[] water;

            if (StrSplit[0].Equals("I"))
            {
                indexOfHomePlayer = Convert.ToInt32(StrSplit[1].Substring(1, StrSplit[1].Length-1));

                bricks = StrSplit[2].Split(';'); 
                stones = StrSplit[3].Split(';');
                water = StrSplit[4].Split(';');

                
                
                this.numberOfBrickCells = bricks.Length;
                this.numbberOfStoneCells = stones.Length;
                this.numebrOfWataterCells = water.Length;

                this.brick_cell = new Obstacle[numberOfBrickCells];
                this.stone_cell = new Obstacle[numbberOfStoneCells];
                this.water_cell = new Water[numebrOfWataterCells];

                int x = 0, y = 0;

                for (int i = 0; i < brick_cell.Length; i++) {

                    x = Convert.ToInt32(bricks[i].Split(',')[0].ToString());
                    y = Convert.ToInt32(bricks[i].Split(',')[1].ToString());

                    map[x, y] = "B_" + i;
                    brick_cell[i] = new Obstacle('B',x, y, 0);

                }

                x = 0; y = 0;
                for (int i = 0; i < stone_cell.Length; i++) {

                    x = Convert.ToInt32(stones[i].Split(',')[0].ToString());
                    y = Convert.ToInt32(stones[i].Split(',')[1].ToString());

                    map[x, y] = "S_" + i;
                    stone_cell[i] = new Obstacle('S', x, y, 0);



                }

                x = 0; y = 0;
                for (int i = 0; i < water_cell.Length; i++) {
                    x = Convert.ToInt32(water[i].Split(',')[0].ToString());
                    y = Convert.ToInt32(water[i].Split(',')[1].ToString());

                    map[x, y] = "W_" + i;
                    water_cell[i] = new Water('W', x, y);

                
                
                }

                return true;


            }
            else {
                return false;
            }

        }
       
        private bool processStartString(String str) {

            if (str.IndexOf("#") != str.Length - 1)
            {
                return false;
            }
            str = str.Substring(0, str.Length - 1);

            String[] Str_Split = str.Split(':');

            if (Str_Split[0].Equals("S"))
            {
                
                this.numberOfPlayers = Str_Split.Length-1;
                player = new Player[numberOfPlayers];
                int x = 0, y = 0 ,direction=0;
                String [] temp;
                for (int i = 0; i < player.Length; i++) {

                    temp = Str_Split[i + 1].Split(';');
                    x = Convert.ToInt32(temp[1].Split(',')[0]);
                    y = Convert.ToInt32(temp[1].Split(',')[1]);
                    map[x, y] = "P_" + i;
                    direction = Convert.ToInt32(temp[2]);
                    player[i] = new Player("P" + i, x, y, direction, 100);
                    
                }

                return true;
            }
            else {
                return false;
            }


        }
     
        public bool MapUpdate(String str) {

            if (str.IndexOf("#") != str.Length - 1)
            {
                Console.WriteLine("Map Update Failed....!!!!!");
                return false;
            }
            str = str.Substring(0, str.Length - 1);

            String[] Str_Split = str.Split(':');

            if (Str_Split[0].Equals("G"))
            {

                String[] temp;
                int x, y, direction,shot, health, coins, points ,damage;
                for (int i = 1; i <= this.numberOfPlayers; i++) {
                    temp = Str_Split[i].Split(';');
                    x = Convert.ToInt32(temp[1].Split(',')[0]);
                    y = Convert.ToInt32(temp[1].Split(',')[1]);
                    direction = Convert.ToInt32(temp[2]);
                    shot = Convert.ToInt32(temp[3]);
                    health = Convert.ToInt32(temp[4]);
                    coins = Convert.ToInt32(temp[5]);
                    points = Convert.ToInt32(temp[6]);
                    int x_previous=this.player[i-1].getCoordinates().getXCoordinate();
                    int y_previous = this.player[i - 1].getCoordinates().getYCoordinate();

                                                
                    if (x == x_previous && y == y_previous)
                    {
                        this.map[x, y] = "P" + (i - 1);
                    }
                    else
                    {
                        if (map[x_previous, y_previous].Equals("P" + (i - 1)))
                        {
                            if (map[x, y].Split('_')[0].Equals("C")) {

                                coinPiles.ElementAt(Convert.ToInt32(map[x, y].Split('_')[1])).Taken = true;
                            }
                            else if (map[x, y].Split('_')[0].Equals("L")) {

                                lifePacks.ElementAt(Convert.ToInt32(map[x, y].Split('_')[1])).Taken = true;
                            }
                            
                            map[x_previous, y_previous] = "Empty";
                            map[x, y] = "P" + (i - 1);
                        }else{
                            Console.WriteLine("Player Upate failed...");
                        }
                    }
                    this.player[i - 1].updatePlayer(x,y,direction,shot,health,coins,points);
                

                }

                temp = Str_Split[this.numberOfPlayers + 1].Split(';');
                String item_index;
                int index;
                for (int i = 0; i < temp.Length; i++) {

                    x = Convert.ToInt32(temp[i].Split(',')[0]);
                    y = Convert.ToInt32(temp[i].Split(',')[1]);
                    damage=Convert.ToInt32(temp[i].Split(',')[2]);

                    item_index = this.map[x, y];
                    if (!item_index.Equals("Empty")) {

                        index=Convert.ToInt32(item_index.Split('_')[1]);
                        if (item_index.Split('_')[0].Equals("B")) {

                            this.brick_cell[index].setDamage(damage);
                        }
                        else if (item_index.Split('_')[0].Equals("S")) {
                            this.stone_cell[index].setDamage(damage);
                        }
                        else if (item_index.Split('_')[0].Equals("W")) {

                            this.water_cell[index].setDamage(damage);
                        }

                    }
                        

                
                }
                Console.WriteLine("Map Update Successfull....");
                return true;

            }
            else {
                Console.WriteLine("Map Update Failed.......!!!!!");
                return false;
            }



        
        }
     
        public bool ItemUpdate(String str) {

            if (str.IndexOf("#") != str.Length - 1)
            {
                return false;
            }
            
            str = str.Substring(0, str.Length - 1);
            int x, y, ttl, val;
            String[] Str_Split = str.Split(':');
            Item temp;
            if (Str_Split[0].Equals("C"))
            {

                x = Convert.ToInt32(Str_Split[1].Split(',')[0]);
                y = Convert.ToInt32(Str_Split[1].Split(',')[1]);
                ttl = Convert.ToInt32(Str_Split[2]);
                val = Convert.ToInt32(Str_Split[3]);
                if (!(map[x, y].Split('_')[0].Equals("B")) && !(map[x, y].Split('_')[0].Equals("S")))
                {
                    map[x, y] = "C_" + coinPiles.Count;
                }
                temp = new Item("C",x, y, ttl, val);
                coinPiles.Add(temp);
                


            }
            else if (Str_Split[0].Equals("L")) {


                x = Convert.ToInt32(Str_Split[1].Split(',')[0]);
                y = Convert.ToInt32(Str_Split[1].Split(',')[1]);
                ttl = Convert.ToInt32(Str_Split[2]);
                if (!(map[x, y].Split('_')[0].Equals("B")) && !(map[x, y].Split('_')[0].Equals("S")))
                {

                    map[x, y] = "L_" + lifePacks.Count;
                }
                temp = new Item("L", x, y, ttl,0);
                lifePacks.Add(temp); 

            }

            return true;

        }


        public void displayMap() {
           
            while (true)
            {
                Console.WriteLine("New Window");
                for (int i = 0; i < this.mapSize; i++)
                {
                    for (int j = 0; j <this.mapSize; j++)
                    {
                        Console.Write(map[i, j] + "-");
                    }
                    Console.WriteLine("\n----------------------------------");
                }
                Console.WriteLine("\n*****************************************************************************\n");
            }
            Thread.Sleep(500);

        }


    }
}
