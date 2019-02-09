using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNA_Assistant
{
	class BreakdownDeterminationSegment : TurnState
	{
		internal BreakdownDeterminationSegment(Game game) : base(game)
		{

		}

		protected override void Entry()
		{
			// determine breakdown results based on unit movement
			throw new NotImplementedException();
		}

		internal override void Execute(Command command)
		{
			// handle commands to resolve breakdowns
			throw new NotImplementedException();
		}

		internal override void Next()
		{
			if (Decisions.Count() == 0)
			{
				game.TurnState = new PositionDeterminationSegment(game);
			}
		}
	}
}
