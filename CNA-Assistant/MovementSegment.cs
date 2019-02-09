using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNA_Assistant
{
	class MovementSegment : TurnState
	{
		internal MovementSegment(Game game) : base(game)
		{

		}

		protected override void Entry()
		{
			
		}

		internal override void Execute(Command command)
		{
			// handle commands for Movement segment
			throw new NotImplementedException();
		}

		internal override void Next()
		{
			game.TurnState = new BreakdownDeterminationSegment(game);
		}
	}
}
