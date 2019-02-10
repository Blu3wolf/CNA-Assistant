using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNA_Assistant
{
	class ForceAssignmentStep : TurnState
	{
		internal ForceAssignmentStep(Game game) : base(game)
		{

		}

		protected override void Entry()
		{
			// generate decisions
			throw new NotImplementedException();
		}

		internal override void Execute(Command command)
		{
			// handle commands to resolve decisions in the Force Assignment Step
			throw new NotImplementedException();
		}

		internal override void Next()
		{
			if (Decisions.Count() == 0)
			{
				game.TurnState = new AntiArmorStep(game);
			}
		}
	}
}
