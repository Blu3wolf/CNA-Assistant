using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNA_Assistant
{
	public class DiceRoll
	{
		// A diceroll object is immutable (never changes). Passing one represents the results of rolling a set of dice. All 'interaction' occurs in the constructor.

		// The diceroll could represent a single die only? And multiple diceroll objects together represent rolling multiple dice. 

		public DiceRoll()
		{
			Random rand = new Random();
			Result = rand.Next(6) + 1; // Result can be from 1 to 6 inclusive, then.
		}

		public DiceRoll(int result)
		{
			if (result < 7 && result > 0)
			{
				Result = result;
			}
			else
			{
				throw new ArgumentOutOfRangeException();
			}
		}

		// Properties

		public int Result { get; }

	}

	public class TwoDice
	{
		// for when you need to roll two dice

		public TwoDice()
		{
			DiceRoll dice1 = new DiceRoll();
			DiceRoll dice2 = new DiceRoll();

			Sum = dice1.Result + dice2.Result;
			LargeResults = 10 * dice1.Result + dice2.Result;
		}

		public TwoDice(int result1, int result2)
		{
			DiceRoll dice1 = new DiceRoll(result1);
			DiceRoll dice2 = new DiceRoll(result2);

			Sum = dice1.Result + dice2.Result;
			LargeResults = 10 * dice1.Result + dice2.Result;
		}

		// Properties

		public int Sum { get; }

		public int LargeResults { get; }
	}
}
