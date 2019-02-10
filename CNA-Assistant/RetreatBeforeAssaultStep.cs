using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNA_Assistant
{
	class RetreatBeforeAssaultStep : TurnState
	{
		internal RetreatBeforeAssaultStep(Game game) : base(game)
		{

		}

		protected override void Entry()
		{
			throw new NotImplementedException();
		}

		internal override void Execute(Command command)
		{
			// handle commands to retreat (if non phasing) or to mark units as not in combat anymore
			throw new NotImplementedException();
		}

		internal override void Next()
		{
			game.TurnState = new ForceAssignmentStep(game);
		}
	}
}
