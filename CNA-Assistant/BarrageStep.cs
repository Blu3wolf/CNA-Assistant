﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNA_Assistant
{
	class BarrageStep : TurnState
	{
		internal BarrageStep(Game game) : base(game)
		{

		}

		private List<CombatUnit> FiringUnits;

		private List<CombatUnit> UnitsInContact;

		private List<BarrageTarget> BarrageTargets;
		
		// methods

		private void NewBarrageTarget()
		{

		}

		protected override void Entry()
		{

			throw new NotImplementedException();
		}

		internal override void Execute(Command command)
		{
			// handle commands relevant to the Barrage step
			throw new NotImplementedException();
		}

		internal override void Next()
		{
			if (Decisions.Count() == 0)
			{
				game.TurnState = new RetreatBeforeAssaultStep(game);
			}
		}
	}
}
