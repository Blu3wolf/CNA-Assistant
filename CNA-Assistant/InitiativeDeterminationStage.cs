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
							if (command.Params[0] is DiceRoll && command.Params[1] is DiceRoll)
							{
								DetermineInitiative((DiceRoll)command.Params[0], (DiceRoll)command.Params[1]);
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

			private void DetermineInitiative(DiceRoll diceRoll, DiceRoll enemyRoll)
			{
				int roll = diceRoll.Result + game.GetInitiativeRating(game.SideIs);
				int oproll = enemyRoll.Result + game.GetInitiativeRating(game.EnemySide);

				if (roll > oproll)
				{
					DetermineInitiative(true);
				}
				else if(roll < oproll)
				{
					DetermineInitiative(false);
				}
			}

			internal override void Next()
			{
				if (InitiativeDetermined)
				{
					if (game.SideIs == Side.Axis)
					{
						game.TurnState = new NavalConvoySchedulePhase(game);
					}
					else
					{
						game.TurnState = new NavalConvoyResolutionPhase(game);
					}
				}
			}
		}
	}
}
