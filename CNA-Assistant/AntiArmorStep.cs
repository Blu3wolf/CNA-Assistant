using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNA_Assistant
{
	class AntiArmorStep : TurnState
	{
		internal AntiArmorStep(Game game) : base(game)
		{

		}

		protected override void Entry()
		{
			// Generate decisions - possibly Assault class inherits from Decision?
			throw new NotImplementedException();
		}

		internal override void Execute(Command command)
		{
			// handle commands to resolve anti armor combats (should be List of Decisions representing each combat I guess)
			throw new NotImplementedException();
		}

		internal override void Next()
		{
			if (Decisions.Count() == 0)
			{
				game.TurnState = new CloseAssaultStep(game);
			}
		}
	}
}
