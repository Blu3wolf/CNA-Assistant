using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNA_Assistant
{
	class Game : IGameCommands
	{
		/* The top level object that represents the entire Game model. A reference to the PlayerSide is the primary tool required by the ViewModel, to manipulate the game state.
		 * All units the user has, the current game turn, all of it. Saving the game represents essentially writing the state of the PlayerSide to disc. Loading the game, creating
		 * a new PlayerSide object with specified values from the save file. Selecting New Game means creating a new PlayerSide object, using pre-defined data (Scenario data). 
		 * Some features of a PlayerSide should be immutable. For example, the user cannot decide they are now Axis if they were Commonwealth. These properties should be get only, 
		 * and be set in the constructor. */

		public Game() // Eventually, this should take a SaveFile object as an argument, that contains the values to populate the object with.
		{
			// Initialises all the properties of a PlayerSide object.

			// Sets all immutable properties (Axis/CW).


		}

		// Fields (backing)

		private int gameTurn;

		private bool userHasInitiative;

		private TurnState turnState;

		// Properties

		public Side SideIs
		{ get;  }

		public Side EnemySide
		{ get; }

		public enum Side
		{ Axis, Commonwealth }

		public int GameTurn { get; internal set; }// Game Turn one through to one hundred eleven. 

		public int OpStage { get; internal set; } // Op Stage, zero to four - zero and four being before and after the Operations Stage of the turn. 

		public TurnState TurnState { get; internal set; } // current phase or step of the Sequence of Play - where in the turn we are up to, in other words. 

		public bool HasInitiative { get; internal set; }

		public bool GoesSecond { get; internal set; } // set at start of ops stage. Sounds like I want an ops stage object!

		public bool IsPhasing { get; internal set; }

		public DateTime GameDate // Finds the approximate date of the current GameTurn / OpStage combination and returns it.
		{
			get
			{
				// first date is 15 Sep, 1940... and each Game Turn is 1 week. So to get the Game Turn date, start with 8 Sep 40, and add weeks equal to the Game Turn. 
				DateTime initialdate = new DateTime(1940, 9, 15);
				// add a Time
				TimeSpan stageSpan = new TimeSpan(2, 13, 0, 0);
				// find the interval
				// Naturally, this requires disassembling the TimeSpan struct. Because of course.
				// This is equivalent to interval = stagespan * NumberOfOpStagesSinceGameStart, but TimeSpans cannot be multiplied...
				TimeSpan interval = TimeSpan.FromTicks(stageSpan.Ticks * (OpStage + GameTurn * 3 - 4));
				DateTime date = initialdate.Add(interval);
				return date;
				// This should be as inaccurate as the original developer was!
			}
		}

		public Season CurrentSeason
		{
			get
			{
				// seasons last exactly 12 turns (weeks). Turn 1 is start of Autumn, Winter starts on turn 13
				int seasonnumber = GameTurn / 12; // takes GameTurn, divides by 12, rounds down - so 1st season returns 0, 4th season returns 3, 5th season returns 4, etc
				seasonnumber %= 4; // divides the season number by 4, then sets it as the remainder - so 1st season returns 0, 4th season returns 3, 5th season returns 0, etc

				switch (seasonnumber)
				{
					case 0:
						return Season.Autumn;
					case 1:
						return Season.Winter;
					case 2:
						return Season.Spring;
					case 3:
						return Season.Summer;
					default:
						throw new Exception("Invalid CurrentSeason - is GameTurn negative?");
				}
			}
		}

		public enum Season
		{
			Spring, Summer, Autumn, Winter
		}

		public List<Unit> Units { get; private set; }

		public List<SupplyDump> SupplyDumps { get; private set; }

		// Methods

		public void NextPhase()
		{
			// call Game.TurnState.Next();
		}

		public int GetInitiativeRating(Side side)
		{
			if (side == Side.Axis)
			{
				if (false) // Rommel Present
				{
					return 6;
				}
				else if (false) // German Units Present
				{
					return 3;
				}
				else return 1;
			}
			else
			{
				if (GameTurn >= 91)
				{
					return 5;
				}
				else if (GameTurn >= 43)
				{
					return 4;
				}
				else return 3;
			}
		}

		public void DetermineInitiative(DiceRoll diceRoll, DiceRoll enemyRoll)
		{
			int roll = diceRoll.Result + GetInitiativeRating(SideIs);
			int oproll = enemyRoll.Result + GetInitiativeRating(EnemySide);

			while (roll == oproll)
			{
				diceRoll = new DiceRoll();
				enemyRoll = new DiceRoll();
				roll = diceRoll.Result + GetInitiativeRating(SideIs);
				oproll = enemyRoll.Result + GetInitiativeRating(EnemySide);
			}

			if (roll > oproll)
			{
				userHasInitiative = true;
			}
			else
			{
				userHasInitiative = false;
			}
		}

		public void DetermineInitiative(bool hasInitiative)
		{
			userHasInitiative = hasInitiative;
		}

		public int AxisGetNextTurnShippingLimit(DiceRoll diceRoll)
		{
			// if that isnt a code smell, I dont know what is
			int supply = 0;
			if ((GameTurn < 21) || (GameTurn > 68 && GameTurn < 77) || (GameTurn > 92 && GameTurn < 97))
			{
				// then supply level is B
				supply = 7000 + 1500 * diceRoll.Result;
			}
			else if ((GameTurn > 60 && GameTurn < 65) || (GameTurn > 84 && GameTurn < 89) || (GameTurn > 100 && GameTurn < 105))
			{
				// then supply level is A
				supply = 6000 + 1000 * diceRoll.Result;
			}
			else if ((GameTurn > 40 && GameTurn < 45) || (GameTurn > 48 && GameTurn < 53) || (GameTurn > 64 && GameTurn < 69) || GameTurn > 108)
			{
				// then supply level is C
				supply = 10000 + 1500 * diceRoll.Result;
			}
			else if ((GameTurn > 32 && GameTurn < 37) || (GameTurn > 52 && GameTurn < 57) || (GameTurn > 96 && GameTurn < 101))
			{
				// then supply level is D
				supply = 11000 + 2000 * diceRoll.Result;
			}
			else if ((GameTurn > 20 && GameTurn < 25) || (GameTurn > 28 && GameTurn < 33) || (GameTurn > 44 && GameTurn < 61))
			{
				// then supply is E (last condition satisfies C and D as well, but those were checked above and this is an else)
				supply = 11000 + 2500 * diceRoll.Result;
			}
			else if ((GameTurn < 29) || (GameTurn > 80 && GameTurn < 93))
			{
				// then supply is F (as long as E was checked above)
				supply = 15000 + 2000 * diceRoll.Result;
			}
			else
			{
				// supply level is G by omission
				supply = 32000 + 3000 * diceRoll.Result;
			}

			return supply;
		}

		public enum Evaporation
		{
			Flimsies, Jerrycans, HotWeather
		}

		private void Evaporate(Evaporation evaporation) 
		{
			foreach (Unit unit in Units)
			{
				unit.Evaporate(evaporation);
			}

			// all Fuel and Water sources that have limits also have evaporation. Cairo has no evaporation (unlimited supplies). Cities have no water evaporation (unlimited water). 
			foreach (SupplyDump dump in SupplyDumps)
			{
				dump.Evaporate(evaporation);
			}
		}

		public void TestSkipToNextGameTurn()
		{
			gameTurn += 1;
			opStage = 0;
		}

		public void TestGetDateAtTurn(int gt, int os)
		{
			int curGameTurn = GameTurn;
			int curOpStage = OpStage;
			gameTurn = gt;
			opStage = os;
			Console.WriteLine(GameDate);
			gameTurn = curGameTurn;
			opStage = curOpStage;
		}

		public void TestGetDateAtTurn(int gt)
		{
			int curGameTurn = GameTurn;
			int curOpStage = OpStage;
			gameTurn = gt;
			opStage = 0;
			Console.WriteLine(GameDate);
			gameTurn = curGameTurn;
			opStage = curOpStage;
		}


	}

	enum ShippingLanes
	{
		SicilyBizerta, SicilyTripoli, ItalyBenghazi, GreeceBenghazi, GreeceTobruk, ItalyTobruk
	}
}
