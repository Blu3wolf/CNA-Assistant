using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNA_Assistant
{
	public partial class Game
	{
		class NavalConvoyArrivalPhase : TurnState
		{
			public NavalConvoyArrivalPhase(Game game) : base(game)
			{

			}

			protected override void Entry()
			{
				

				throw new NotImplementedException();
			}

			internal override void Execute(Command command)
			{
				// add new units to game manually (planned and scheduled arrivals, handled on paper in first release)
				throw new NotImplementedException();
			}

			internal override void Next()
			{
				if (true) // later there will be blocking decisions that complicate the arrivals phase
				{
					// then move to next step as usual - for now, skip Fleet Movement and Repair steps
					/*if (game.SideIs == Game.Side.Commonwealth)
					{
						game.TurnState = new CommonwealthFleetPhase(game);
					}
					else*/
					{
						if (game.IsPhasing)
						{
							game.TurnState = new ReserveDesignationPhase(game);
						}
						else
						{
							game.TurnState = new MovementSegment(game);
						}
					}
				}
			}
		}
	}
	
}
