using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNA_Assistant
{
	public partial class Game
	{
		class NavalConvoySchedulePhase : TurnState
		{
			internal NavalConvoySchedulePhase(Game game) : base(game)
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

			private int ShippingLimit;

			private enum ShippingLevel
			{
				A, B, C, D, E, F, G
			}

			private static readonly ShippingLevel[] ShippingLevelsChart =
				{
					ShippingLevel.B,
					ShippingLevel.B,
					ShippingLevel.B,
					ShippingLevel.B,
					ShippingLevel.B,
					ShippingLevel.E,
					ShippingLevel.F,
					ShippingLevel.E,
					ShippingLevel.D,
					ShippingLevel.G,
					ShippingLevel.C,
					ShippingLevel.E,
					ShippingLevel.C,
					ShippingLevel.D,
					ShippingLevel.E,
					ShippingLevel.A,
					ShippingLevel.C,
					ShippingLevel.B,
					ShippingLevel.B,
					ShippingLevel.G,
					ShippingLevel.F,
					ShippingLevel.A,
					ShippingLevel.F,
					ShippingLevel.B,
					ShippingLevel.D,
					ShippingLevel.A,
					ShippingLevel.G,
					ShippingLevel.C
				};

			private ShippingLevel GetShippingLevel()
			{
				// gets level for next week - which is the one we are planning for
				int month = (game.GameTurn) / 4;
				return ShippingLevelsChart[month];
			}

			private void SetNextTurnShippingLimit(DiceRoll diceRoll)
			{
				ShippingLevel shippingLevel = GetShippingLevel();
				int supply = 0;
				switch (shippingLevel)
				{
					case ShippingLevel.A:
						supply = 6000 + 1000 * diceRoll.Result;
						break;
					case ShippingLevel.B:
						supply = 7000 + 1500 * diceRoll.Result;
						break;
					case ShippingLevel.C:
						supply = 10000 + 1500 * diceRoll.Result;
						break;
					case ShippingLevel.D:
						supply = 11000 + 2000 * diceRoll.Result;
						break;
					case ShippingLevel.E:
						supply = 11000 + 2500 * diceRoll.Result;
						break;
					case ShippingLevel.F:
						supply = 15000 + 2000 * diceRoll.Result;
						break;
					case ShippingLevel.G:
						supply = 32000 + 3000 * diceRoll.Result;
						break;
					default:
						break;
				}

				// round up to nearest thousand
				if (supply % 1000 != 0)
				{
					supply /= 1000;
					supply++;
					supply *= 1000;
				}

				ShippingLimit = supply;
				// subtract from Shipping Limit the weight of previously Planned Replacements!

			}

			internal override void Next()
			{
				// check if all scheduled Convoys are valid 
				// limits include port limits, and shipping limit

				game.TurnState = new NavalConvoyResolutionPhase(game);
			}
		}
	}
}
