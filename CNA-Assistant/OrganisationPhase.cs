using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNA_Assistant
{
	class OrganisationPhase : TurnState
	{
		internal OrganisationPhase(Game game) : base(game)
		{

		}

		protected override void Entry()
		{
			// generate decisions
			throw new NotImplementedException();
		}

		internal override void Execute(Command command)
		{
			// handle commands relevant to organisation phase
			throw new NotImplementedException();
		}

		internal override void Next()
		{
			if (Decisions.Count == 0)
			{
				// add scheduled reinforcements, replacements to game
				throw new NotImplementedException();
				// then move to next step as usual
				if (game.SideIs == Game.Side.Commonwealth)
				{
					game.TurnState = new CommonwealthFleetPhase(game);
				}
				else
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
