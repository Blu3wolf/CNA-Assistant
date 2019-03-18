using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNA_Assistant
{
	public partial class Game
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

		// Properties

		public Side SideIs
		{ get;  }

		public Side EnemySide
		{ get; }

		public enum Side
		{ Axis, Commonwealth }

		public int GameTurn { get; private set; }// Game Turn one through to one hundred eleven. 

		public int OpStage { get; private set; } // Op Stage, zero to four - zero and four being before and after the Operations Stage of the turn. 

		private TurnState TurnState { get; set; } // current phase or step of the Sequence of Play - where in the turn we are up to, in other words. 

		public bool HasInitiative { get; private set; }

		public bool GoesSecond { get; private set; } // set at start of ops stage. Sounds like I want an ops stage object!

		public bool IsPhasing { get; private set; }

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

		public enum Weather
		{
			Normal, Hot, Sandstorm, Rainstorm
		}

		public Weather CurrentWeather { get; private set; }

		private int[] WeatherLocations { get; set; }

		public List<Unit> Units { get; private set; }

		public ReadOnlyDictionary<int, LocationSupplies> SupplyDumps { get => new ReadOnlyDictionary<int, LocationSupplies>(supplyDumps); }

		private Dictionary<int, LocationSupplies> supplyDumps { get; } // int is Location ID (replace with Hex later)

		public ReadOnlyCollection<HungryHex> HungryHexes => hungryHexes.AsReadOnly();

		private List<HungryHex> hungryHexes;

		public ReadOnlyCollection<int> Oases => oases.AsReadOnly();

		private List<int> oases;

		// Methods

		public void NextPhase()
		{
			TurnState.Next();
		}

		public void DetermineInitiative(bool hasInitiative)
		{
			TurnState.Execute(new Command(Command.Type.DetermineInitiative, hasInitiative));
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
			foreach (LocationSupplies dump in SupplyDumps.Values)
			{
				dump.Evaporate(evaporation);
			}
		}

		private void EndOpsStage()
		{
			foreach (Unit unit in Units)
			{
				unit.EndOpsStage();
			}
			OpStage += 1;
		}

		private void NextTurn()
		{
			EndOpsStage();
			GameTurn += 1;
			OpStage = 0;
			TurnState = new InitiativeDeterminationStage(this);
		}

		public Weather GetWeather(int location)
		{
			if (CurrentWeather == Weather.Hot || CurrentWeather == Weather.Normal)
			{
				return CurrentWeather;
			}
			else
			{
				int map = location / 10000;
				if (map > 0 && map <= 5)
				{
					if (WeatherLocations.Contains(map)) // technically, also if sandstorms and delta terrain, return Normal weather
					{
						return CurrentWeather;
					}
					else
					{
						return Weather.Normal;
					}
				}
				else
				{
					throw new ArgumentOutOfRangeException("location");
				}
			}
			
			
		}

		public void TestGetDateAtTurn(int gt, int os)
		{
			int curGameTurn = GameTurn;
			int curOpStage = OpStage;
			GameTurn = gt;
			OpStage = os;
			Console.WriteLine(GameDate);
			GameTurn = curGameTurn;
			OpStage = curOpStage;
		}

		public void TestGetDateAtTurn(int gt)
		{
			int curGameTurn = GameTurn;
			int curOpStage = OpStage;
			GameTurn = gt;
			OpStage = 0;
			Console.WriteLine(GameDate);
			GameTurn = curGameTurn;
			OpStage = curOpStage;
		}
	}
}
