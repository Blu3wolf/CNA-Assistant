using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNA_Assistant
{
	class ReserveDesignationPhase : TurnState
	{
		internal ReserveDesignationPhase(Game game) : base(game)
		{

		}

		protected override void Entry()
		{
			
		}

		internal override void Execute(Command command)
		{
			// handle commands to make units reserve or not reserve
			throw new NotImplementedException();
		}

		internal override void Next()
		{
			game.TurnState = new MovementSegment(game);
		}
	}
}
