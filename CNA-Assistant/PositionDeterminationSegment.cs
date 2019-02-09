using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNA_Assistant
{
	class PositionDeterminationSegment : TurnState
	{
		internal PositionDeterminationSegment(Game game) : base(game)
		{

		}

		protected override void Entry()
		{
			throw new NotImplementedException();
		}

		internal override void Execute(Command command)
		{
			// handle commands to assign barrage units Forward or Back, and to assign units to be in combat or not
			throw new NotImplementedException();
		}

		internal override void Next()
		{
			throw new NotImplementedException();
		}
	}
}
