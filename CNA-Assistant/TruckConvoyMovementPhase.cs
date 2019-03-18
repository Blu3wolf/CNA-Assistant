﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNA_Assistant
{
	public partial class Game
	{
		class TruckConvoyMovementPhase : TurnState
		{
			internal TruckConvoyMovementPhase(Game game) : base(game)
			{

			}

			protected override void Entry()
			{
				throw new NotImplementedException();
			}

			internal override void Execute(Command command)
			{
				// handle commands relevant during truck convoy movement phase
				throw new NotImplementedException();
			}

			internal override void Next()
			{
				if (Decisions.Count() == 0)
				{
					game.TurnState = new RailMovementPhase(game);
				}
			}
		}
	}
}
