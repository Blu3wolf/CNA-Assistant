using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNA_Assistant
{
	public partial class Game
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
				if (true) // if no outstanding decisions
				{
					if (game.GoesSecond)
					{
						if (game.OpStage == 3)
						{
							game.NextTurn();
						}
						else
						{
							game.EndOpsStage();
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
}
