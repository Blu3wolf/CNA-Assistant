using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNA_Assistant
{
	class WeatherDeterminationPhase : TurnState
	{
		internal WeatherDeterminationPhase(Game game) : base(game)
		{

		}

		protected override void Entry()
		{
			// generate decision to roll for weather
			throw new NotImplementedException();
		}

		internal override void Execute(Command command)
		{
			// handle valid commands to determine weather, possibly add further decisions based on results of those commands
			throw new NotImplementedException();
		}

		internal override void Next()
		{
			throw new NotImplementedException();
		}
	}
}
