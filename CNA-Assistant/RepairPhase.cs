using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNA_Assistant
{
	class RepairPhase : TurnState
	{
		internal RepairPhase(Game game) : base(game)
		{

		}

		protected override void Entry()
		{
			throw new NotImplementedException();
		}

		internal override void Execute(Command command)
		{
			// handle commands relevant to the repair phase
			throw new NotImplementedException();
		}

		internal override void Next()
		{
			game.TurnState = new PatrolPhase(game);
		}
	}
}
