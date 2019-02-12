using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNA_Assistant
{
	public partial class Game
	{
		class InitiativeDeterminationStage : TurnState
		{
			internal InitiativeDeterminationStage(Game game) : base(game)
			{

			}

			protected override void Entry()
			{
				// only one decision to make: who has initiative
				throw new NotImplementedException();
			}

			internal override void Execute(Command command)
			{
				throw new NotImplementedException();
			}

			internal override void Next()
			{
				if (Decisions.Count() == 0)
				{
					game.TurnState = new NavalConvoySchedulePhase(game);
				}
			}
		}
	}
}
