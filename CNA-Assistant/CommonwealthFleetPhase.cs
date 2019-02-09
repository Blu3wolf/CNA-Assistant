using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNA_Assistant
{
	class CommonwealthFleetPhase : TurnState
	{
		internal CommonwealthFleetPhase(Game game) : base(game)
		{

		}

		protected override void Entry()
		{
			
		}

		internal override void Execute(Command command)
		{
			// handle commands for assigning ships to hexes, or repairing ships
			throw new NotImplementedException();
		}

		internal override void Next()
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
