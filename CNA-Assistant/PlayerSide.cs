using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNA_Assistant
{
	class PlayerSide
	{
		/* The top level object that represents the entire Game model. A reference to the PlayerSide is the primary tool required by the ViewModel, to manipulate the game state.
		 * All units the user has, the current game turn, all of it. Saving the game represents essentially writing the state of the PlayerSide to disc. Loading the game, creating
		 * a new PlayerSide object with specified values from the save file. Selecting New Game means creating a new PlayerSide object, using pre-defined data (Scenario data). */

		public PlayerSide() // Eventually, this should take a SaveFile object as an argument, that contains the values to populate the object with.
		{
			// Initialises all the properties of a PlayerSide object.


		}

		// Properties

		public int GameTurn; // Game Turn one through to one hundred eleven. 

		public int OpStage; // Op Stage, zero to four - zero and four being before and after the Operations Stage of the turn. 

		//public PhaseStep TurnStep; // current phase or step of the Sequence of Play - where in the turn we are up to, in other words. 

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

		// Methods

		public void NextStep() // Advances play to the next phase or step of the Sequence of Play - potentially retreating in the Sequence of Play.
		{

		}

		public void TestGetDateAtTurn(int gameTurn, int opStage)
		{
			GameTurn = gameTurn;
			OpStage = opStage;
			Console.WriteLine(GameDate);
		}

		public void TestGetDateAtTurn(int gameTurn)
		{
			GameTurn = gameTurn;
			OpStage = 0;
			Console.WriteLine(GameDate);
		}


	}
}
