using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNA_Assistant
{
	public partial class Game
	{
		class InitiativeDeclarationPhase : TurnState
		{
			internal InitiativeDeclarationPhase(Game game) : base(game)
			{
				InitiativeDeclared = false;
			}

			protected override void Entry()
			{
				// only decision is whether or not user is player A (phasing first) or player B (phasing second)
				throw new NotImplementedException();
			}

			internal override void Execute(Command command)
			{
				switch (command.CommandType)
				{
					case Command.Type.DeclareInitiative:
						{
							if (command.Params[0] is bool)
							{
								DeclareInitiative((bool)command.Params[0]);
							}
						}
						break;
					default:
						break;
				}
			}

			private bool InitiativeDeclared;

			private void DeclareInitiative(bool goesFirst)
			{
				game.GoesSecond = !goesFirst;
				game.IsPhasing = goesFirst;
				InitiativeDeclared = true;
				Next();
			}

			internal override void Next()
			{
				if (InitiativeDeclared)
				{
					game.TurnState = new WeatherDeterminationPhase(game);
				}
			}
		}
	}
	
}
