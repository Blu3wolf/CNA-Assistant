using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNA_Assistant
{
	internal class StoresExpenditureStage : TurnState
	{
		internal StoresExpenditureStage(Game game) : base(game)
		{

		}

		protected override void Entry()
		{
			// handle evaporation/spillage of fuel and water
			// generate list of decisions
			throw new NotImplementedException();
		}

		internal override void Execute(Command command)
		{
			// handle valid Commands to resolve decisions
			throw new NotImplementedException();
		}

		internal override void Next()
		{
			if (Decisions.Count == 0)
			{
				game.OpStage = 1;
				game.TurnState = new InitiativeDeclarationPhase(game);
			}
		}
	}
}
