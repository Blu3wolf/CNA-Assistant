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

		// Properties

		public int Result { get; }

	}
}
