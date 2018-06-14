using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureGame
{
    class Program
    {

        static void Main(string[] args)
        {

            /*
            Console.WriteLine("Welcome. Here you will adventure through the fantasy land of Middle Earth.");
            // Add more backstory to intrigue the gamer (will take place in a castle)

            Console.WriteLine("What race would you like to be?");
            Console.WriteLine("You may choose between Man, Dwarf, Hobbit, or Elf.");
            myCharacter.race = Console.ReadLine();

            Console.WriteLine("Next you will choose which class you would like to be.");
            Console.WriteLine("Each class has its own advantages and disadvantages.");
            Console.WriteLine("A warrior has ");
            Console.WriteLine("An archer has ");
            Console.WriteLine("A spy has ");
            Console.WriteLine("A lore-master has ");
            Console.WriteLine("Which would you like to choose?");
            myCharacter.playerClass = Console.ReadLine();

            Console.WriteLine("What would you like to name your character?");
            myCharacter.name = Console.ReadLine();

            Console.WriteLine("You are a " + myCharacter.race + " " + myCharacter.playerClass + " named " + myCharacter.name);
            */
            Game myGame = new Game();            

            Console.ReadKey();
        }

        private static void help()
        {
            Console.WriteLine("See action commands by typing: \"list action commands\"");
            Console.WriteLine("See combat commands by typing \"list combat commands\"");
        }

        private static void listActionCommands()
        {
            Console.WriteLine("Your passive commands are: ");
            Console.WriteLine(" Look around");
            Console.WriteLine(" Check inventory");
            Console.WriteLine(" Check hit points");
            Console.WriteLine(" Use [item]");
            Console.WriteLine(" Equip [item]");
            Console.WriteLine(" Go to [object/person]");
            Console.WriteLine(" Pick up [item]");
            Console.WriteLine(" Engage [monster] with [weapon]");
            Console.WriteLine(" Move [direction]");
        }

        private static void listCombatCommands()
        {
            Console.WriteLine("Melee: ");
            Console.WriteLine("Ranged: ");
        }
    }

    class Game
    {
        public Character myCharacter = new Character();

        public Game()
        {
            Tutorial tutorial = new Tutorial();
            tutorial.createTutorialRegion();
            tutorial.doTutorial(myCharacter);
        }
    }

    class Character
    {
        public string name;
        public string race;

        public Item[] inventory = {};

        public int charXCoord;
        public int charYCoord;
        public string inRegionName;

        public void moveNorth()
        {
            this.charYCoord += 1;
        }

        public void moveEast()
        {
            this.charXCoord += 1;
        }

        public void moveSouth()
        {
            this.charYCoord -= 1;
        }

        public void moveWest()
        {
            this.charXCoord -= 1;
        }

        public void setCoord(int x, int y)
        {
            this.charXCoord = x;
            this.charYCoord = y;
        }
    }

    // For expansions
    class World
    {
            
    }

    // Each individual region that the player can explore
    class Region
    {
        public string name;
        public int width { get; private set; }
        public int height { get; private set; }
        //private Room[,] rooms = new Room[5, 5];

        public static void checkRoom(char a, int b)
        {

        }

        public static void createRegion(int w, int h)
        {
            Room[,] roomsArray = new Room[w, h];
            for(int i=0; i<w; i++)
            {
                for(int j=0; j<h; j++)
                {
                    //roomsArray[i][j]
                }
            }

            // rooms = roomsArray.Clone(); FIX
        }
    }

    class Tutorial : Region
    {
        private Room[,] rooms = new Room[2, 2];
        
        public void createTutorialRegion()
        {
            for(int i=0; i<2; i++)
            {
                for(int j=0; j<2; j++)
                {
                    rooms[i, j] = new Room();
                }
            }
            // Creates connections between rooms
            rooms[0, 0].setRoomConnections(true, false, false, false);
            rooms[0, 1].setRoomConnections(false, true, true, false);
            rooms[1, 0].setRoomConnections(true, false, false, false);
            rooms[1, 1].setRoomConnections(false, false, true, true);
            // Adds objects to rooms
            Object cabinet = new Object("cabinet", 0, 0);
            rooms[0, 0].addObject(cabinet);
        }
        
        public Character doTutorial(Character character)
        {
            //Console.WriteLine("You are starting in a dark room. 'Look around' to see what you can find.");
            bool tutorialFinished = false;
            string playerInput;
            character.setCoord(0, 0);
            Console.WriteLine("Type 'check exits' to see the doors in the room.");
            while (tutorialFinished == false)
            {
                playerInput = Console.ReadLine();
                character = checkCommand(playerInput, character, rooms[character.charXCoord, character.charYCoord]);
            }

            return character;
        }

        private Character checkCommand(string input, Character character, Room room)
        {
            if(input.Equals("check exits"))
            {
                checkDoors(character.charXCoord, character.charYCoord);
            }
            else if(input.Equals("go north"))
            {
                if(character.charYCoord < 2 && room.isNorth() == true)
                {
                    character.moveNorth();
                    Console.WriteLine("You go through the northern doorway.");
                }
                else
                {
                    Console.WriteLine("You can't go north.");
                }
            }
            else if(input.Equals("go east"))
            {
                if(character.charXCoord < 2 && room.isEast() == true)
                {
                    character.moveEast();
                    Console.WriteLine("You go through the eastern doorway.");
                }
                else
                {
                    Console.WriteLine("You can't go east.");
                }
            }
            else if(input.Equals("go south"))
            {
                if(character.charYCoord != 0 && room.isSouth() == true)
                {
                    character.moveSouth();
                    Console.WriteLine("You go through the southern doorway.");
                }
                else
                {
                    Console.WriteLine("You can't go south.");
                }
            }
            else if(input.Equals("go west"))
            {
                if(character.charXCoord != 0 && room.isWest() == true)
                {
                    character.moveWest();
                    Console.WriteLine("You go through the western doorway.");
                }
                else
                {
                    Console.WriteLine("You can't go west.");
                }
            }
            else if(input.Equals("look around"))
            {
                
            }
            else
            {
                Console.WriteLine("Please enter a valid command.");
            }

            return character;
        }

        public void checkDoors(int roomX, int roomY)
        {
            Console.WriteLine("There are doors to the: ");
            if (rooms[roomX, roomY].isNorth())
            {
                Console.WriteLine(" - north");
            }
            if(rooms[roomX, roomY].isEast())
            {
                Console.WriteLine(" - east");
            }
            if(rooms[roomX, roomY].isSouth())
            {
                Console.WriteLine(" - south");
            }
            if(rooms[roomX, roomY].isWest())
            {
                Console.WriteLine(" - west");
            }

        }
        
    }
        
    // Items can be picked up by the player
    class Item
    {
        private string itemName;
        public bool isEquipable = false;

    }

    // Objects cannot be picked up by the player. Items can be set on top of or conceiled in an Object
    class Object
    {
        public string objectName;
        private string itemNameOn;
        private string itemNameIn;
        public int objXCoord;
        public int objYCoord;

        private Item[] containerList = new Item[5];
        
        public Object(string name, int xCoord, int yCoord)
        {
            this.objectName = name;
            this.objXCoord = xCoord;
            this.objYCoord = yCoord;
        }
    }

    class Room
    {
        private int MAX_OBJECTS = 3;
        private int MAX_ITEMS = 2;
        private int MAX_MONSTERS = 3;

        private List<Object> objectsInRoom = new List<Object>();
        private List<Item> itemsInRoom = new List<Item>();
        private string inRegion;
        public int roomCoordX;
        public int roomCoordY;

        private bool[] roomConnections = new bool[4] {false, false, false, false};

        public void setRoomConnections(bool a, bool b, bool c, bool d)
        {
            roomConnections[0] = a;
            roomConnections[1] = b;
            roomConnections[2] = c;
            roomConnections[3] = d;
        }

        public bool isNorth()
        {
            if(roomConnections[0] == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool isEast()
        {
            if (roomConnections[1] == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool isSouth()
        {
            if (roomConnections[2] == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool isWest()
        {
            if (roomConnections[3] == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void addObject(Object name)
        {
            if (objectsInRoom.Count - 1 < MAX_OBJECTS)
            {
                objectsInRoom.Add(name);
            }
        }
        public void addItem(Item name)
        {
            if (itemsInRoom.Count - 1 < MAX_OBJECTS)
            {
                itemsInRoom.Add(name);
            }
        }

    }
}
