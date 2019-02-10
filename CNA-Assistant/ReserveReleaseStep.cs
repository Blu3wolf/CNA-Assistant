using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNA_Assistant
{
	class ReserveReleaseStep : TurnState
	{
		internal ReserveReleaseStep(Game game) : base(game)
		{

		}

		protected override void Entry()
		{
			// generate Decisions
			throw new NotImplementedException();
		}

		internal override void Execute(Command command)
		{
			// handle commands for the reserve release step
			// one command allows user to go to a new movement segment
			throw new NotImplementedException();
		}

		internal override void Next()
		{
			if (Decisions.Count() == 0)
			{
				game.TurnState = new TruckConvoyMovementPhase(game);
			}
			throw new NotImplementedException();
		}
	}
}
