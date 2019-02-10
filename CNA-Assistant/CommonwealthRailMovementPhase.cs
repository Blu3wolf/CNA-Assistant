﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNA_Assistant
{
	class CommonwealthRailMovementPhase : TurnState
	{
		internal CommonwealthRailMovementPhase(Game game) : base(game)
		{

		}

		protected override void Entry()
		{
			throw new NotImplementedException();
		}

		internal override void Execute(Command command)
		{
			throw new NotImplementedException();
		}

		internal override void Next()
		{
			game.TurnState = new RepairPhase(game);
		}
	}
}