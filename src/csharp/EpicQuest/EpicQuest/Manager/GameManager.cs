using EpicQuest.GameEnums;
using EpicQuest.Interfaces;
using EpicQuest.Models;
using EpicQuest.Models.Dungeon;
using EpicQuest.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace EpicQuest.Manager
{
    /// <summary>
    /// Epic Quest core GameManager class
    /// that handles general game state.
    /// </summary>
    public sealed class GameManager
    {
        #region Private Data Fields

        //Private List representing the party members involved with this adventure
        private List<Hero> partyMembers = new List<Hero>();

        private EpicDungeon gameDungeon;

        #endregion Private Data Fields

        #region Main Game Loop Method

        /// <summary>
        /// Main Game Loop 'Start' method
        /// called from the Main entry point method of the 
        /// Program class (for this executable).
        /// </summary>
        public void RunMainGameLoop()
        {
            //RunTestBedMethod();

            //Debug information (method entered)
            Debug.WriteLine(Utils.MethodDebugMsg());

            //Game startup initialisation logic
            GameStartup();

            //Always run the game loop at least once (currently, break when user types EXIT)
            do
            {
                //Start game message
                Console.WriteLine();
                Console.Write("Do you want to venture into the dungeon? Type EXIT and hit ENTER to flee in terror or simply press ENTER to continue: ");

                //Does the user want to quit...
                if (Utils.InterpretUserInput(Console.ReadLine()) == GameAction.ExitGame)
                {
                    break;
                }

                //Clear the current game state (i.e. Party Members)
                ClearGameState();

                //Create a new Party of heroes
                CreateQuestParty();

                //
                CreateDungeon();

                HandleFullDungeonDelve();
            } 
            while (true);

            //RunMainGameLoop method exit (confirmed via the output window)
            Debug.WriteLine(Utils.MethodDebugMsg(DebugMsgType.MethodLeft));
        }

        private void HandleFullDungeonDelve()
        {
            Dictionary<KeyValuePair<HeroDefensiveItemType, HeroDefensiveItemSlotType>, int> dictionaryTest = Armour.ArmourMappings;

            StaticGameActions.HandleCombat(new Brawler(), new Skeleton());

            /*
                A Turn
                -----------------------

                1) Next room is revealed
                2) Monsters in the room are listed
                3) Each hero gets a chance to attack the monsters
                - All heroes defeated = End of game
                4) Remaining monsters get to attack the heroes
                5) If no monsters remain treasures are revealed
                6) Treasures can be allocated to any hero
                7) Next room is revealed - If all rooms have been processed the dungeon is defeated and the player win 
             
             */
            Console.WriteLine();

            //Heroes enter each room in the dungeon
            gameDungeon.DungeonRooms.ForEach(room =>
                {
                    HandleRoomCombatPhase(room);

                    //All monsters dead - Handle treasure
                    HandleRoomCollectTreasurePhase(room);

                    Console.WriteLine();
                    Console.WriteLine("Room Cleared!");
                });

            Console.WriteLine();
            Console.WriteLine("Dungeon Cleared!");
        }

        private void HandleRoomCombatPhase(Room room)
        {
            var monstersObjs = room.RoomFeatures.Where(feature => feature is Monster);
            List<Monster> monsterList = monstersObjs.ToList().ConvertAll(feature => (Monster)feature);

            do
            {
                HandleAttackMonsterPhase(monsterList);
                HandleAttackHeroPhase(room, monsterList);
            }
            while (!room.RoomClearedOfMonsters); //Heroes only progress when all monsters are dead
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="room"></param>
        private void HandleRoomCollectTreasurePhase(Room room)
        {
            
        }

        private void HandleAttackMonsterPhase(List<Monster> monsterList)
        {
            //Party members attack monsters
            partyMembers.ForEach(hero =>
            {
                monsterList.ForEach(monster =>
                {

                });
            });
        }

        private void HandleAttackHeroPhase(Room room, List<Monster> monsterList)
        {
            //Remaining monsters attack party members
            monsterList.ForEach(monster =>
            {
                partyMembers.ForEach(hero =>
                {

                });

                Console.WriteLine("{0} defeated!", monster.GetType().Name);
                room.RemoveContentFromRoom(monster as IRoomContent);
            });
        }

        #endregion Main Game Loop Method

        #region Private GameManager Helper Methods

        /// <summary>
        /// Private helper method that contains all of the Epic 
        /// Quest game initialisation logic (on start).
        /// </summary>
        private void GameStartup()
        {
            //Write out a formatted string (in a red text colour) that represents the Games Title
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(Utils.WriteGameTitle());

            //Reset the console to it's default colour scheme
            Console.ResetColor();
        }

        /// <summary>
        /// Private helper method that can be called to clear
        /// down the Epic Quest game state.
        /// </summary>
        private void ClearGameState()
        {
            partyMembers.Clear();
        }

        /// <summary>
        /// Private helper method (to be refactored/potentially moved) that
        /// allows the creation of a Party of heroes (currently allows 3 members).
        /// </summary>
        private void CreateQuestParty()
        {
            //Input help information - print to the console
            Console.WriteLine();
            Console.WriteLine("Create your Party (BRW = Brawler, CLR = Cleric, MAG = Mage, NEC = Necromancer, THF = Thief)...");

            //Loop three times and allow the creation of the relevant heroes
            for (int i = 0; i < 3; i++)
            {
                //Create a new hero (based on user selection)
                Console.WriteLine();
                Console.Write("Choose your hero number {0}: ", i);
                Hero newHero = null;

                switch (Utils.InterpretUserInput(Console.ReadLine()))
                {
                    case GameAction.ClericChosen:
                        {
                            newHero = new Cleric();
                        }
                        break;
                    case GameAction.MageChosen:
                        {
                            newHero = new Mage();
                        }
                        break;
                    case GameAction.NecromancerChosen:
                        {
                            newHero = new Necromancer();
                        }
                        break;
                    case GameAction.ThiefChosen:
                        {
                            newHero = new Thief();
                        }
                        break;
                    case GameAction.BrawlerChosen:
                    default:
                        {
                            newHero = new Brawler();
                        }
                        break;
                }

                //Safety check - Only add the hero if it's set correctly
                if (newHero != null)
                {
                    partyMembers.Add(newHero);
                    Console.WriteLine("{0} added to the Party.", newHero.GetType().Name);
                }
            }
        }

        private void CreateDungeon()
        {
            //TestDiceRoll();
            gameDungeon = new EpicDungeon(EpicDungeon.GenerateRandomRoomConfiguration());
        }

        #endregion Private GameManager Helper Methods

        //private void TestDiceRoll()
        //{
        //    Die oneAttackDie = new GreenDie();

        //    Random rollDie = new Random();
        //    DieFace face = oneAttackDie.DieFaces[rollDie.Next(0, 5)];

        //    string t = face.ToString();

        //    byte damage = (byte)face;

        //    string r = damage.ToString();

        //    //Quick test
        //    Brawler brawlerHero = (Brawler)partyMembers.First();

        //    byte totalDamage = 0;

        //    brawlerHero.HeroWeapon.Dice.ForEach(die =>
        //        {
        //            DieFace newFace = die.DieFaces[rollDie.Next(0, 5)];
        //            byte dieDamage = (byte)newFace;

        //            Console.WriteLine("{0} did {1} damage.", die.GetType().Name, dieDamage);

        //            totalDamage += dieDamage;
        //        });

        //    Console.WriteLine("Total Damage Rolled: {0}.", totalDamage);
        //}

        //private void RunTestBedMethod()
        //{
        //    List<IRoomContent> roomOneContentList = new List<IRoomContent>();
        //    roomOneContentList.Add(new Kobold());
        //    roomOneContentList.Add(new Skeleton());

        //    Room roomOne = new Room(roomOneContentList);

        //    Room roomTwo = new Room(new Kobold(), new Skeleton(), new Kobold());

        //    Kobold koboldInBothRooms = new Kobold();
        //    Skeleton skeleyInRoomOne = new Skeleton();

        //    roomOne.AddContentToRoom(koboldInBothRooms);
        //    roomTwo.AddContentToRoom(koboldInBothRooms, skeleyInRoomOne);

        //    Console.WriteLine("Room One Features...");
        //    roomOne.RoomFeatures.ForEach(ft => { Console.WriteLine(ft.GetType().Name); });

        //    Console.WriteLine("Room Two Features...");
        //    roomTwo.RoomFeatures.ForEach(ft => { Console.WriteLine(ft.GetType().Name); });

        //    roomOne.RemoveContentFromRoom(skeleyInRoomOne);
        //    roomTwo.RemoveContentFromRoom(skeleyInRoomOne);

        //    foreach (IRoomContent item in roomOne)
        //    {
        //        Console.WriteLine(item.GetType().Name);
        //    }

        //    EpicDungeon dungeonOne = new EpicDungeon(roomOne, roomTwo);
        //    EpicDungeon dungeonTwo = new EpicDungeon
        //        (
        //            new Room(new Kobold(), new Skeleton()),
        //            new Room(new Skeleton()),
        //            new Room(new Kobold(), new Skeleton())
        //        );

        //    foreach (Room room in dungeonOne)
        //    {

        //    }
        //}
    }
}