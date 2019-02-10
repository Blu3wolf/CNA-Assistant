using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNA_Assistant
{
	class PatrolPhase : TurnState
	{
		internal PatrolPhase(Game game) : base(game)
		{

		}

		protected override void Entry()
		{
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
				if (game.GoesSecond)
				{
					if (game.OpStage == 3)
					{
						game.GameTurn += 1;
						game.TurnState = new InitiativeDeterminationStage(game);
					}
					else
					{
						game.OpStage += 1;
						game.TurnState = new InitiativeDeclarationPhase(game);
					}
				}
				else
				{
					// then its the enemy's turn to phase
					game.IsPhasing = false;
					game.TurnState = new MovementSegment(game);
				}
			}
		}
	}
}
