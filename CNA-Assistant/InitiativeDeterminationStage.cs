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

			public bool InitiativeDetermined { get; private set; }

			protected override void Entry()
			{
				InitiativeDetermined = false;
			}

			internal override void Execute(Command command)
			{
				switch (command.CommandType)
				{
					case Command.Type.DetermineInitiative:
						{
							if (command.Params[0] is bool)
							{
								DetermineInitiative((bool)command.Params[0]);
							}
						}
						break;
					default:
						break;
				}
			}

			private void DetermineInitiative(bool hasInitiative)
			{
				game.HasInitiative = hasInitiative;
				InitiativeDetermined = true;
				Next();
			}

			internal override void Next()
			{
				if (InitiativeDetermined)
				{
					game.TurnState = new StoresExpenditureStage(game);
				}
			}
		}
	}
}
