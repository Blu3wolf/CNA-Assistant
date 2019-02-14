using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNA_Assistant
{
	public partial class Game
	{
		class NavalConvoyResolutionPhase : TurnState
		{
			internal NavalConvoyResolutionPhase(Game game) : base(game)
			{

			}

			protected override void Entry()
			{
				// decisions are relatively abstract at present, with the air game abstracted there is only an abstract attack on the Axis convoys.
				throw new NotImplementedException();
			}

			internal override void Execute(Command command)
			{
				throw new NotImplementedException();
			}

			internal override void Next()
			{
				if (Decisions.Count() == 0)
				{
					game.TurnState = new StoresExpenditureStage(game);
				}
			}
		}
	}
}
