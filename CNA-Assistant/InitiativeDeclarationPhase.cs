using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNA_Assistant
{
	class InitiativeDeclarationPhase : TurnState
	{
		internal InitiativeDeclarationPhase(Game game) : base(game)
		{

		}

		protected override void Entry()
		{
			// only decision is whether or not user is player A (phasing first) or player B (phasing second)
			throw new NotImplementedException();
		}

		internal override void Execute(Command command)
		{
			// handle commands that decide who has initiative, then call Next() directly
			throw new NotImplementedException();
		}

		internal override void Next()
		{
			if (Decisions.Count() == 0)
			{
				game.TurnState = new WeatherDeterminationPhase(game);
			}
		}
	}
}
