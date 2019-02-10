using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNA_Assistant
{
	class CloseAssaultStep : TurnState
	{
		internal CloseAssaultStep(Game game) : base(game)
		{

		}

		protected override void Entry()
		{
			// generate Decisions - one per Assault? probably more I guess
			throw new NotImplementedException();
		}

		internal override void Execute(Command command)
		{
			// handle commands relevant to the CloseAssault Step
			// One command allows the non-phasing user to return to the movement segment
			throw new NotImplementedException();
		}

		internal override void Next()
		{
			if (Decisions.Count() == 0)
			{
				if (game.IsPhasing)
				{
					game.TurnState = new ReserveReleaseStep(game);
				}
				else
				{
					if (game.GoesSecond)
					{
						game.IsPhasing = true;
						game.TurnState = new ReserveDesignationPhase(game);
					}
				}
			}
		}
	}
}
